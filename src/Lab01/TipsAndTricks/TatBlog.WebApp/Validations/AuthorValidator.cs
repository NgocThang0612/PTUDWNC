using FluentValidation;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class AuthorValidator : AbstractValidator<AuthorEditModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorValidator(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Bạn phải thêm tiêu đề cho tác giả")
                .MaximumLength(500)
                .WithMessage("Tiêu đề quá dài !!!!!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bạn phải thêm nội dung cho tác giả");

            RuleFor(x => x.Notes)
                .NotEmpty()
                .WithMessage("Bạn phải thêm phiên bản cho tác giả")
                .MaximumLength(1000)
                .WithMessage("Ký tự quá dài !!!");

            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .WithMessage("Bạn phải thêm tên định danh cho tác  giả")
                .MaximumLength(1000)
                .WithMessage("Tên định danh quá dài !!!");

            RuleFor(x => x.UrlSlug)
                .MustAsync(async (authorModel, slug, cancellationToken) =>
                    !await _authorRepository.IsAuthorSlugExistedAsync(
                        authorModel.Id, slug, cancellationToken))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");


            When(x => x.Id <= 0, () =>
            {
                RuleFor(x => x.ImageFile)
                    .Must(x => x is { Length: > 0 })
                    .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
            })
            .Otherwise(() =>
            {
                RuleFor(x => x.ImageFile)
                    .MustAsync(SetImageIfNotExist)
                    .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
            });
        }

        // Kiểm tra xem bài viết đã có hình ảnh chưa.
        // Nếu chưa có, bắt buộc người dùng phải chọn file.
        private async Task<bool> SetImageIfNotExist(
            AuthorEditModel authorModel,
            IFormFile imageFile,
            CancellationToken cancellationToken)
        {
            // Lấy thông tin bài viết từ CSDL
            var author = await _authorRepository.GetAuthorByIdAsync(
                authorModel.Id, cancellationToken);

            // Nếu bài viết đã có hình ảnh => không bắt buộc chọn file
            if (!string.IsNullOrWhiteSpace(author?.ImageUrl))
                return true;

            // Ngược lại (bài viết chưa có hình ảnh), kiểm tra xem
            // người dùng đã chọn file hay chưa. Nếu chưa thì báo lỗi
            return imageFile is { Length: > 0 };
        }
    }
}
