using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class PageCategoryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResellerId = table.Column<string>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    CatalogId = table.Column<Guid>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    ShippingAddress = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMasters_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderMasters_AspNetUsers_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderMasters_AspNetUsers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageItemCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageItemCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageItemCategorys_PageItemCategorys_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PageItemCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderTransactionss",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    OrderMasterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTransactionss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTransactionss_OrderMasters_OrderMasterId",
                        column: x => x.OrderMasterId,
                        principalTable: "OrderMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderAttachmentss",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    AttachmentType = table.Column<string>(nullable: true),
                    OrderTransactionsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAttachmentss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAttachmentss_OrderTransactionss_OrderTransactionsId",
                        column: x => x.OrderTransactionsId,
                        principalTable: "OrderTransactionss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAttachmentss_OrderTransactionsId",
                table: "OrderAttachmentss",
                column: "OrderTransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_CatalogId",
                table: "OrderMasters",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_ResellerId",
                table: "OrderMasters",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMasters_SupplierId",
                table: "OrderMasters",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransactionss_OrderMasterId",
                table: "OrderTransactionss",
                column: "OrderMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PageItemCategorys_ParentId",
                table: "PageItemCategorys",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAttachmentss");

            migrationBuilder.DropTable(
                name: "PageItemCategorys");

            migrationBuilder.DropTable(
                name: "OrderTransactionss");

            migrationBuilder.DropTable(
                name: "OrderMasters");
        }
    }
}
