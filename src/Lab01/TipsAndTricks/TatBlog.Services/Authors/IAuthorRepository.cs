﻿using System;
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
    Task<Author> GetAuthorByIdAsync(int id, CancellationToken cancellationToken = default);
    //Câu 2. C : Tìm một tác giả theo tên định danh (slug). 
    Task<Author> GetAuthorByUrlSlugAsync(string Slug, CancellationToken cancellationToken = default);
    //Câu 2. D : Lấy và phân trang danh sách tác giả kèm theo số lượng bài viết của tác giả
    //đó.Kết quả trả về kiểu IPagedList<AuthorItem>.

    Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
        int pageSize = 1, int pageNumber = 5,
        string name = null,
        CancellationToken cancellationToken = default);

    Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPagedAuthorsAsync<T>(
         Func<IQueryable<Author>, IQueryable<T>> mapper,
         IPagingParams pagingParams,
         string name = null,
         CancellationToken cancellationToken = default);

    //Câu 2. E : Thêm hoặc cập nhật thông tin một tác giả.
    Task<bool> IsAuthorSlugExistedAsync(
            int id,
            string slug,
            CancellationToken cancellationToken = default);
    Task<Author> AddAuthorAsync(Author author, CancellationToken cancellationToken = default);
    //Câu 2. F : Tìm danh sách N tác giả có nhiều bài viết nhất. N là tham số đầu vào.
    Task<IList<AuthorItem>> ListAuthorAsync(int N, CancellationToken cancellationToken = default);

    //Câu 2 bla ble
    Task<IList<AuthorItem>> GetAuthorsAsync(
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthorByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

    Task<int> NumberOfAuthors(CancellationToken cancellationToken = default);

    Task<Author> GetCachedAuthorByIdAsync(int authorId);

    Task<bool> AddOrUpdateAsync(
        Author author, CancellationToken cancellationToken = default);

    Task<bool> SetImageUrlAsync(
        int authorId, string imageUrl,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthorAsync(
        int authorId, CancellationToken cancellationToken = default);
}
