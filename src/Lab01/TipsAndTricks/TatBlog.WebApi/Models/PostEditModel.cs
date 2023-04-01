namespace TatBlog.WebApi.Models
{
    public class PostEditModel
    {
        public string Title { get; set; }
        public string NameAuthor { get; set; }
        public string UrlSlug { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
