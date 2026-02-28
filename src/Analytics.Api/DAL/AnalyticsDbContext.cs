using Analytics.Api.DAL.EFConfigurations;
using Analytics.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Api.DAL
{
    public class AnalyticsDbContext : DbContext
    {
        public DbSet<PageVisitEntity> PageVisits { get; set; }
        public DbSet<DailyStatEntity> DailyStats { get; set; }

        public DbSet<OfftubeTechEntity> OfftubeTech { get; set; }

        public DbSet<MppTestsEntity> MppTests { get; set; }

        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Регистрация конфигураций
            modelBuilder.ApplyConfiguration(new PageVisitConfiguration());
            modelBuilder.ApplyConfiguration(new DailyStatConfiguration());
            modelBuilder.ApplyConfiguration(new OfftubeTechConfiguration());
            modelBuilder.ApplyConfiguration(new MppTestsEntityConfiguration());

            base.OnModelCreating(modelBuilder);

            // Глобальные настройки для всех строковых свойств
            //modelBuilder.UseCollation("en_US.utf8");

            // Глобальная конфигурация для всех DateTime свойств
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entityType.GetProperties())
            //    {
            //        if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
            //        {
            //            property.SetColumnType("timestamp with time zone");
            //        }
            //    }
            //}
        }

    }
}
