using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TatBlog.Core.Entities;

namespace TatBlog.Data.Mappings;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure (EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(a => a.Approved)
            .HasDefaultValue(false)
           .IsRequired();

        builder.Property(a => a.JoinedDate)
            .HasColumnType("datetime");

        builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.PostId)
                .HasConstraintName("FK_Comments_Posts")
                .OnDelete(DeleteBehavior.Cascade);

    }
}
