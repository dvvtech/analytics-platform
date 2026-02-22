using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analytics.Api.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "OfftubeTech",
                type: "character varying(350)",
                maxLength: 350,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "OfftubeTech");
        }
    }
}
