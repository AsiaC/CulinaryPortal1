using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryPortal.API.Migrations
{
    public partial class CategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "RecipeIngredient",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RecipeIngredient",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
