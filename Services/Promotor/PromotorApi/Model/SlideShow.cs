using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromotorApi.Model
{
    public enum SlideShowStatusEnum
    {
        [Description("Ekran aktywny")]
        Active = 1,
        [Description("Ekran usunięty")]
        Deleted = 2
    }

    public class SlideShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual List<SlideShowItem> Slides { get; set; }

        public SlideShow()
        {
            Slides = new List<SlideShowItem>();
        }
    }

    public class SlideShowEntityTypeConfiguration : IEntityTypeConfiguration<SlideShow>
    {
        public void Configure(EntityTypeBuilder<SlideShow> builder)
        {
            builder.ToTable("PromotorApi_SlideShow");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(cb => cb.Description)
                .HasDefaultValue(string.Empty);

            builder.Property(cb => cb.ValidFrom)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(cb => cb.CreationDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.HasMany(x => x.Slides)
                .WithOne(x => x.SlideShow)
                .HasForeignKey(x => x.SlideShowId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue((int)SlideShowStatusEnum.Active);
        }
    }
}
