using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Data.Mappings;

namespace TatBlog.Data.Contexts;

public class BlogDBContext : DbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Thay đổi chuỗi kết nối cho phù hợp
        //optionsBuilder.UseSqlServer(@"Server=LAPTOP-HRP2I52Q;Database=TatBlog;
        //     Trusted_Connection=True;MultipleActiveResultSets=true");

        optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-HRP2I52Q;Initial Catalog=TatBlog;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(Category).Assembly);
    }
}
