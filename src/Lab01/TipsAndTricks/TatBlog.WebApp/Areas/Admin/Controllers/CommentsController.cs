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

public class CommentsController : Controller
{
    private readonly ILogger<CommentsController> _logger;
    private readonly IBlogRepository _blogRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMediaManager _mediaManager;
    private readonly IMapper _mapper;

    public CommentsController(
        ILogger<CommentsController> logger,
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

        
        var model = await _blogRepository.GetPagedCommentAsync(pageNumber, pageSize);
        return View(model);
    }

    public async Task<IActionResult> DeleteComment(int id = 0)
    {
        await _blogRepository.DeleteCommentByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ToggleApproved(int id = 0)
    {
        await _blogRepository.ToggleApprovedAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id = 0)
    {
        //ID = 0 <=> Thêm bài viết mới
        //ID > 0 : Đọc dữ liệu của bài viết từ CSDL
        var comment = id > 0
            ? await _blogRepository.GetCommentByIdAsync(id)
            : null;

        //Tạo view model từ dữ liệu của bài viết
        var model = comment == null
            ? new CommentEditModel()
            : _mapper.Map<CommentEditModel>(comment);

        //Gán các giá trị khác cho view model


        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        [FromServices] IValidator<CommentEditModel> commentValidator,
        CommentEditModel model)
    {
        var validationResult = await commentValidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
        }


        var comment = model.Id > 0
            ? await _blogRepository.GetCommentByIdAsync(model.Id)
            : null;

        if (comment == null)
        {
            comment = _mapper.Map<Comment>(model);

            comment.Id = 0;
        }
        else
        {
            _mapper.Map(model, comment);

        }

        await _blogRepository.CreateOrUpdateCommentAsync(
            comment);

        return RedirectToAction(nameof(Index));
    }
}
