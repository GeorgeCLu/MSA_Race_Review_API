using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA_Race_Review_API.Migrations
{
    public partial class SecondCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewId",
                table: "Race");

            migrationBuilder.AddColumn<int>(
                name: "raceId",
                table: "Review",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_raceId",
                table: "Review",
                column: "raceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Race_raceId",
                table: "Review",
                column: "raceId",
                principalTable: "Race",
                principalColumn: "raceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Race_raceId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_raceId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "raceId",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "reviewId",
                table: "Race",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
