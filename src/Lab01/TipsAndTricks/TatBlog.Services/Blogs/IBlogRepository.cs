﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Constants;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using static Azure.Core.HttpHeader;

namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{
    #region
    // B. Phần hướng dẫn
    // Tìm bài viết có tên định danh là "slug"
    // và được đăng vào tháng 'month' năm 'year'
    //Task<Post> GetPostAsync(
    //    int year,
    //    int month,
    //    string slug,
    //    CancellationToken cancellationToken = default);
    //// CancellationToken cho phép hủy liên kết giữa các luồng, các Task object

    //// Tìm top N bài viết phổ biến được nhiều người xem nhất
    //Task<IList<Post>> GetPopularArticlesAsync(
    //    int numPosts,
    //    CancellationToken cancellationToken= default);

    //// Kiểm tra xem tên định danh của bài viết đã có hay chưa
    //Task<bool> IsPostSlugExistedAsync(
    //    int postId, string slug,
    //    CancellationToken cancellationToken = default);

    //// Lấy danh sách chuyên mục và số lượng bài viết
    //// nằm thuộc từng chuyên mục/chủ đề
    //Task<IList<CategoryItem>> GetCategoriesAsync(
    //    bool showOnMenu = false,
    //    CancellationToken cancellationToken = default);

    //// Tăng số lượt xem của một bài viết
    //Task IncreaseViewCountAsync(
    //    int postId,
    //    CancellationToken cancellationToken = default);

    //// Lấy danh sách từ khóa/thẻ và phân trang theo
    //// các tham số pagingParams
    //Task<IPagedList<TagItem>> GetPagedTagsAsync(
    //    IPagingParams pagingParams,
    //    CancellationToken cancellationToken = default);

    #endregion

    #region
    // Phần C. Bài tập thực hành
    //Câu 1.A : Tìm một thẻ (Tag) theo tên định danh (Slug)
    Task<Tag> GetTagByUrlAsync(string slug, CancellationToken cancellationToken = default);

    //Câu 1.C : Lấy danh sách all các thẻ (Tag) kèm theo số bài viết chứa thẻ đó
    Task<IList<TagItem>> GetAllTagsByPostAsync(CancellationToken cancellationToken = default);

    //Câu 1.D : Xóa một thẻ theo mã cho trước
    Task DeleteTagByIdAsync(
        int TagId,
        CancellationToken cancellationtoken = default);

    //Câu 1.E : Tìm một chuyên mục (Category) theo tên định danh (slug)
    Task<Category> GetCategoryByUrlAsync(string slug, CancellationToken cancellationToken = default);

    //Câu 1.F : Tìm một chuyên mục theo mã số cho trước
    Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

    //Câu 1.G : Thêm hoặc cập nhật một chuyên mục/chủ đề
    Task AddCategoryAsync(
        Category category,
        CancellationToken cancellationToken = default);

    //Câu 1.H : Xóa một chuyên mục theo mã số cho trước
    Task DeleteCategoryByIdAsync(
        int CategoryId,
        CancellationToken cancellationToken = default);

    //Câu 1.I : Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
    Task<bool> IsExitedCategoryBySlugAsync(
        int categoriesId, string slug,
        CancellationToken cancellationToken = default);

    //Câu 1.J : Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu IPagedList<CategoryItem>
    Task<IPagedList<CategoryItem>> GetPagedCategoryAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);

    //Câu 1.K : Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào. Kết
    //quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số, Bài viết
    Task<IList<PostItem>> CountByPostAsync(
        int N,
        CancellationToken cancellationToken = default);

    //Câu 1.L : Tìm một bài viết theo mã số
    Task<Post> GetPostByIdAsync(int id, CancellationToken cancellationToken = default);

    //Câu 1.M : Thêm hay cập nhật một bài viết.
    Task AddPostAsync(
        Post post,
        CancellationToken cancellationToken = default);

    //Câu 1. N : Chuyển đổi trạng thái Published của bài viết
    Task SwitchPublishAsync(
        int Id,
        CancellationToken cancellationToken = default);

    //Câu 1. O : Lấy ngẫu nhiên N bài viết. N là tham số đầu vào
    Task<IList<Post>> RandomPostAsync(int N,CancellationToken cancellationToken = default);

    //Câu 1. P : Tạo lớp PostQuery để lưu trữ các điều kiện tìm kiếm bài viết. Chẳng hạn:
    //mã tác giả, mã chuyên mục, tên ký hiệu chuyên mục, năm/tháng đăng bài,
    //từ khóa, …

    //Câu 1. Q : Tìm tất cả bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối tượng
    //PostQuery(kết quả trả về kiểu IList<Post>)
    Task<IList<Post>> GetAllPostByPostQuery(
        PostQuery postquery, 
        CancellationToken cancellationToken = default);

    //Câu 1. R : Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối
    //tượng PostQuery.
    Task <int> CountPostByPostQuery(
        PostQuery postquery,
        CancellationToken cancellationToken = default);

    //Câu 1 . S : Tìm và phân trang các bài viết thỏa mãn điều kiện tìm kiếm được cho trong
    //đối tượng PostQuery(kết quả trả về kiểu IPagedList<Post>)
    Task<IPagedList<Post>> GetPagedPostByPostQueryAsync(
        IPagingParams pagingParams,
        CancellationToken cancellationToken = default);

    //Câu 1. T : Tương tự câu trên nhưng yêu cầu trả về kiểu IPagedList<T>. Trong đó T
    //là kiểu dữ liệu của đối tượng mới được tạo từ đối tượng Post.Hàm này có
    //thêm một đầu vào là Func<IQueryable<Post>, IQueryable<T>> mapper
    //để ánh xạ các đối tượng Post thành các đối tượng T theo yêu cầu.
    #endregion
    #region
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
    Task AddAuthorAsync(
        Author author,
        CancellationToken cancellationToken = default);
    //Câu 2. F : Tìm danh sách N tác giả có nhiều bài viết nhất. N là tham số đầu vào.
    Task<IList<Author>> ListAuthorAsync(int N, CancellationToken cancellationToken = default);
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

