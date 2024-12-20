using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubProj.Server.Migrations
{
    public partial class AddFieldsToBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UsersAddedBooks");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "UsersAddedBooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "UsersAddedBooks");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UsersAddedBooks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
