using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;

namespace TatBlog.Core.Entities;

public class Comment : IEntity
{
    // Mã tác giả bài viết
    public int Id { get; set; }

    // Tên tác giả
    public string FullName { get; set; }

    // Ngày bắt đầu
    public DateTime JoinedDate { get; set; }

    // Ghi chú 
    public string Description { get; set; }

    public bool Approved { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }

}
