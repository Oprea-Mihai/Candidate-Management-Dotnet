using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagement.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallAvailabilityStart = table.Column<TimeSpan>(type: "time", nullable: true),
                    CallAvailabilityEnd = table.Column<TimeSpan>(type: "time", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GitHubUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeTextComment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
