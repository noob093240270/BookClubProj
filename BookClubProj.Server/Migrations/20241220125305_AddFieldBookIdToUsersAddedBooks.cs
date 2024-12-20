using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubProj.Server.Migrations
{
    public partial class AddFieldBookIdToUsersAddedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UsersAddedBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UsersAddedBooks");
        }
    }
}
