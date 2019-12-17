using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromotorApi.Model
{
    public class SlideShowItem
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SlideShowId { get; set; }
        public virtual SlideShow SlideShow { get; set; }
        public string Content { get; set; }
    }

    public class SlideShowItemEntityTypeConfiguration : IEntityTypeConfiguration<SlideShowItem>
    {
        public void Configure(EntityTypeBuilder<SlideShowItem> builder)
        {
            builder.ToTable("PromotorApi_SlideShowItem");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Order)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .HasDefaultValue(string.Empty);

            builder.Property(cb => cb.Description)
                .HasDefaultValue(string.Empty);

            builder.Property(cb => cb.Content)
                .IsRequired()
                .HasDefaultValue("<div><h1>Proszę uzupełnić treść slajdu</h1></div>");
        }
    }
}
