using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Analytics.Api.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    StatDate = table.Column<DateTime>(type: "date", nullable: false, comment: "Дата статистики"),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true, defaultValue: "UN", comment: "Код страны"),
                    OperatingSystem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValue: "Unknown", comment: "Операционная система"),
                    VisitsCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "Количество посещений")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStats", x => x.Id);
                },
                comment: "Агрегированная ежедневная статистика");

            migrationBuilder.CreateTable(
                name: "PageVisits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CountryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OperatingSystem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Browser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeviceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Referrer = table.Column<string>(type: "text", nullable: true),
                    PageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    VisitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Время посещения в UTC"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageVisits", x => x.Id);
                },
                comment: "Таблица для хранения посещений страниц");

            migrationBuilder.CreateIndex(
                name: "idx_dailystats_countrycode",
                table: "DailyStats",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "idx_dailystats_statdate",
                table: "DailyStats",
                column: "StatDate",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "uq_dailystats_date_country_os",
                table: "DailyStats",
                columns: new[] { "StatDate", "CountryCode", "OperatingSystem" },
                unique: true,
                filter: "\"CountryCode\" IS NOT NULL AND \"OperatingSystem\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "idx_pagevisits_createdat",
                table: "PageVisits",
                column: "CreatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "idx_pagevisits_os",
                table: "PageVisits",
                column: "OperatingSystem");

            migrationBuilder.CreateIndex(
                name: "idx_pagevisits_visittime",
                table: "PageVisits",
                column: "VisitTime",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStats");

            migrationBuilder.DropTable(
                name: "PageVisits");
        }
    }
}
