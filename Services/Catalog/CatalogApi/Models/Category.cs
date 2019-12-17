using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public int ExternalId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Path { get; set; }
        public int ProductCounter { get; set; }
        public bool IsVisible { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }

        public Category()
        {
            SubCategories = new HashSet<Category>();
            ProductCategories = new List<ProductCategory>();
        }

        public List<int> GetPathAsIntArray()
        {
            var path = new List<int>();

            if (string.IsNullOrEmpty(Path))
                return path;

            var splitPath = Path.Split('-');

            if (!splitPath.Any())
                return path;

            foreach (var s in splitPath)
            {
                try
                {
                    path.Add(Convert.ToInt32(s));
                }
                catch
                {

                }
            }

            return path;
        }
    }

    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CatalogApi_Category");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired();

            builder.Property(cb => cb.ProductCounter)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(cb => cb.Path)
                .HasDefaultValue(string.Empty);

            builder.HasMany(x => x.SubCategories)
                .WithOne(x => x.ParentCategory)
                .HasForeignKey(x => x.ParentCategoryId);

            builder.Property(c => c.IsVisible)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
