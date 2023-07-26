using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cherrys_construction_mvc.Migrations
{
    /// <inheritdoc />
    public partial class modifiedCompanyCertificateModelRemovedIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "CompanyCertificates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "CompanyCertificates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
