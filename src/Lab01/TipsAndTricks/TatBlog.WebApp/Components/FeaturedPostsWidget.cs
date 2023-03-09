using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class FeaturedPostsWidget : ViewComponent
{
    private readonly IBlogRepository _blogRepository;

    public FeaturedPostsWidget(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Top 3 bài viết được xem nhiều nhất
        var posts = await _blogRepository.GetPopularArticlesAsync(3);

        return View(posts);
    }
}
