//using Microsoft.AspNetCore.Mvc;
//using TatBlog.Services.Blogs;

//namespace TatBlog.WebApp.Components;

//public class BestAuthorsWidget : ViewComponent
//{
//    private readonly IBlogRepository _blogRepository;

//    public BestAuthorsWidget(IBlogRepository blogRepository)
//    {
//        _blogRepository = blogRepository;
//    }

//    public async Task<IViewComponentResult> InvokeAsync()
//    {
//        // TOP 4 tác giả có nhiều bài viết nhất
//        var authors = await _blogRepository.ListAuthorAsync(4);

//        return View(authors);
//    }
//}
