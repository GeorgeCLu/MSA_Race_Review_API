using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA_Race_Review_API.Migrations
{
    public partial class SecondCreate8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "scoreSum",
                table: "Race",
                unicode: false,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalReviews",
                table: "Race",
                unicode: false,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scoreSum",
                table: "Race");

            migrationBuilder.DropColumn(
                name: "totalReviews",
                table: "Race");
        }
    }
}
