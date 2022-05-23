using Microsoft.EntityFrameworkCore.Migrations;

namespace MeowLearn.Data.Migrations
{
    public partial class AddMediaTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Name", "ThumbnailImagePath" },
                values: new object[] { 1, "Lecture", "/images/LectureImage.png" }
            );

            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Name", "ThumbnailImagePath" },
                values: new object[] { 2, "Video", "/images/VideoImage.png" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "MediaType", keyColumn: "Id", keyValue: 1);

            migrationBuilder.DeleteData(table: "MediaType", keyColumn: "Id", keyValue: 2);
        }
    }
}
