using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevekoperProject.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperCompanies",
                columns: table => new
                {
                    DeveloperCompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeveloperCompanyName = table.Column<string>(maxLength: 30, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperCompanies", x => x.DeveloperCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlaceName = table.Column<string>(maxLength: 30, nullable: false),
                    DeveloperCompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_DeveloperCompanies_DeveloperCompanyId",
                        column: x => x.DeveloperCompanyId,
                        principalTable: "DeveloperCompanies",
                        principalColumn: "DeveloperCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildingName = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 80, nullable: false),
                    TotalFloor = table.Column<string>(maxLength: 30, nullable: false),
                    FlatSize = table.Column<string>(maxLength: 30, nullable: false),
                    FlatPrice = table.Column<decimal>(maxLength: 30, nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Buildings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ProjectId",
                table: "Buildings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeveloperCompanyId",
                table: "Projects",
                column: "DeveloperCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "DeveloperCompanies");
        }
    }
}
