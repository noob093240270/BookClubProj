using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubProj.Server.Migrations
{
    public partial class AddFieldUserIdToUsersAddedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersAddedBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersAddedBooks");
        }
    }
}
