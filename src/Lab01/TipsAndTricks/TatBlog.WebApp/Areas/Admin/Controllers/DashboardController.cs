using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.Services.Subscribers;

namespace TatBlog.WebApp.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ISubscriberRepository _subscriberRepository;

    public DashboardController(
        ILogger<DashboardController> logger,
        IBlogRepository blogRepository,
        IAuthorRepository authorRepository,
        ISubscriberRepository subscriberRepository)
    {
        _logger = logger;
        _authorRepository = authorRepository;
        _blogRepository = blogRepository;
        _subscriberRepository = subscriberRepository;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalPosts = await _blogRepository.TotalPosts();
        ViewBag.PostsUnpublished = await _blogRepository.PostsUnpublished();
        ViewBag.NumberOfCategories = await _blogRepository.NumberOfCategories();
        ViewBag.NumberOfAuthors = await _authorRepository.NumberOfAuthors();
        ViewBag.NumberOfComments = await _blogRepository.NumberOfComments();
        ViewBag.NumberOfFollower = await _subscriberRepository.NumberOfFollowerAsync();
        ViewBag.NumberOfFollowerToDay = await _subscriberRepository.NumberOfFollowerTodayAsync();

        return View();
    }
}
