using FluentValidation;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class SubscriberValidator : AbstractValidator<SubscriberEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public SubscriberValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.ResonUnsubscribe)
                .NotEmpty()
                .WithMessage("Bạn phải ghi lý do");

            RuleFor(x => x.Notes)
                .NotEmpty()
                .WithMessage("Bạn phải ghi nội dung");
 
        }

    }
}
