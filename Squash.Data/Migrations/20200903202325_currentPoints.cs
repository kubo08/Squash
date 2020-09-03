using Microsoft.EntityFrameworkCore.Migrations;

namespace Squash.Data.Migrations
{
    public partial class currentPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ActualPoints",
                table: "Players",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPoints",
                table: "Players");
        }
    }
}
