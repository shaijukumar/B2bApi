using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageImages");

            migrationBuilder.CreateTable(
                name: "PagePhotos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    PageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagePhotos_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagePhotos_PageId",
                table: "PagePhotos",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagePhotos");

            migrationBuilder.CreateTable(
                name: "PageImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageImages_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageImages_PageId",
                table: "PageImages",
                column: "PageId");
        }
    }
}
