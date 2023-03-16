using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Constants;
using TatBlog.Services.Blogs;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // Action này xử lý HTTP request đến trang chủ của
        // ứng dụng web hoặc tìm kiếm bài viết theo từ khóa
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 5)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                Keyword = keyword
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(postList);

            
        }
        public async Task<IActionResult> Category(
            string slug = null,
            int pageNumber = 1,
            int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                CategorySlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(postList);

            
        }

        public async Task<IActionResult> Author(
            string slug = null,
            int pageNumber = 1,
            int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                AuthorSlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(postList);

            
        }

        public async Task<IActionResult> Tag(
            string slug = null,
            int pageNumber = 1,
            int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                TagSlug = slug
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(postList);
        }

        public async Task<IActionResult> Post(
            int year ,
            int month ,
            int day ,
            string slug = null,
            int pageNumber = 1,
            int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                TagSlug = slug,
                PostedYear = year ,
                PostedMonth= month ,
                PostedDay= day ,
                
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var post = await _blogRepository
                .GetPostAsync(year, month, slug);
            await _blogRepository.IncreaseViewCountAsync(post.Id);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(post);

        }

        public async Task<IActionResult> Archives(
            int year,
            int month,
            int pageNumber = 1,
            int pageSize = 2)
        {
            // Tạo đối tượng chứa các điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                // Chỉ lấy những bài viết có trạng thái Published
                PublishedOnly = true,

                // Tìm bài viết theo từ khóa
                PostedYear = year,
                PostedMonth = month,

            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository
                .GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại điều kiện truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            // Truyền danh sách bài viết vào View để render ra HTML
            return View(postList);

        }

        public IActionResult About()
            => View();

        public IActionResult Contact()
            => View();

        public IActionResult Rss()
            => Content("Nội dung sẽ được cập nhật");
    }
}
