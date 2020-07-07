using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class PagesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    URLTitle = table.Column<string>(nullable: true),
                    PageHtml = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_PageItemCategorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PageItemCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CategoryId",
                table: "Pages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_URLTitle",
                table: "Pages",
                column: "URLTitle",
                unique: true,
                filter: "[URLTitle] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
