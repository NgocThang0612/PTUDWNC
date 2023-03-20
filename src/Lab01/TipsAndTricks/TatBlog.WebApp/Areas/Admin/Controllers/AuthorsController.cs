using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
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
}
