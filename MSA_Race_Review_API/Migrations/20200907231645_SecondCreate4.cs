using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA_Race_Review_API.Migrations
{
    public partial class SecondCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Race_Review_reviewId",
                table: "Race");

            migrationBuilder.DropIndex(
                name: "IX_Race_reviewId",
                table: "Race");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Race_reviewId",
                table: "Race",
                column: "reviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Race_Review_reviewId",
                table: "Race",
                column: "reviewId",
                principalTable: "Review",
                principalColumn: "reviewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
