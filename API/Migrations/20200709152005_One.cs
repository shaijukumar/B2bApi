using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Details1 = table.Column<string>(nullable: true),
                    Details2 = table.Column<string>(nullable: true),
                    Details3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    SizeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
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
                name: "TestApps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestApps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPhoto",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    URLTitle = table.Column<string>(nullable: true),
                    PageHtml = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    PageStatus = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    PhotosId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserPhoto_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "UserPhoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    PageId = table.Column<Guid>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Catalogs_AspNetUsers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogPhotos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    CatalogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogPhotos_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryColores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    configid = table.Column<string>(nullable: true),
                    CatlogId = table.Column<Guid>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    CatalogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryColores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryColores_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategorySize",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    configid = table.Column<string>(nullable: true),
                    CatlogId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    CatalogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySize_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotosId",
                table: "AspNetUsers",
                column: "PhotosId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogPhotos_CatalogId",
                table: "CatalogPhotos",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_CategoryId",
                table: "Catalogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_SupplierId",
                table: "Catalogs",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryColores_CatalogId",
                table: "CategoryColores",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySize_CatalogId",
                table: "CategorySize",
                column: "CatalogId");

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
                name: "IX_PageImages_PageId",
                table: "PageImages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageItemCategorys_ParentId",
                table: "PageItemCategorys",
                column: "ParentId");

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
                name: "AppConfig");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CatalogPhotos");

            migrationBuilder.DropTable(
                name: "CategoryColores");

            migrationBuilder.DropTable(
                name: "CategorySize");

            migrationBuilder.DropTable(
                name: "OrderAttachmentss");

            migrationBuilder.DropTable(
                name: "PageImages");

            migrationBuilder.DropTable(
                name: "TestApps");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OrderTransactionss");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "OrderMasters");

            migrationBuilder.DropTable(
                name: "PageItemCategorys");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserPhoto");
        }
    }
}
