using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Authors;

public interface IAuthorRepository
{
    //Câu 2. A : Tạo interface IAuthorRepository và lớp AuthorRepository. 
    //Câu 2. B : Tìm một tác giả theo mã số
    Task<Author> GetAuthorByIdAsync(int Id, CancellationToken cancellationToken = default);
    //Câu 2. C : Tìm một tác giả theo tên định danh (slug). 
    Task<Author> GetAuthorByUrlSlugAsync(string Slug, CancellationToken cancellationToken = default);
    //Câu 2. D : Lấy và phân trang danh sách tác giả kèm theo số lượng bài viết của tác giả
    //đó.Kết quả trả về kiểu IPagedList<AuthorItem>.
    Task<IPagedList<AuthorItem>> GetPagedAuthorAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);
    //Câu 2. E : Thêm hoặc cập nhật thông tin một tác giả.
    Task<bool> IsAuthorSlugExistedAsync(
            int id,
            string slug,
            CancellationToken cancellationToken = default);
    Task AddAuthorAsync(
        Author author,
        CancellationToken cancellationToken = default);
    //Câu 2. F : Tìm danh sách N tác giả có nhiều bài viết nhất. N là tham số đầu vào.
    Task<IList<AuthorItem>> ListAuthorAsync(int N, CancellationToken cancellationToken = default);
}
