using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cherrys_construction_mvc.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ServiceTypes_ServiceTypeId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ServiceTypes_ServiceTypeId",
                table: "Projects",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ServiceTypes_ServiceTypeId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ServiceTypes_ServiceTypeId",
                table: "Projects",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id");
        }
    }
}
