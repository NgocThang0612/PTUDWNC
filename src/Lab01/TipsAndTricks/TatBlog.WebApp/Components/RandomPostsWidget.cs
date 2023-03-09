using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class RandomPostsWidget : ViewComponent
{
    private readonly IBlogRepository _blogRepository;

    public RandomPostsWidget(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // TOP 5 bài viết ngẫu nhiên
        var posts = await _blogRepository.RandomPostAsync(5);

        return View(posts);
    }
}
