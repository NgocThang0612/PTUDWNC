using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Core.DTO;
using TatBlog.Core.Contracts;
using TatBlog.Services.Extensions;
using TatBlog.Core.Constants;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace TatBlog.Services.Blogs;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDBContext _context;

    public BlogRepository(BlogDBContext context)
    {
        _context = context;
    }

    #region
    //// Tìm bài viết có tên định danh là 'slug'
    //// và được đăng vào tháng 'month' năm 'year'
    //public async Task<Post> GetPostAsync(
    //    int year,
    //    int month,
    //    string slug,
    //    CancellationToken cancellationToken = default)
    //{
    //    IQueryable<Post> postsQuery = _context.Set<Post>()
    //    .Include(x => x.Category)
    //    .Include(x => x.Author);

    //    if (year > 0)
    //    {
    //        postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);

    //    }

    //    if (month > 0)
    //    {
    //        postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);

    //    }

    //    if (!string.IsNullOrWhiteSpace(slug))
    //    {
    //        postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
    //    }

    //    return await postsQuery.FirstOrDefaultAsync(cancellationToken);

    //}

    //// Tìm top N bài viết phổ biến được nhiều người xem nhất
    public async Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .OrderByDescending(p => p.ViewCount)
            .Take(numPosts)
            .ToListAsync(cancellationToken);
    }

    // Kiểm tra xem tên định danh của bài viết đã có hay chưa
    public async Task<bool> IsPostSlugExistedAsync(
        int postId,
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .AnyAsync(x => x.Id != postId && x.UrlSlug == slug,
            cancellationToken);
    }

    //// Lấy danh sách chuyên mục và số lượng bài viết
    //// nằm thuộc từng chuyên mục/chủ đề
    public async Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Category> categories = _context.Set<Category>();

        if (showOnMenu)
        {
            categories = categories.Where(x => x.ShowOnMenu);
        }

        return await categories
            .OrderBy(x => x.Name)
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                ShowOnMenu = x.ShowOnMenu,
                PostCount = x.Posts.Count(p => p.Published)
            })
            .ToListAsync(cancellationToken);
    }

    //// Tăng số lượt xem của một bài viết
    public async Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
            .Where(x => x.Id == postId)
            .ExecuteUpdateAsync(p =>
            p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
            cancellationToken);
    }

    //// Lấy danh sách từ khóa/thẻ và phân trang theo
    //// các tham số pagingParams
    //public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
    //    IPagingParams pagingParams,
    //    CancellationToken cancellationToken = default)
    //{
    //    var tagQuery = _context.Set<Tag>()
    //        .Select(x => new TagItem()
    //        {
    //            Id = x.Id,
    //            Name = x.Name,
    //            UrlSlug = x.UrlSlug,
    //            Description = x.Description,
    //            PostCount = x.Posts.Count(p => p.Published)
    //        });
    //    return await tagQuery
    //        .ToPagedListAsync(pagingParams, cancellationToken);
    //}
    #endregion
    #region
    // C. Bài tập thực hành
    //Câu 1.A : Tìm một thẻ (Tag) theo tên định danh (slug)
    public async Task<Tag> GetTagByUrlAsync(
        string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Tag>()
            .Where(x => x.UrlSlug == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 1.C : Lấy danh sách all các thẻ (Tag) kèm theo số bài viết chứa thẻ đó
    public async Task<IList<TagItem>> GetAllTagsByPostAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<Tag> tags = _context.Set<Tag>();

        var query = tags
            .OrderBy(x => x.Name)
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published)
            });
            return await query.ToListAsync(cancellationToken);
    }

    //Câu 1.D : Xóa một thẻ theo mã cho trước
    public async Task DeleteTagByIdAsync(int TagId, CancellationToken cancellationtoken = default)
    {
        await _context.Database
               .ExecuteSqlRawAsync("DELETE FROM PostTag WHERE TagsId = " + TagId, cancellationtoken);
        await _context.Set<Tag>()
            .Where (x => x.Id == TagId)
            .ExecuteDeleteAsync(cancellationtoken);
    }

    //Câu 1.E : Tìm một chuyên mục (Category) theo tên định danh (slug)
    public async Task<Category> GetCategoryByUrlAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
           .Where(x => x.UrlSlug == slug)
           .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 1.F : Tìm một chuyên mục theo mã số cho trước
    public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 1.G : Thêm hoặc cập nhật một chuyên mục/chủ đề
    public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        //  tim danh sach cua category truoc viet trong if 
        // tìm id, tiep theo dùng .ExecuteUpdateAsync để cập nhật
       
        if (IsExitedCategoryBySlugAsync(category.Id, category.UrlSlug).Result)
        Console.WriteLine("Error: Exsited Slug");
        else

                if (category.Id > 0) // true: update || false: add
        {
            await _context.Set<Category>()
                  .Where(x => x.Id == category.Id)
                  .ExecuteUpdateAsync(c => c
                    .SetProperty(x => x.Name, category.Name)
                    .SetProperty(x => x.UrlSlug, category.UrlSlug)
                    .SetProperty(x => x.Description, category.Description)
                    .SetProperty(x => x.ShowOnMenu, category.ShowOnMenu)
                    .SetProperty(x => x.Posts, category.Posts), cancellationToken);
        }
        else
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }

    //Câu 1.H : Xóa một chuyên mục theo mã số cho trước
    public async Task DeleteCategoryByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        await _context.Database
               .ExecuteSqlRawAsync("DELETE FROM Posts WHERE CategoryId = " + categoryId, cancellationToken);
        await _context.Set<Category>()
            .Where(x => x.Id == categoryId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    //Câu 1.I : Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
    public async Task<bool> IsExitedCategoryBySlugAsync(int categoriesId, string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .AnyAsync(x => x.UrlSlug == slug && x.Id != categoriesId,
            cancellationToken);
    }

    //Câu 1.J : Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu IPagedList<CategoryItem>
    public async Task<IPagedList<CategoryItem>> GetPagedCategoryAsync(
        IPagingParams pagingParams, 
        CancellationToken cancellationToken = default)
    {
        var categoryQuery = _context.Set<Category>()
            .Select(x => new CategoryItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published),
                ShowOnMenu = x.ShowOnMenu,
            });
        return await categoryQuery
            .ToPagedListAsync(pagingParams, cancellationToken);
    }

    //Câu 1.K : Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào. Kết
    //quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số, Bài viết
    public async Task<IList<PostItem>> CountByPostAsync(int N, CancellationToken cancellationToken = default)
    {

        return await _context.Set<Post>()
            .GroupBy(x => new { x.PostedDate.Year, x.PostedDate.Month })
            .Select(g => new PostItem()
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                PostCount = g.Count(x => x.Published)
            })
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ToListAsync(cancellationToken);

    }

    //Câu 1.L : Tìm một bài viết theo mã số
    public async Task<Post> GetPostByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Post>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    //Câu 1.M : Thêm hay cập nhật một bài viết.
    public async Task AddPostAsync(Post post, CancellationToken cancellationToken = default)
    {
        if (IsPostSlugExistedAsync(post.Id, post.UrlSlug).Result)
            Console.WriteLine("Error: Exsited Slug");
        else

                if (post.Id > 0) // true: update || false: add
        {
            await _context.Set<Post>()
                  .Where(x => x.Id == post.Id)
                  .ExecuteUpdateAsync(c => c
                    .SetProperty(x => x.Title, post.Title)
                    .SetProperty(x => x.ShortDescription, post.ShortDescription)
                    .SetProperty(x => x.Description, post.Description)
                    .SetProperty(x => x.Meta, post.Meta)
                    .SetProperty(x => x.UrlSlug, post.UrlSlug)
                    .SetProperty(x => x.ImageUrl, post.ImageUrl)
                    .SetProperty(x => x.ViewCount, post.ViewCount)
                    .SetProperty(x => x.Published, post.Published)
                    .SetProperty(x => x.ModifiedDate, post.ModifiedDate)
                    .SetProperty(x => x.PostedDate, post.PostedDate)
                    .SetProperty(x => x.AuthorId, post.AuthorId)
                    .SetProperty(x => x.CategoryId, post.CategoryId)
                    .SetProperty(x => x.Tags, post.Tags), cancellationToken);
        }
        else
        {
            _context.Posts.AddRange(post);
            _context.SaveChanges();
        }
    }

    //Câu 1. N : Chuyển đổi trạng thái Published của bài viết
    public async Task SwitchPublishAsync(int Id, CancellationToken cancellationToken = default)
    {
        await _context.Set<Post>()
                .Where(p => p.Id == Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Published, p => !p.Published),
                cancellationToken);
    }

    //Câu 1. O : Lấy ngẫu nhiên N bài viết. N là tham số đầu vào
    public async Task<IList<Post>> RandomPostAsync(int N, CancellationToken cancellationToken = default)
    {
        return (IList<Post>)await _context.Set<Post>()
            .OrderBy(x => Guid.NewGuid())
            .Take(N).ToListAsync(cancellationToken);
    }

    //Câu 1. Q : Tìm tất cả bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối tượng
    //PostQuery(kết quả trả về kiểu IList<Post>)
    public async Task<IList<Post>> GetAllPostByPostQuery(PostQuery postquery, CancellationToken cancellationToken = default)
    {
        return await FilterPost(postquery)
            .ToListAsync(cancellationToken);
    }

    //Câu 1. R : Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối
    //tượng PostQuery.
    public async Task<int> CountPostByPostQuery(PostQuery postquery, CancellationToken cancellationToken = default)
    {
        return await FilterPost(postquery)
            .CountAsync(cancellationToken);
    }

    //Câu 1 . S : Tìm và phân trang các bài viết thỏa mãn điều kiện tìm kiếm được cho trong
    //đối tượng PostQuery(kết quả trả về kiểu IPagedList<Post>)
    public async Task<IPagedList<Post>> GetPagedPostsAsync(PostQuery pq, IPagingParams pagingParams,  CancellationToken cancellationToken = default)
    {
        return await FilterPost(pq)
                .ToPagedListAsync(pagingParams, cancellationToken);
    }
    public IQueryable<Post> FilterPost(PostQuery pq)
    {
        var query = _context.Set<Post>()
            .Include(c => c.Category)
            .Include(t => t.Tags)
            .Include(a => a.Author);
        return query
            .WhereIf(pq.AuthorId > 0, p => p.AuthorId == pq.AuthorId)
            .WhereIf(!string.IsNullOrWhiteSpace(pq.AuthorSlug), p => p.Author.UrlSlug.Contains(pq.AuthorSlug))
            .WhereIf(pq.PostId > 0, p => p.Id == pq.PostId)
            .WhereIf(pq.CategoryId > 0, p => p.CategoryId == pq.CategoryId)
            .WhereIf(!string.IsNullOrWhiteSpace(pq.CategorySlug), p => p.Category.UrlSlug == pq.CategorySlug)
            .WhereIf(pq.PostedYear > 0, p => p.PostedDate.Year == pq.PostedYear)
            .WhereIf(pq.PostedMonth > 0, p => p.PostedDate.Month == pq.PostedMonth)
            .WhereIf(pq.TagId > 0, p => p.Tags.Any(x => x.Id == pq.TagId))
            .WhereIf(!string.IsNullOrWhiteSpace(pq.TagSlug), p => p.UrlSlug == pq.TagSlug)
            .WhereIf(pq.PublishedOnly != null, p => p.Published == pq.PublishedOnly);

            
    }
    public async Task<IPagedList<Post>> GetPagedPostsAsync(
            PostQuery pq,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
    {
        return await FilterPost(pq)
            .ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Post.PostedDate), "DESC",
                cancellationToken);
    }
    //Câu 1. T : Tương tự câu trên nhưng yêu cầu trả về kiểu IPagedList<T>. Trong đó T
    //là kiểu dữ liệu của đối tượng mới được tạo từ đối tượng Post.Hàm này có
    //thêm một đầu vào là Func<IQueryable<Post>, IQueryable<T>> mapper
    //để ánh xạ các đối tượng Post thành các đối tượng T theo yêu cầu.


    #endregion

    #region
    
    #endregion
    #region
    //Câu 3. A : Tạo lớp Subscriber để lưu trữ Email của người đăng ký, ngày đăng ký,
    //ngày hủy theo dõi, lý do hủy, trường cờ báo cho biết người dùng tự hủy
    //theo dõi hay bị người quản trị ngăn chặn và ghi chú của người quản trị
    //website.

    //Câu 3. B : Tạo lớp SubscriberMap để cấu hình bảng và các cột được ánh xạ từ lớp
    //Subscriber

    //Câu 3. C : Bổ sung thêm thuộc tính vào lớp BlogDbContext và sử dụng EF Core
    //Migration để thêm bảng mới vào cơ sở dữ liệu

    //Câu 3. D : Tạo interface ISubscriberRepository và lớp SubscriberRepository

    //Câu 3. E : Định nghĩa các phương thức để thực hiện các công việc sau:
    //Đăng ký theo dõi: SubscribeAsync(email)
    //Hủy đăng ký: UnsubscribeAsync(email, reason, voluntary)
    //Phát triển ứng dụng Web nâng cao 2023
    //Lab 01 Trang 34
    // Chặn một người theo dõi: BlockSubscriberAsync(id, reason, notes)
    // Xóa một người theo dõi: DeleteSubscriberAsync(id)
    // Tìm người theo dõi bằng ID: GetSubscriberByIdAsync(id)
    // Tìm người theo dõi bằng email: GetSubscriberByEmailAsync(email)
    // Tìm danh sách người theo dõi theo nhiều tiêu chí khác nhau, kết quả
    //được phân trang: Task<IPagedList<Subscriber>>
    //SearchSubscribersAsync(pagingParams, keyword, unsubscribed,
    //involuntary). 
    #endregion
}
