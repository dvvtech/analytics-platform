
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Analytics.Api.DAL.Entities;

namespace Analytics.Api.DAL.EFConfigurations
{
    public class PageVisitConfiguration : IEntityTypeConfiguration<PageVisitEntity>
    {
        public void Configure(EntityTypeBuilder<PageVisitEntity> builder)
        {
            builder.ToTable("PageVisits");

            // Первичный ключ
            builder.HasKey(e => e.Id);

            // Автоинкремент
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            // Свойства            
            builder.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.OperatingSystem)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.Browser)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.Referrer)
                .IsRequired(false);

            builder.Property(e => e.PageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.VisitTime)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            // Индексы
            builder.HasIndex(e => e.VisitTime)
                .HasDatabaseName("idx_pagevisits_visittime")
                .IsDescending(true);

            builder.HasIndex(e => e.OperatingSystem)
                .HasDatabaseName("idx_pagevisits_os");

            builder.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("idx_pagevisits_createdat")
                .IsDescending(true);

            // Комментарии к таблице (PostgreSQL specific)
            builder.ToTable(t => t.HasComment("Таблица для хранения посещений страниц"));

            // Комментарии к полям            

            builder.Property(e => e.VisitTime)
                .HasComment("Время посещения в UTC");
        }
    }
}
