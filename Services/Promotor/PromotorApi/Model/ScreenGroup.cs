using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromotorApi.Model
{
    public enum ScreenGroupStatusEnum
    {
        Active = 1,
        Deleted = 2
    }

    public class ScreenGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual List<Screen> Screens { get; set; }

        public ScreenGroup()
        {
            Screens = new List<Screen>();
        }
    }

    public class ScreenGroupEntityTypeConfiguration : IEntityTypeConfiguration<ScreenGroup>
    {
        public void Configure(EntityTypeBuilder<ScreenGroup> builder)
        {
            builder.ToTable("PromotorApi_ScreenGroup");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(cb => cb.Description)
                .HasDefaultValue(string.Empty);

            builder.HasMany(x => x.Screens)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue((int)ScreenGroupStatusEnum.Active);
        }
    }
}
