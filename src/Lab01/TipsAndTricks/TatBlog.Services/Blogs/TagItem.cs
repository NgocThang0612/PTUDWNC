namespace TatBlog.Services.Blogs
{
    public class TagItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int PostCount { get; set; }
        
    }
}