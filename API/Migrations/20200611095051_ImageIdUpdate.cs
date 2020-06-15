using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class ImageIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "configid",
                table: "CategoryColores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "configid",
                table: "CategoryColores");
        }
    }
}
