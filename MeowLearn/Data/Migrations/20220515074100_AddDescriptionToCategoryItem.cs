using Microsoft.EntityFrameworkCore.Migrations;

namespace MeowLearn.Data.Migrations
{
    public partial class AddDescriptionToCategoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CategoryItem",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CategoryItem");
        }
    }
}
