using Microsoft.EntityFrameworkCore.Migrations;

namespace Squash.Data.Migrations
{
    public partial class isJogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJogo",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJogo",
                table: "Matches");
        } 
    }
}
