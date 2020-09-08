using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA_Race_Review_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    reviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reviewText = table.Column<string>(maxLength: 255, nullable: false),
                    reviewScore = table.Column<int>(nullable: false),
                    reviewerName = table.Column<string>(maxLength: 20, nullable: false),
                    upvotes = table.Column<int>(nullable: false),
                    timeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.reviewId);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    raceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    raceName = table.Column<string>(nullable: false),
                    championship = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false),
                    track = table.Column<string>(nullable: false),
                    location = table.Column<string>(nullable: false),
                    averageScore = table.Column<int>(nullable: false),
                    reviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.raceId);
                    table.ForeignKey(
                        name: "FK_Race_Review_reviewId",
                        column: x => x.reviewId,
                        principalTable: "Review",
                        principalColumn: "reviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Race_reviewId",
                table: "Race",
                column: "reviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
