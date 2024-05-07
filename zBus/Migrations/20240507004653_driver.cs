using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zBus.Migrations
{
    public partial class driver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "Fisrt_name",
                table: "Drivers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_name",
                table: "Drivers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fisrt_name",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Last_name",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Drivers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
