using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PromotorApi.Model
{
    public enum PromotionScreenStatusEnum
    {
        Active = 1,
        Deleted = 2
    }

    public class Screen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public int? GroupId { get; set; }
        public virtual ScreenGroup Group { get; set; }
    }

    public class PromotionScreenEntityTypeConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> builder)
        {
            builder.ToTable("PromotorApi_Screen");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired();


            builder.Property(cb => cb.Address)
                .HasDefaultValue(string.Empty);

//            builder.HasMany(x => x.SubCategories)
//                .WithOne(x => x.ParentCategory)
//                .HasForeignKey(x => x.ParentCategoryId);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue((int)PromotionScreenStatusEnum.Active);
        }
    }
}
