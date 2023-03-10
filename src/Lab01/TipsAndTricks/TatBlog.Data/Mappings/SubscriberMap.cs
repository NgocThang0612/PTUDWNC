using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;

namespace TatBlog.Data.Mappings
{
    public class SubscriberMap : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("Subsribers");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.SubscribeDate)
                .HasColumnType("datetime");

            builder.Property(a => a.UnsubscribeDate)
                .HasColumnType("datetime");

            builder.Property(a => a.ResonUnsubscribe)
                .HasMaxLength(500);

            builder.Property(a => a.Voluntary);
                

            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            
        }
    }
}
    

    


