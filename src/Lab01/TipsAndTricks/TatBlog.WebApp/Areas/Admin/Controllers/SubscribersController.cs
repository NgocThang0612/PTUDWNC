using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Entities;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.Services.Subscribers;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers;

public class SubscribersController : Controller
{
    private readonly ILogger<SubscribersController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly IMediaManager _mediaManager;
    private readonly IMapper _mapper;

    public SubscribersController(
        ILogger<SubscribersController> logger,
        IBlogRepository blogRepository,
        IAuthorRepository authorRepository,
        ISubscriberRepository subscriberRepository,
        IMediaManager mediaManager,
        IMapper mapper)
    {
        _logger = logger;
        _authorRepository = authorRepository;
        _blogRepository = blogRepository;
        _mediaManager = mediaManager;
        _mapper = mapper;
        _subscriberRepository= subscriberRepository;
    }


    //public IActionResult Index()
    //{
    //    return View();
    //}

    public async Task<IActionResult> Index(
        [FromQuery(Name = "p")] int pageNumber = 1,
        [FromQuery(Name = "ps")] int pageSize = 5)
    {
        _logger.LogInformation("Tạo điều kiện truy vấn");

        // Sử dụng Mapster để tạo đối tượng PostQuery
        // từ đối tượng PostFillterModel model
        //var categoryQuery = _mapper.Map<PostQuery>(model);
        //var postQuery = new PostQuery()
        //{
        //    Keyword = model.Keyword,
        //    CategoryId = model.CategoryId,
        //    AuthorId = model.AuthorId,
        //    Year = model.Year,
        //    Month = model.Month,
        //};

        //ViewBag.PostsList = await _blogRepository
        //    .GetPagedPostsAsync(categoryQuery, pageNumber, pageSize);

        //await PopulatePostFilterModelAsync(model);
        var model = await _subscriberRepository.GetPagedSubscribersAsync(pageNumber, pageSize);
        return View(model);
    }

    public async Task<IActionResult> DeleteCategory(int id = 0)
    {
        await _blogRepository.DeleteCategoryByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ToggleShowOnMenu(int id = 0)
    {
        await _blogRepository.ToggleShowOnMenuAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id = 0)
    {
        //ID = 0 <=> Thêm bài viết mới
        //ID > 0 : Đọc dữ liệu của bài viết từ CSDL
        var subscriber = id > 0
            ? await _subscriberRepository.GetSubscriberByIdAsync(id)
            : null;

        //Tạo view model từ dữ liệu của bài viết
        var model = subscriber == null
            ? new SubscriberEditModel()
            : _mapper.Map<SubscriberEditModel>(subscriber);

        //Gán các giá trị khác cho view model


        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        [FromServices] IValidator<SubscriberEditModel> subscirberValidator,
        SubscriberEditModel model)
    {
        var validationResult = await subscirberValidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
        }


        var subscriber = model.Id > 0
            ? await _subscriberRepository.GetSubscriberByIdAsync(model.Id)
            : null;

        

        _mapper.Map(model, subscriber);
        subscriber.Voluntary = false;
        subscriber.UnsubscribeDate = DateTime.Now;


        await _subscriberRepository.UpdateSubscriberAsync(
            subscriber);

        return RedirectToAction(nameof(Index));
    }
}
