using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace PromotorApi.Model
{
    public class PromotorContext : DbContext
    {
        private readonly string connectionString;

        public PromotorContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }

        public PromotorContext(DbContextOptions<PromotorContext> options) : base(options)
        {
        }

        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenGroup> Groups { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }
        public DbSet<SlideShowItem> SlideShowItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PromotionScreenEntityTypeConfiguration());
            builder.ApplyConfiguration(new ScreenGroupEntityTypeConfiguration());
            builder.ApplyConfiguration(new SlideShowEntityTypeConfiguration());
            builder.ApplyConfiguration(new SlideShowItemEntityTypeConfiguration());
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(5, r => TimeSpan.FromSeconds(5))
                .Execute(() => Database.Migrate());
        }
    }
}
