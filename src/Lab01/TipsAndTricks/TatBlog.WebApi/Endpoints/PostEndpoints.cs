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

public static class PostEndpoints
{
    public static WebApplication MapPostEndpoints(
        this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api/posts");

        routeGroupBuilder.MapGet("/", GetPosts)
            .WithName("GetPosts")
            .Produces<ApiResponse<PaginationResult<PostDto>>>();

        routeGroupBuilder.MapGet("/featured/{limit:int}", GetFeaturedPosts)
            .WithName("GetFeaturedPostsByLimit")
            .Produces<ApiResponse<PostDto>>();

        routeGroupBuilder.MapGet("/random/{limit:int}", GetRandomPosts)
            .WithName("GetRandomPostsByLimit")
            .Produces<ApiResponse<PostDto>>();

        routeGroupBuilder.MapGet("/archives/{limit:int}", GetArchivesPosts)
            .WithName("GetArchivesPostsByLimit")
            .Produces<ApiResponse<PostDto>>();

        routeGroupBuilder.MapGet("/{id:int}", GetPostDetails)
            .WithName("GetPostById")
            .Produces<ApiResponse<PostDetail>>();

        routeGroupBuilder.MapGet(
            "/byslug/{slug:regex(^[a-z0-9_-]+$)}",
            GetPostBySlug)
            .WithName("GetPostBySlug")
            .Produces<ApiResponse<PaginationResult<PostDto>>>();

        routeGroupBuilder.MapPost("/", AddPost)
            .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
            .WithName("AddNewPost")
            .Produces(401)
            .Produces<ApiResponse<PostDto>>();

        routeGroupBuilder.MapPost("/{id:int}/picture", SetPostPicture)
            .WithName("SetPostPicture")
            .Accepts<IFormFile>("multipart/form-data")
            .Produces(401)
            .Produces<ApiResponse<string>>();

        routeGroupBuilder.MapPut("/{id:int}", UpdatePost)
            .WithName("UpdateAnPost")
            .Produces(401)
            .Produces<ApiResponse<string>>();

        routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
            .WithName("DeleteAnPost")
            .Produces(401)
            .Produces<ApiResponse<string>>();

        routeGroupBuilder.MapGet("/{id:int}/comments", GetCommentByPost)
            .WithName("GetCommentByPost")
            .Produces(401)
            .Produces<ApiResponse<Comment>>();

        return app;
    }

    private static async Task<IResult> GetPosts(
        [AsParameters] PostFilterModel model,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var postQuery = mapper.Map<PostQuery>(model);

        var postsList = await blogRepository
            .GetPagedPostQueryAsync(postQuery, model, posts => posts.ProjectToType<PostDto>());

        var paginationResult =
            new PaginationResult<PostDto>(postsList);

        return Results.Ok(ApiResponse.Success(paginationResult));
    }

    private static async Task<IResult> GetFeaturedPosts(
        int numPost,
        IBlogRepository blogRepository)
    {
        var post = await blogRepository.GetPopularArticlesAsync(numPost);
        return post == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
            $"Không tìm thấy bài viết {numPost}"))
            : Results.Ok(ApiResponse.Success((post)));
    }

    private static async Task<IResult> GetRandomPosts(
        int numPost,
        IBlogRepository blogRepository)
    {
        var post = await blogRepository.RandomPostAsync(numPost);
        return post == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
            $"Không tìm thấy bài viết {numPost}"))
            : Results.Ok(ApiResponse.Success((post)));
    }

    private static async Task<IResult> GetArchivesPosts(
        int numPost,
        IBlogRepository blogRepository)
    {
        var post = await blogRepository.CountByPostAsync(numPost);
        return post == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
            $"Không tìm thấy bài viết {numPost}"))
            : Results.Ok(ApiResponse.Success((post)));
    }

    private static async Task<IResult> GetPostDetails(
        int id,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var post = await blogRepository.GetCachedPostByIdAsync(id);
        return post == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy bài viết có mã số {id}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<PostDto>(post)));
    }

    private static async Task<IResult> GetPostBySlug(
        string slug,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var post = await blogRepository.GetPostBySlugAsync(slug);
        return post == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy bài viết có mã số {slug}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<PostDto>(post)));
    }

    private static async Task<IResult> AddPost(
        PostEditModel model,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        if (await blogRepository
                .IsPostSlugExistedAsync(0, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
        }

        var post = mapper.Map<Post>(model);
        await blogRepository.AddOrUpdatePostAsync(post);

        return Results.Ok(ApiResponse.Success(
            mapper.Map<PostDto>(post), HttpStatusCode.Created));
    }

    private static async Task<IResult> SetPostPicture(
        int id, IFormFile imageFile,
        IBlogRepository blogRepository,
        IMediaManager mediaManager)
    {
        var imageUrl = await mediaManager.SaveFileAsync(
            imageFile.OpenReadStream(),
            imageFile.FileName, imageFile.ContentType);

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest,
                "Không lưu được tập tin"));
        }

        await blogRepository.SetImageUrlAsync(id, imageUrl);
        return Results.Ok(ApiResponse.Success(imageUrl));
    }

    private static async Task<IResult> UpdatePost(
        int id, PostEditModel model,
        IValidator<PostEditModel> validator,
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
                .IsPostSlugExistedAsync(id, model.UrlSlug))
        {
            return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
        }

        var post = mapper.Map<Post>(model);
        post.Id = id;

        return await blogRepository.AddOrUpdatePostAsync(post)
            ? Results.Ok(ApiResponse.Success("Post is update",
                      HttpStatusCode.NoContent))
            : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                      "Could not find post"));
    }

    private static async Task<IResult> DeletePost(
        int id, IBlogRepository blogRepository)
    {
        return await blogRepository.DeletePostByIdAsync(id)
            ? Results.Ok(ApiResponse.Success("Post is delered",
                      HttpStatusCode.NoContent))
            : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                      "Could not find post"));
    }

    private static async Task<IResult> GetCommentByPost(
        int id,
        IBlogRepository blogRepository,
        IMapper mapper)
    {
        var comment = await blogRepository.GetCommentByIdAsync(id);
        return comment == null
            ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy bình luận của bài viết có mã số {id}"))
            : Results.Ok(ApiResponse.Success(mapper.Map<Comment>(comment)));
    }
}
