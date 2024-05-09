using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zBus.Migrations
{
    public partial class CascadeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_ArrivalStationID",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_DepartureStationID",
                table: "Trips");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_ArrivalStationID",
                table: "Trips",
                column: "ArrivalStationID",
                principalTable: "Stations",
                principalColumn: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_DepartureStationID",
                table: "Trips",
                column: "DepartureStationID",
                principalTable: "Stations",
                principalColumn: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_ArrivalStationID",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_DepartureStationID",
                table: "Trips");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_ArrivalStationID",
                table: "Trips",
                column: "ArrivalStationID",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_DepartureStationID",
                table: "Trips",
                column: "DepartureStationID",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
