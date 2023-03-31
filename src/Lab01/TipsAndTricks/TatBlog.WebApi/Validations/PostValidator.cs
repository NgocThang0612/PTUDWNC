using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations;

public class PostValidator : AbstractValidator<PostEditModel>
{
    public PostValidator() 
    {
        RuleFor(a => a.Title)
            .NotEmpty()
            .WithMessage("Tiêu đề không được để trống")
            .MaximumLength(100)
            .WithMessage("Tiêu đề tối đa 100 ký tự");

        RuleFor(a => a.UrlSlug)
            .NotEmpty()
            .WithMessage("UrlSlug không được để trống")
            .MaximumLength(100)
            .WithMessage("UrlSlug tối đa 100 ký tự");

        RuleFor(a => a.PostedDate)
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Ngày đăng bài không hợp lệ");
            

        RuleFor(a => a.NameAuthor)
            .NotEmpty()
            .WithMessage("Tên tác giả không được để trống")
            .MaximumLength(100)
            .WithMessage("Tên tác giả tối đa 100 ký tự");

        RuleFor(a => a.ShortDescription)
            .MaximumLength(500)
            .WithMessage("Ghi chú tối đa 500 ký tự");
    }
}
