using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegSystem.Migrations
{
    public partial class Mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TimeTable",
                newName: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "TimeTable",
                newName: "Id");
        }
    }
}
