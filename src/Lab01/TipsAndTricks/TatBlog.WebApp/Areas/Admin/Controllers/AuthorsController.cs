using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Entities;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers;

public class AuthorsController : Controller
{
    private readonly ILogger<PostsController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IMediaManager _mediaManager;
    private readonly IValidator<PostEditModel> _validator;


    public AuthorsController(
        ILogger<PostsController> logger,
        IBlogRepository blogRepository,
        IAuthorRepository authorRepository,
        IMapper mapper,
        IMediaManager mediaManager,
        IValidator<PostEditModel> validator)
    {
        _logger = logger;
        _blogRepository = blogRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _mediaManager = mediaManager;
        _validator = validator;

    }

    public async Task<IActionResult> Index(
        [FromQuery(Name = "p")] int pageNumber = 1,
        [FromQuery(Name = "ps")] int pageSize = 5)
    {

        var model = await _authorRepository.GetPagedAuthorsAsync(pageNumber, pageSize);

        return View(model);
    }

    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorRepository.DeleteAuthorByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id = 0)
    {
        // Id = 0 <=> Thêm bài viết mới
        // Id > 0: Đọc bài viết từ CSDL
        var author = id > 0
            ? await _authorRepository.GetAuthorByIdAsync(id)
            : null;

        // Tạo view model từ dữ liệu bài viết
        var model = author == null
            ? new AuthorEditModel()
            : _mapper.Map<AuthorEditModel>(author);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
       [FromServices] IValidator<AuthorEditModel> authorValidator,
       AuthorEditModel model)
    {
        var validationResult = await authorValidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
        }

   
        var author = model.Id > 0
            ? await _authorRepository.GetAuthorByIdAsync(model.Id)
            : null;

        if (author == null)
        {
            author = _mapper.Map<Author>(model);

            author.Id = 0;
            author.JoinedDate = DateTime.Now;
        }
        else
        {
            _mapper.Map(model, author);
        }

        //Nếu người dùng có upload hình ảnh minh họa cho bài viết
        if (model.ImageFile?.Length > 0)
        {
            // Thì thực hiện việc lưu tập tin vào thư  mục uploads
            var newImagePath = await _mediaManager.SaveFileAsync(
                model.ImageFile.OpenReadStream(),
                model.ImageFile.FileName,
                model.ImageFile.ContentType);

            // Nếu lưu thành công, xóa tập tin hình ảnh cũ (nếu có)
            if (!string.IsNullOrWhiteSpace(newImagePath))
            {
                await _mediaManager.DeleteFileAsync(author.ImageUrl);
                author.ImageUrl = newImagePath;
            }
        }

        await _authorRepository.AddAuthorAsync(author);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> VerifyAuthorSlug(
       int id, string urlSlug)
    {
        var slugExisted = await _authorRepository
            .IsAuthorSlugExistedAsync(id, urlSlug);

        return slugExisted
            ? Json($"Slug '{urlSlug}' đã được sử dụng")
            : Json(true);
    }
}
