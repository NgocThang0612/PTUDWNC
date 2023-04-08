using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;
using TatBlog.Core.Collections;
using TatBlog.Core.Constants;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints;

public static class TagEndpoints
{
    public static WebApplication MapTagEndpoints(
        this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api/tags");

        routeGroupBuilder.MapGet("/", GetTags)
            .WithName("GetTags")
            .Produces<ApiResponse<PaginationResult<TagItem>>>();

        routeGroupBuilder.MapGet("/{id:int}", GetTagDetails)
            .WithName("GetTagById")
            .Produces<ApiResponse<TagItem>>();
        //.Produces(404);

        routeGroupBuilder.MapGet("/tagcloud", GetTagCloud)
            .WithName("GetTagCloud")
            .Produces<ApiResponse<IList<TagItem>>>();

        routeGroupBuilder.MapGet(
            "/{slug:regex(^[a-z0-9_-]+$)}/posts",
            GetPostsByTagSlug)
            .WithName("GetPostsByTagSlug")
            .Produces<ApiResponse<PaginationResult<TagDto>>>();

        routeGroupBuilder.MapPost("/", AddTag)
            .AddEndpointFilter<ValidatorFilter<TagEditModel>>()
            .WithName("AddNewTag")
            .Produces(401)
            .Produces<ApiResponse<CategoryItem>>();
        //.Produces(201)
        //.Produces(400)
        //.Produces(409);

        routeGroupBuilder.MapPut("/{id:int}", UpdateTag)
            .WithName("UpdateAnTag")
            .Produces(401)
            .Produces<ApiResponse<string>>();
        //.AddEndpointFilter<ValidatorFilter<AuthorEditModel>>()
        //.Produces(204)
        //.Produces(400)
        //.Produces(409);

        routeGroupBuilder.MapDelete("/{id:int}", DeleteTag)
            .WithName("DeleteAnTag")
            .Produces(401)
            .Produces<ApiResponse<string>>();
        //.Produces(204)
        //.Produces(404);

        return app;
    }

    private static async Task<IResult> GetTags(
        [AsParameters] TagFilterModel model,
        IBlogRepository blogRepository)
    {
        var tagsList = await blogRepository
            .GetPagedTagAsync(model, model.Name);

        var paginationResult =
            new PaginationResult<TagItem>(tagsList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> GetTagCloud(
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var tag = await blogRepository.GetAllTagsByPostAsync();
        return tag == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy thẻ"))
            : Results.Ok(ApiResponse.Success(mapper.Map<IList<TagItem>>(tag)));
    }

    private static async Task<IResult> GetTagDetails(
        int id,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var tag = await blogRepository.GetCachedTagByIdAsync(id);
        return tag == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy thẻ có mã số {id}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<TagItem>(tag)));
    }

    private static async Task<IResult> GetPostsByTagSlug(
        [FromRoute] string slug,
        [AsParameters] PagingModel pagingModel,
        IBlogRepository blogRepository)
    {
        var postQuery = new PostQuery()
        {
            TagSlug = slug,
            PublishedOnly = true,
        };
        var tagsList = await blogRepository.GetPagedPostsAsync(
            postQuery, pagingModel,
            tags => tags.ProjectToType<TagDto>());
        var paginationResult = new PaginationResult<TagDto>(tagsList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> AddTag(
        TagEditModel model,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        if (await blogRepository
                .IsTagSlugExistedAsync(0, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
        }

        var tag = mapper.Map<Tag>(model);
        await blogRepository.AddOrUpdateTagAsync(tag);

        return Results.Ok(ApiResponse.Success(
            mapper.Map<TagItem>(tag), HttpStatusCode.Created));
    }


    private static async Task<IResult> UpdateTag(
        int id, TagEditModel model,
        IValidator<TagEditModel> validator,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.BadRequest, validationResult));
        }

        if (await blogRepository
                .IsTagSlugExistedAsync(id, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
        }

        var tag = mapper.Map<Tag>(model);
        tag.Id = id;

        return await blogRepository.AddOrUpdateTagAsync(tag)
            ? Results.Ok(ApiResponse.Success("Tag is update",
                      HttpStatusCode.NoContent))
            : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                      "Could not find tag"));
    }

    private static async Task<IResult> DeleteTag(
        int id, IBlogRepository blogRepository)
    {
        return await blogRepository.DeleteTagsByIdAsync(id)
            ? Results.Ok(ApiResponse.Success("Tag is delered",
                      HttpStatusCode.NoContent))
            : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                      "Could not find tag"));
    }

}
