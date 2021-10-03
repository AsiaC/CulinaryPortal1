using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryPortal.API.Migrations
{
    public partial class RemoveDescriptionFromPhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
