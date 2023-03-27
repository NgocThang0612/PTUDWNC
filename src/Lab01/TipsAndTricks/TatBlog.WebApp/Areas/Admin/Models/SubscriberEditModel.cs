using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TatBlog.WebApp.Areas.Admin.Models;

public class SubscriberEditModel
{
    
    public int Id { get; set; }

    public string Email { get; set; }

    public DateTime SubscribeDate { get; set; }

    [DisplayName("Ngày hủy")]
    public DateTime? UnsubscribeDate { get; set; }

    [DisplayName("Lý do")]
    public string ResonUnsubscribe { get; set; }

    [DisplayName("Trạng thái")]
    public bool? Voluntary { get; set; }

    [DisplayName("Nội dung")]
    public string Notes { get; set; }

   

}

