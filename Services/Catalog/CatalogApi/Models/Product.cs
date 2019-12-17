using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string MarketingDescriptionSmall { get; set; }
        public string MarketingDescriptionHTML { get; set; }
        public bool Novelty { get; set; }
        public string EanCode { get; set; }
        public double Price { get; set; }
        public double PricePromotion { get; set; }
        public double StockQuantity { get; set; }
        public bool Imported { get; set; }
        public bool AvailableInShop { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductAttribute> Attributes { get; set; }
        public virtual ICollection<ProductCategory> Categories { get; set; }

        public Product()
        {
            Images = new List<ProductImage>();
            Attributes = new List<ProductAttribute>();
            Categories = new List<ProductCategory>();
        }
    }

    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("CatalogApi_Product");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(cb => cb.Name).IsRequired();
            builder.Property(cb => cb.Slug).IsRequired();
            builder.Property(ci => ci.ImageUrl).HasDefaultValue("");
            builder.Property(ci => ci.MarketingDescriptionHTML).HasDefaultValue("");
            builder.Property(ci => ci.MarketingDescriptionSmall).HasDefaultValue("");
            builder.Property(ci => ci.AvailableInShop).HasDefaultValue(true);
            builder.Property(ci => ci.Novelty).HasDefaultValue(false);
            builder.Property(ci => ci.Imported).HasDefaultValue(false);
            builder.Property(ci => ci.Price).HasDefaultValue(0);
            builder.Property(ci => ci.PricePromotion).HasDefaultValue(0);
            builder.Property(ci => ci.StockQuantity).HasDefaultValue(0);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Attributes)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Categories)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductPhotos)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductCategories)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductAttributes)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductStock)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductBestseller)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.HasMany(x => x.ProductColors)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
            //
            //            builder.Property(c => c.Imported)
            //                .IsRequired()
            //                .HasDefaultValue(true);
            //
            //            builder.HasMany(x => x.PosStockHistories)
            //                .WithOne(x => x.Product)
            //                .HasForeignKey(x => x.ProductId)
            //                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
