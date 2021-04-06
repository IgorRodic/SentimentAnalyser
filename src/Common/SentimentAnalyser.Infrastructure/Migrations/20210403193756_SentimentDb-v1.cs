using Microsoft.EntityFrameworkCore.Migrations;

namespace SentimentAnalyser.Infrastructure.Migrations
{
    public partial class SentimentDbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sentiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SentimentScore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentiments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sentiments");
        }
    }
}
