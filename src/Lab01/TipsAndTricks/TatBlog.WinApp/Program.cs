using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Encodings.Web;
using TatBlog.Core.Constants;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Authors;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// Tạo với đối tượng DbContext để quản lý phiên làm việc
// với CSDL và trạng thái các đối tượng
var context = new BlogDBContext();

// Tạo đối tượng BlogRepository
IBlogRepository blogRepo = new BlogRepository(context);

// Tạo đối tượng khởi tạo dữ liệu mẫu
var seeder = new DataSeeder(context);

// Gọi hàm Initialize để nhập dữ liệu mẫu
seeder.Initialize();

#region
// PHẦN B
// Đọc danh sách bài viết từ CSDL
// với CSDL và trạng thái của đối tượng
//var posts = context.Posts
//    .Where(p => p.Published)
//    .OrderBy(p => p.Title)
//    .Select(p => new
//    {
//        Id = p.Id,
//        Title = p.Title,
//        ViewCount = p.ViewCount,
//        PostedDate = p.PostedDate,
//        Author = p.Author.FullName,
//        Category = p.Category.Name
//    })
//    .ToList();

//Xuất danh sách bài viết ra màn hình
//foreach (var post in posts)
//{
//    Console.WriteLine("ID      : {0}", post.Id);
//    Console.WriteLine("Title   : {0}", post.Title);
//    Console.WriteLine("View    : {0}", post.ViewCount);
//    Console.WriteLine("Date    : {0:MM/dd//yyyy}", post.PostedDate);
//    Console.WriteLine("Author  : {0}", post.Author);
//    Console.WriteLine("Category: {0}", post.Category);
//    Console.WriteLine("".PadRight(80, '-'));
//}

//Đọc danh sách tác giả từ CSDL
//var authors = context.Authors.ToList();

//Xuất danh sách tác giả ra màn hình
//Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
//    "ID", "FullName", "Email", "JoinedDate");


//foreach (var author in authors)
//{
//    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyyy}",
//        author.Id, author.FullName, author.Email, author.JoinedDate);
//}

//Tìm 3 bài viết được xem/đọc nhiều nhất
//var postsP = await blogRepo.GetPopularArticlesAsync(3);

//Xuất danh sách bài viết ra màn hình
//foreach(var post in postsP)
//{
//    Console.WriteLine("ID      : {0}", post.Id);
//    Console.WriteLine("Title   : {0}", post.Title);
//    Console.WriteLine("View    : {0}", post.ViewCount);
//    Console.WriteLine("Date    : {0:MM/dd/yyyy}", post.PostedDate);
//    Console.WriteLine("Author  : {0}", post.Author.FullName);
//    Console.WriteLine("Category: {0}", post.Category.Name);
//    Console.WriteLine("".PadRight(80, '-'));
//}

//Lấy danh sách chuyên mục
//var categories = await blogRepo.GetCategoriesAsync();

//Xuất ra màn hình
//Console.WriteLine("{0,-5}{1,-50}{2,10}",
//    "ID","Name","Count");


//foreach (var item in categories)
//{
//    Console.WriteLine("{0,-5}{1,-50}{2,10}",
//        item.Id, item.Name, item.PostCount);

//}
//Console.WriteLine("".PadRight(80, '-'));

//Tạo đối tượng chứa tham số phân trang
//var pagingParams = new PagingParams
//                   {
//                       PageNumber = 1,       // Lấy kết quả ở trang số 1
//                       PageSize = 5,         // Lấy 5 mẫu tin
//                       SortColumn = "Name",  // Sắp xếp theo tên
//                       SortOrder = "DESC",   // Theo chiều giảm dần
//                   };

//Lấy danh sách từ khóa
//var tagsList = await blogRepo.GetPagedTagsAsync(pagingParams);

//Xuất ra màn hinh
//Console.WriteLine("{0,-5}{1,-50}{2,10}",
//    "ID", "TagName", "Count");

//foreach (var item in tagsList)
//{
//    Console.WriteLine("{0,-5}{1,-50}{2,10}",
//        item.Id, item.Name, item.PostCount);
//}
//Console.WriteLine("".PadRight(80, '-'));
#endregion
#region
// Phần C. Bài tập thực hànhq
async void Cau1a()
{
    //1.A.Tìm một thẻ(Tag) theo tên định danh(Slug)
var tags = await blogRepo.GetTagByUrlAsync("google");

    // Xuất ra màn hình
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
        "ID", "TagName", "Url", "Des");

    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
          tags.Id, tags.Name, tags.UrlSlug, tags.Description);
}

async void Cau1c()
{
    // 1.C. Lấy danh sách all các thẻ (Tag) kèm theo số bài viết chứa thẻ đó
    var tags = await blogRepo.GetAllTagsByPostAsync();
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
        "ID", "TagName", "Url", "Des");
    foreach (var tag in tags)
    {
        Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
          tag.Id, tag.Name, tag.UrlSlug, tag.Description);
    }
}

async void Cau1d()
{
    //Câu 1.D : Xóa một thẻ theo mã cho trước
    await blogRepo.DeleteTagByIdAsync(10);
    Console.ReadLine();
    var tagT = await blogRepo.GetAllTagsByPostAsync();
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
        "ID", "TagName", "Url", "Des");
    foreach (var tag in tagT)
    {
        Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
          tag.Id, tag.Name, tag.UrlSlug, tag.Description);
    }
}

async void Cau1e()
{
    //Câu 1.E : Tìm một chuyên mục (Category) theo tên định danh (slug)
    var categories = await blogRepo.GetCategoryByUrlAsync("OOP");
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
        "ID", "Name", "Slug", "Des");
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
          categories.Id, categories.Name, categories.UrlSlug, categories.Description);
}

