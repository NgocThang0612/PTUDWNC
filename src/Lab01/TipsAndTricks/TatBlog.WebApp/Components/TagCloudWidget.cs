using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components;

public class TagCloudWidget : ViewComponent
{
    private readonly IBlogRepository _blogRepository;

    public TagCloudWidget(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Lấy danh sách chủ đề
        var tags = await _blogRepository.GetAllTagsByPostAsync();

        return View(tags);
    }
}
