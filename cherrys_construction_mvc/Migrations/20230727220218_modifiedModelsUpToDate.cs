using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cherrys_construction_mvc.Migrations
{
    /// <inheritdoc />
    public partial class modifiedModelsUpToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleDescription",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ArticleTitle",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleDescription",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArticleTitle",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
