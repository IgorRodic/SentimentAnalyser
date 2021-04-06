using Microsoft.EntityFrameworkCore.Migrations;

namespace SentimentAnalyser.Infrastructure.Migrations
{
    public partial class SentimentDbv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sentiments",
                columns: new[] { "Id", "SentimentScore", "Word" },
                values: new object[,]
                {
                    { 1, 0.4f, "nice" },
                    { 2, 0.8f, "excellent" },
                    { 3, 0f, "modest" },
                    { 4, -0.8f, "horrible" },
                    { 5, -0.5f, "ugly" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sentiments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sentiments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sentiments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sentiments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sentiments",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}