using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InMemoryCaching.API.Migrations
{
    public partial class CustomerEntityupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "zip_code",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
