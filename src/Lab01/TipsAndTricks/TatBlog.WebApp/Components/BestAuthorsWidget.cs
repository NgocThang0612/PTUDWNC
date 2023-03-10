using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class BestAuthorsWidget : ViewComponent
{
    private readonly IAuthorRepository _authorRepository;

    public BestAuthorsWidget(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // TOP 4 tác giả có nhiều bài viết nhất
        var authors = await _authorRepository.ListAuthorAsync(4);
        return View(authors);
    }
}
