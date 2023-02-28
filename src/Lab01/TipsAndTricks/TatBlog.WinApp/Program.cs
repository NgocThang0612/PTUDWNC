using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

// Tạo với đối tượng DbContext để quản lý phiên làm việc
// với CSDL và trạng thái các đối tượng
var context = new BlogDBContext();

// Tạo đối tượng BlogRepository
IBlogRepository blogRepo = new BlogRepository(context);

// Tạo đối tượng khởi tạo dữ liệu mẫu
var seeder = new DataSeeder(context);

// Gọi hàm Initialize để nhập dữ liệu mẫu
seeder.Initialize();

// Đọc danh sách bài viết từ CSDL
// với CSDL và trạng thái của đối tượng
var posts = context.Posts
    .Where(p => p.Published)
    .OrderBy(p => p.Title)
    .Select(p => new
    {
        Id = p.Id,
        Title = p.Title,
        ViewCount = p.ViewCount,
        PostedDate = p.PostedDate,
        Author = p.Author.FullName,
        Category =p.Category.Name
    })
    .ToList();

// Xuất danh sách bài viết ra màn hình
foreach (var post in posts)
{
    Console.WriteLine("ID      : {0}", post.Id);
    Console.WriteLine("Title   : {0}", post.Title);
    Console.WriteLine("View    : {0}", post.ViewCount);
    Console.WriteLine("Date    : {0:MM/dd//yyyy}", post.PostedDate);
    Console.WriteLine("Author  : {0}", post.Author);
    Console.WriteLine("Category: {0}", post.Category);
    Console.WriteLine("".PadRight(80, '-'));
}    
    



// Đọc danh sách tác giả từ CSDL
var authors = context.Authors.ToList();

//Xuất danh sách tác giả ra màn hình
Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
    "ID", "FullName", "Email", "JoinedDate");


foreach (var author in authors)
{
    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}",
        author.Id, author.FullName, author.Email, author.JoinedDate);
}


// Tìm 3 bài viết được xem/đọc nhiều nhất
var postsP = await blogRepo.GetPopularArticlesAsync(3);

// Xuất danh sách bài viết ra màn hình
foreach(var post in postsP)
{
    Console.WriteLine("ID      : {0}", post.Id);
    Console.WriteLine("Title   : {0}", post.Title);
    Console.WriteLine("View    : {0}", post.ViewCount);
    Console.WriteLine("Date    : {0:MM/dd/yyyy}", post.PostedDate);
    Console.WriteLine("Author  : {0}", post.Author.FullName);
    Console.WriteLine("Category: {0}", post.Category.Name);
    Console.WriteLine("".PadRight(80, '-'));
}

// Lấy danh sách chuyên mục
var categories = await blogRepo.GetCategoriesAsync();

// Xuất ra màn hình
Console.WriteLine("{0,-5}{1,-50}{2,10}",
    "ID","Name","Count");


foreach (var item in categories)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}",
        item.Id, item.Name, item.PostCount);
}

// Tạo đối tượng chứa tham số phân trang
var pagingParams = new PagingParams
{
    PageNumber = 1,       // Lấy kết quả ở trang số 1
    PageSize = 5,         // Lấy 5 mẫu tin
    SortColumn = "Name",  // Sắp xếp theo tên
    SortOrder = "DESC",   // Theo chiều giảm dần
};

// Lấy danh sách từ khóa
var tagsList = await blogRepo.GetPagedTagsAsync(pagingParams);

// Xuất ra màn hinh
Console.WriteLine("{0,-5}{1,-50}{2,10}",
    "ID", "TagName", "Count");

foreach(var item in tagsList)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}",
        item.Id, item.Name, item.PostCount);
}    