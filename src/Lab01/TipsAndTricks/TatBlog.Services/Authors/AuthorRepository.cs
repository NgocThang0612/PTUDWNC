using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Authors;

public class AuthorRepository : IAuthorRepository
{
    private readonly BlogDBContext _context;

    public AuthorRepository(BlogDBContext context)
    {
        _context = context;
    }
    //Câu 2. B : Tìm một tác giả theo mã số
    public async Task<Author> GetAuthorByIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
            .Where(t => t.Id == Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 2. C : Tìm một tác giả theo tên định danh (slug)
    public async Task<Author> GetAuthorByUrlSlugAsync(string Slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
            .Where(t => t.UrlSlug == Slug)
            .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 2. D : Lấy và phân trang danh sách tác giả kèm theo số lượng bài viết của tác giả
    //đó.Kết quả trả về kiểu IPagedList<AuthorItem>.
    public async Task<IPagedList<AuthorItem>> GetPagedAuthorAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        var authorQuery = _context.Set<Author>()
            .Select(x => new AuthorItem()
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlSlug = x.UrlSlug,
                ImageUrl = x.ImageUrl,
                JoinedDate = x.JoinedDate,
                Email = x.Email,
                Notes = x.Notes
            });
        return await authorQuery
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    //Câu 2. E : Thêm hoặc cập nhật thông tin một tác giả
    public async Task<bool> IsAuthorSlugExistedAsync(
            int id,
            string slug,
            CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
            .AnyAsync(x => x.Id != id && x.UrlSlug == slug,
            cancellationToken);
    }
    public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default)
    {
        if (IsAuthorSlugExistedAsync(author.Id, author.UrlSlug).Result)
            Console.WriteLine("Error: Exsited Slug");
        else

                if (author.Id > 0) // true: update || false: add
        {
            await _context.Set<Author>()
                  .Where(x => x.Id == author.Id)
                  .ExecuteUpdateAsync(c => c
                    .SetProperty(x => x.FullName, author.FullName)
                    .SetProperty(x => x.UrlSlug, author.UrlSlug)
                    .SetProperty(x => x.ImageUrl, author.ImageUrl)
                    .SetProperty(x => x.JoinedDate, author.JoinedDate)
                    .SetProperty(x => x.Email, author.Email)
                    .SetProperty(x => x.Notes, author.Notes)
                  .SetProperty(x => x.Posts, author.Posts),
                     cancellationToken);
        }
        else
        {
            _context.Authors.AddRange(author);
            _context.SaveChanges();
        }
    }

    //Câu 2. F : Tìm danh sách N tác giả có nhiều bài viết nhất. N là tham số đầu vào.
    public async Task<IList<AuthorItem>> ListAuthorAsync(int N, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
            .Select(x => new AuthorItem()
            {
                Id = x.Id,
                FullName = x.FullName,
                UrlSlug = x.UrlSlug,
                ImageUrl = x.ImageUrl,
                JoinedDate = x.JoinedDate,
                Email = x.Email,
                Notes = x.Notes,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .OrderByDescending(x => x.PostCount)
            .Take(N)
            .ToListAsync(cancellationToken);
    }

    //Câu 2 bla ble
    public async Task<IList<AuthorItem>> GetAuthorsAsync(
            CancellationToken cancellationToken = default)
    {
        return await _context.Set<Author>()
            .OrderBy(a => a.FullName)
            .Select(a => new AuthorItem()
            {
                Id = a.Id,
                FullName = a.FullName,
                Email = a.Email,
                JoinedDate = a.JoinedDate,
                ImageUrl = a.ImageUrl,
                UrlSlug = a.UrlSlug,
                PostCount = a.Posts.Count(P => P.Published)
            })
            .ToListAsync(cancellationToken);
    }

    
}
