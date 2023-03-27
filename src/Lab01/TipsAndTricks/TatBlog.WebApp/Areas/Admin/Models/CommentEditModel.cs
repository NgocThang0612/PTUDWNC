using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TatBlog.WebApp.Areas.Admin.Models;

public class CommentEditModel
{
    
    public int Id { get; set; }

    [DisplayName("Người bình luận")]
    public string FullName { get; set; }


    [DisplayName("Ngày bình luận")]
    public DateTime JoinedDate { get; set; }


    [DisplayName("Nội dung")]
    public string Description { get; set; }

    [DisplayName("Menu")]
    public bool Approved { get; set; }

}

