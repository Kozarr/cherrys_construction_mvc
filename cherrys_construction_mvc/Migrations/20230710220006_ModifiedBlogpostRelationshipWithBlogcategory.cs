using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cherrys_construction_mvc.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedBlogpostRelationshipWithBlogcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostBlogCategory");

            migrationBuilder.AddColumn<int>(
                name: "BlogCategoryId",
                table: "BlogPosts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogCategoryId",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "BlogPostBlogCategory",
                columns: table => new
                {
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostBlogCategory", x => new { x.BlogPostId, x.BlogCategoryId });
                    table.ForeignKey(
                        name: "FK_BlogPostBlogCategory_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostBlogCategory_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostBlogCategory_BlogCategoryId",
                table: "BlogPostBlogCategory",
                column: "BlogCategoryId");
        }
    }
}
