using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGallop_Resort.Api.Migrations
{
    /// <inheritdoc />
    public partial class seedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalPrice",
                value: 3699m);

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PricePerNight",
                value: 1789m);

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalPrice",
                value: 3499m);

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PricePerNight",
                value: 1799m);

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
