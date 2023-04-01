using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations;

public class CategoryValidator : AbstractValidator<CategoryEditModel>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Tên chủ đề không được để trống")
            .MaximumLength(100)
            .WithMessage("Tên chủ đề tối đa 100 ký  tự");

        RuleFor(c => c.UrlSlug)
            .NotEmpty()
            .WithMessage("UrlSlug không được để trống")
            .MaximumLength(100)
            .WithMessage("UrlSlug tối đa 100 ký tự");

        RuleFor(c => c.Description)
            .MaximumLength(1000)
            .WithMessage("Nội dung tối đa 1000 ký  tự");

    }    
}
