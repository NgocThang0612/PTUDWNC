﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace TatBlog.WebApp.Areas.Admin.Models;

public class CategoriesFilterModel
{
    [DisplayName("Từ khóa")]
    public string Keyword { get; set; }

    [DisplayName("Show on menu")]
    public bool ShowOnMenu { get; set; }
}
