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

public static class CommentEndpoints
{
    public static WebApplication MapCommentEndpoints(
        this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api/comments");

        routeGroupBuilder.MapGet("/", GetComments)
            .WithName("GetComments")
            .Produces<ApiResponse<PaginationResult<Comment>>>();

        routeGroupBuilder.MapGet("/{id:int}", GetCommentDetails)
            .WithName("GetCommentById")
            .Produces<ApiResponse<Comment>>();
        //.Produces(404);

        routeGroupBuilder.MapPost("/", AddComment)
            .AddEndpointFilter<ValidatorFilter<CommentEditModel>>()
            .WithName("AddNewTag")
            .Produces(401)
            .Produces<ApiResponse<CategoryItem>>();


        //routeGroupBuilder.MapPut("/{id:int}", UpdateComment)
        //    .WithName("UpdateAnComment")
        //    .Produces(401)
        //    .Produces<ApiResponse<string>>();


        routeGroupBuilder.MapDelete("/{id:int}", DeleteComment)
            .WithName("DeleteAnComment")
            .Produces(401)
            .Produces<ApiResponse<string>>();


        return app;
    }

    private static async Task<IResult> GetComments(
        [AsParameters] CommentFilterModel model,
        IBlogRepository blogRepository)
    {
        var commentsList = await blogRepository
            .GetPagedCommentAsync(model, model.Name);

        var paginationResult =
            new PaginationResult<Comment>(commentsList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> GetCommentDetails(
        int id,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var tag = await blogRepository.GetCachedCommentByIdAsync(id);
        return tag == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy bình luận có mã số {id}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<Comment>(tag)));
    }


    private static async Task<IResult> AddComment(
        CommentEditModel model,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        if (await blogRepository
                .IsCommentSlugExistedAsync(0, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
        }

        var comment = mapper.Map<Comment>(model);
        await blogRepository.CreateOrUpdateCommentAsync(comment);

        return Results.Ok(ApiResponse.Success(
            mapper.Map<Comment>(comment), HttpStatusCode.Created));
    }


    //private static async Task<IResult> UpdateComment(
    //    int id, CommentEditModel model,
    //    IValidator<CommentEditModel> validator,
    //    IBlogRepository blogRepository,
    //    IMapper mapper)
    //{
    //    var validationResult = await validator.ValidateAsync(model);

    //    if (!validationResult.IsValid)
    //    {
    //        return Results.Ok(ApiResponse.Fail(
    //            HttpStatusCode.BadRequest, validationResult));
    //    }

    //    if (await blogRepository
    //            .IsCommentSlugExistedAsync(id, model.UrlSlug))
    //    {
    //        return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
    //            $"Slug '{model.UrlSlug}' đã được sử dụng"));
    //    }

    //    var comment = mapper.Map<Comment>(model);
    //    comment.Id = id;

    //    return await blogRepository.CreateOrUpdateCommentAsync(comment)
    //        ? Results.Ok(ApiResponse.Success("Comment is update",
    //                  HttpStatusCode.NoContent))
    //        : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
    //                  "Could not find comment"));
    //}

    private static async Task<IResult> DeleteComment(
        int id, IBlogRepository blogRepository)
    {
        return await blogRepository.DeleteCommentByIdAsync(id)
            ? Results.Ok(ApiResponse.Success("Comment is delered",
                      HttpStatusCode.NoContent))
            : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                      "Could not find comment"));
    }

}
