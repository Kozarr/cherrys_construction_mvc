using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cherrys_construction_mvc.Migrations
{
    /// <inheritdoc />
    public partial class testimonyModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testimonies_Projects_ProjectId",
                table: "Testimonies");

            migrationBuilder.DropIndex(
                name: "IX_Testimonies_ProjectId",
                table: "Testimonies");

            migrationBuilder.AddColumn<int>(
                name: "TestimonyId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestimonyId",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonies_ProjectId",
                table: "Testimonies",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonies_Projects_ProjectId",
                table: "Testimonies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
