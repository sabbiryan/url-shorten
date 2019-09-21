using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShorten.EntityFrameworkCore.Migrations
{
    public partial class Title_Description_Added_On_UrlMapEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UrlMaps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UrlMaps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UrlMaps");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UrlMaps");
        }
    }
}
