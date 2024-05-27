using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zBus.Migrations
{
    public partial class Updateorder8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripUser");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_User_Id",
                table: "Trips",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_User_Id",
                table: "Trips",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_User_Id",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_User_Id",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Trips");

            migrationBuilder.CreateTable(
                name: "TripUser",
                columns: table => new
                {
                    TripsTripId = table.Column<int>(type: "int", nullable: false),
                    UsersUser_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripUser", x => new { x.TripsTripId, x.UsersUser_Id });
                    table.ForeignKey(
                        name: "FK_TripUser_Trips_TripsTripId",
                        column: x => x.TripsTripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripUser_Users_UsersUser_Id",
                        column: x => x.UsersUser_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripUser_UsersUser_Id",
                table: "TripUser",
                column: "UsersUser_Id");
        }
    }
}
