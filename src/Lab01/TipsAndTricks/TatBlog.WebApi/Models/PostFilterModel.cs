using TatBlog.Core.Constants;
using TatBlog.Core.Entities;

namespace TatBlog.WebApi.Models;

public class PostFilterModel : PagingModel
{
    public string Title { get; set; }
}
