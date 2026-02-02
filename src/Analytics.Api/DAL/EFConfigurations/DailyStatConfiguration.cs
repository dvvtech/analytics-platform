using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Analytics.Api.DAL.Entities;

namespace Analytics.Api.DAL.EFConfigurations
{
    public class DailyStatConfiguration : IEntityTypeConfiguration<DailyStatEntity>
    {
        public void Configure(EntityTypeBuilder<DailyStatEntity> builder)
        {
            builder.ToTable("DailyStats");

            // Первичный ключ
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            // Свойства
            builder.Property(e => e.StatDate)
                .IsRequired()
                .HasColumnType("date")
                .HasComment("Дата статистики");

            builder.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsRequired(false)
                .HasComment("Код страны");

            builder.Property(e => e.OperatingSystem)
                .HasMaxLength(100)
                .IsRequired(false)
                .HasComment("Операционная система");

            builder.Property(e => e.VisitsCount)
                .IsRequired()
                .HasDefaultValue(0)
                .HasComment("Количество посещений");

            // Уникальный индекс для агрегации
            builder.HasIndex(e => new { e.StatDate, e.CountryCode, e.OperatingSystem })
                .IsUnique()
                .HasDatabaseName("uq_dailystats_date_country_os")
                .HasFilter("[CountryCode] IS NOT NULL AND [OperatingSystem] IS NOT NULL");

            // Индексы для быстрого поиска
            builder.HasIndex(e => e.StatDate)
                .HasDatabaseName("idx_dailystats_statdate")
                .IsDescending(true);

            builder.HasIndex(e => e.CountryCode)
                .HasDatabaseName("idx_dailystats_countrycode");

            // Конфигурация для отсутствующих значений
            builder.Property(e => e.CountryCode)
                .HasDefaultValue("UN");

            builder.Property(e => e.OperatingSystem)
                .HasDefaultValue("Unknown");

            // Комментарий к таблице
            builder.ToTable(t => t.HasComment("Агрегированная ежедневная статистика"));
        }
    }
}
