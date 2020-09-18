using Microsoft.EntityFrameworkCore.Migrations;

namespace InMemoryCaching.API.Migrations
{
    public partial class CustomerEntityupdatefieldnametocapital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "street",
                table: "Customers",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Customers",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Customers",
                newName: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Customers",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Customers",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Customers",
                newName: "city");
        }
    }
}
