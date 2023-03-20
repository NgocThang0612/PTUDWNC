using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TatBlog.WebApp.Areas.Admin.Models;

public class CategoriesEditModel
{
    
    public int Id { get; set; }

    [DisplayName("Tên chủ đề")]
    public string Name { get; set; }


    [DisplayName("Tên định danh")]
    public string UrlSlug { get; set; }


    [DisplayName("Nội dung")]
    public string Description { get; set; }

    [DisplayName("Menu")]
    public bool ShowOnMenu { get; set; }

}

