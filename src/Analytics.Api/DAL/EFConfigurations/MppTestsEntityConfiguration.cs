using Analytics.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Api.DAL.EFConfigurations
{
    public class MppTestsEntityConfiguration : IEntityTypeConfiguration<MppTestsEntity>
    {
        public void Configure(EntityTypeBuilder<MppTestsEntity> builder)
        {
            builder.ToTable("MppTests");

            // Первичный ключ
            builder.HasKey(e => e.Id);

            // Автоинкремент
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            // Свойства            
            builder.Property(e => e.Country)
                .HasMaxLength(120)
                .IsRequired(false);

            builder.Property(e => e.City)
                .HasMaxLength(120)
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

            builder.Property(e => e.Operation)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.VisitTimeUTC)
                .IsRequired()
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();

            // Индексы
            builder.HasIndex(e => e.VisitTimeUTC)
                .HasDatabaseName("idx_pagevisits_visittime3")
                .IsDescending(true);

            builder.HasIndex(e => e.OperatingSystem)
                .HasDatabaseName("idx_pagevisits_os3");

            // Комментарии к таблице (PostgreSQL specific)
            builder.ToTable(t => t.HasComment("Таблица для хранения посещений страниц"));

            // Комментарии к полям            
            builder.Property(e => e.VisitTimeUTC)
                .HasComment("Время посещения в UTC");
        }
    }
}
