using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockManagemnts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    QtyType = table.Column<string>(nullable: true),
                    RequiredStock = table.Column<int>(nullable: false),
                    CurrentStock = table.Column<int>(nullable: false),
                    ShopTag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockManagemnts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockManagemnts_StockCats_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "StockCats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockManagemnts_CategoryId",
                table: "StockManagemnts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockManagemnts");
        }
    }
}
