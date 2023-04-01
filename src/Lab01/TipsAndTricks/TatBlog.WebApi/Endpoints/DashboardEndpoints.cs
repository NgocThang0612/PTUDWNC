using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Subscribers;
using TatBlog.WebApi.Models;
using TatBlog.WebApi.Models.Dashboard;

namespace TatBlog.WebApi.Endpoints
{
    public static class DashboardEndpoints
    {
        public static WebApplication MapDashboardEndpoints(
      this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/dashboards");

            routeGroupBuilder.MapGet("/", GetDashboard)
             .WithName("GetDashboard")
                .Produces<ApiResponse<Dashboards>>();
            return app;
        }

        private static async Task<IResult> GetDashboard(
            IAuthorRepository authorRepository,
            IBlogRepository blogRepository,
            ISubscriberRepository subscriberRepository)
        {
            var CountPost = await blogRepository.TotalPosts();
            var CountAuthor = await authorRepository.NumberOfAuthors();
            var CountCategory = await blogRepository.NumberOfCategories();
            var CountUnPublicPost = await blogRepository.PostsUnpublished();
            var CountComment = await blogRepository.NumberOfComments();
            var CountSubscriber = await subscriberRepository.NumberOfFollowerAsync();
            var CountSubscriberState = await subscriberRepository.NumberOfFollowerTodayAsync();

            var dashboard = new Dashboards()
            {
                TotalPost = CountPost,
                TotalAuthor = CountAuthor,
                TotalCategorie = CountCategory,
                TotalSubscriber = CountSubscriber,
                TotalUnapprovedComment = CountComment,
                TotalUnpublishedPost = CountUnPublicPost,
                TotalNewSubscriberToday = CountSubscriberState
            };
            return Results.Ok(ApiResponse.Success(dashboard));
        }
    }
}