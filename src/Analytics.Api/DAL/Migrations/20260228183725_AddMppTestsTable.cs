using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Analytics.Api.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMppTestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MppTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Country = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    City = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    OperatingSystem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Browser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeviceType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Operation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    VisitTimeUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()", comment: "Время посещения в UTC")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MppTests", x => x.Id);
                },
                comment: "Таблица для хранения посещений страниц");

            migrationBuilder.CreateIndex(
                name: "idx_pagevisits_os3",
                table: "MppTests",
                column: "OperatingSystem");

            migrationBuilder.CreateIndex(
                name: "idx_pagevisits_visittime3",
                table: "MppTests",
                column: "VisitTimeUTC",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MppTests");
        }
    }
}
