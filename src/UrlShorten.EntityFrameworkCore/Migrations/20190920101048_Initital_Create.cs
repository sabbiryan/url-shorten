using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShorten.EntityFrameworkCore.Migrations
{
    public partial class Initital_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlMaps",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    DeviceInfo = table.Column<string>(nullable: true),
                    Identity = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShortenUrl = table.Column<string>(nullable: true),
                    RawUrl = table.Column<string>(nullable: true),
                    HitCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlMaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HitLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModificationTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    DeviceInfo = table.Column<string>(nullable: true),
                    UrlMapId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HitLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HitLogs_UrlMaps_UrlMapId",
                        column: x => x.UrlMapId,
                        principalTable: "UrlMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HitLogs_UrlMapId",
                table: "HitLogs",
                column: "UrlMapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HitLogs");

            migrationBuilder.DropTable(
                name: "UrlMaps");
        }
    }
}
