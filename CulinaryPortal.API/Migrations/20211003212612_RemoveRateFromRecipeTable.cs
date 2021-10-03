using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryPortal.API.Migrations
{
    public partial class RemoveRateFromRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
