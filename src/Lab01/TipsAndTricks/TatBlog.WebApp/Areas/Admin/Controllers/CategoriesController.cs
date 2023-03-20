using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers;

public class CategoriesController : Controller
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMediaManager _mediaManager;
    private readonly IMapper _mapper;

    public CategoriesController(
        ILogger<CategoriesController> logger,
        IBlogRepository blogRepository,
        IAuthorRepository authorRepository,
        IMediaManager mediaManager,
        IMapper mapper)
    {
        _logger = logger;
        _authorRepository = authorRepository;
        _blogRepository = blogRepository;
        _mediaManager = mediaManager;
        _mapper = mapper;

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
        var model = await _blogRepository.GetPagedCategoryAsync(pageNumber, pageSize);
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
        var category = id > 0
            ? await _blogRepository.GetCategoryByIdAsync(id)
            : null;

        //Tạo view model từ dữ liệu của bài viết
        var model = category == null
            ? new CategoriesEditModel()
            : _mapper.Map<CategoriesEditModel>(category);

        //Gán các giá trị khác cho view model
       

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        [FromServices] IValidator<CategoriesEditModel> categoryValidator,
        CategoriesEditModel model)
    {
        var validationResult = await categoryValidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
        }


        var category = model.Id > 0
            ? await _blogRepository.GetCategoryByIdAsync(model.Id)
            : null;

        if (category == null)
        {
            category = _mapper.Map<Category>(model);

            category.Id = 0;
        }
        else
        {
            _mapper.Map(model, category);

        }

        await _blogRepository.CreateOrUpdateCategoryAsync(
            category);

        return RedirectToAction(nameof(Index));
    }
}