async void Cau1f()
{
    //Câu 1.F : Tìm một chuyên mục theo mã số cho trước
    var categories = await blogRepo.GetCategoryByIdAsync(1);
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
        "ID", "Name", "Slug", "Des");
    Console.WriteLine("{0,-5}{1,-50}{2,-30}{3,-30}",
          categories.Id, categories.Name, categories.UrlSlug, categories.Description);
}

async void Cau1g()
{
    //Câu 1.G : Thêm hoặc cập nhật một chuyên mục/chủ đề
    Category category = new Category()
    {
        Id = -1,
        Name = "Thang",
        Description = "dangngocthang",
        UrlSlug = "ngoc-thang",
        ShowOnMenu = true
    };
    await blogRepo.AddCategoryAsync(category);
}

async void Cau1h()
{
    ////Câu 1.H : Xóa một chuyên mục theo mã số cho trước
    await blogRepo.DeleteCategoryByIdAsync(9);
}

async void Cau1i()
{
    //Câu 1.I : Kiểm tra tên định danh (slug) của một chuyên mục đã tồn tại hay chưa
    int id = 3;
    string slug = "";
    Console.WriteLine(await blogRepo.IsExitedCategoryBySlugAsync(id, slug));
}

async void Cau1j()
{
    //Câu 1.J : Lấy và phân trang danh sách chuyên mục, kết quả trả về kiểu IPagedList<CategoryItem>
    var pagingParams = new PagingParams()
    {
        PageNumber = 1,
        PageSize = 5,
        SortColumn = "Name",
        SortOrder = "DESC"
    };

    var categoriesList = await blogRepo.GetPagedCategoryAsync(pagingParams);
    Console.WriteLine("{0,-5}{1,-50}{2,10}",
        "ID", "Name", "Count");
    foreach (var item in categoriesList)
    {
        Console.WriteLine("{0,-5}{1,-50}{2,10}",
            item.Id, item.Name, item.PostCount);
    }
}


//Câu 1.K : Đếm số lượng bài viết trong N tháng gần nhất. N là tham số đầu vào. Kết
//quả là một danh sách các đối tượng chứa các thông tin sau: Năm, Tháng, Số, Bài viết

async void Cau1l()
{
    //Câu 1.L : Tìm một bài viết theo mã số
    int id = 3;
    Post post = await blogRepo.GetPostByIdAsync(id);
    Console.WriteLine("ID       :{0}", post.Id);
    Console.WriteLine("Title    :{0}", post.Title);
    Console.WriteLine("View     :{0}", post.ViewCount);
    Console.WriteLine("Date     :{0}:MM/dd/yyyy", post.PostedDate);
    Console.WriteLine("Author   :{0}", post.Author);
    Console.WriteLine("Category :{0}", post.Category);
    Console.WriteLine("Tags :{0}", post.Tags.Count);
    Console.WriteLine("".PadRight(80, '-'));
}

async void Cau1m()
{
    //Câu 1.M : Thêm hay cập nhật một bài viết.

}

async void Cau1n()
{
    //Câu 1. N : Chuyển đổi trạng thái Published của bài viết
    int Id = 3;
    await blogRepo.SwitchPublishAsync(Id);
}

async void Cau1o()
{
    //Câu 1. O : Lấy ngẫu nhiên N bài viết. N là tham số đầu vào
    int id = 3;
    var random = await blogRepo.RandomPostAsync(id);

    foreach (var post in random)
    {
        Console.WriteLine("ID       :{0}", post.Id);
        Console.WriteLine("Title    :{0}", post.Title);
        Console.WriteLine("View     :{0}", post.ViewCount);
        Console.WriteLine("Date     :{0}:MM/dd/yyyy", post.PostedDate);
        Console.WriteLine("Author   :{0}", post.Author);
        Console.WriteLine("Category :{0}", post.Category);
        Console.WriteLine("Tags :{0}", post.Tags.Count);
        Console.WriteLine("".PadRight(80, '-'));
    }
}


async void Cau1q()
{
    //Câu 1. Q : Tìm tất cả bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối tượng
    //PostQuery(kết quả trả về kiểu IList<Post>)
    PostQuery pq = new PostQuery()
    {
        AuthorId = 3
    };
    var posts = await blogRepo.GetAllPostByPostQuery(pq);

    foreach (var post in posts)
    {
        Console.WriteLine("ID       :{0}", post.Id);
        Console.WriteLine("Title    :{0}", post.Title);
        Console.WriteLine("View     :{0}", post.ViewCount);
        Console.WriteLine("Date     :{0}:MM/dd/yyyy", post.PostedDate);
        Console.WriteLine("Author   :{0}", post.Author);
        Console.WriteLine("Category :{0}", post.Category);
        //Console.WriteLine("Tags :{0}", post.Tags.Count);
        Console.WriteLine("".PadRight(80, '-'));
    }
}

async void Cau1r()
{
    //Câu 1. R : Đếm số lượng bài viết thỏa mãn điều kiện tìm kiếm được cho trong đối
    //tượng PostQuery.
    PostQuery pq = new PostQuery()
    {
        AuthorId = 3
    };
    int count = await blogRepo.CountPostByPostQuery(pq);
    Console.WriteLine("Count post: ", count);
}


//Phần 2:
//IAuthorRepository authorRepo = new AuthorRepository(context);
async void Cau2b()
{
    //Câu 2. B : Tìm một tác giả theo mã số

}
#endregion