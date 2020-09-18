using Microsoft.EntityFrameworkCore.Migrations;

namespace InMemoryCaching.API.Migrations
{
    public partial class CustomerEntityupdateZip_CodetoZipCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zip_code",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
