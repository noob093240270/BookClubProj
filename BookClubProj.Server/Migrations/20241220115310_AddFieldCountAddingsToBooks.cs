using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubProj.Server.Migrations
{
    public partial class AddFieldCountAddingsToBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountAddings",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountAddings",
                table: "Books");
        }
    }
}
