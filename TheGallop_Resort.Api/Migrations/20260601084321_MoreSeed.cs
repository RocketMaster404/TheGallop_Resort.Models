using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheGallop_Resort.Api.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Status", "TotalPrice" },
                values: new object[] { new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 6998m });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 3, "johan.karlsson@email.com", "Johan", "Karlsson", "0701112233" },
                    { 4, "emma.nilsson@email.com", "Emma", "Nilsson", "0702223344" },
                    { 5, "lucas.andersson@email.com", "Lucas", "Andersson", "0703334455" },
                    { 6, "maja.lindberg@email.com", "Maja", "Lindberg", "0704445566" },
                    { 7, "oscar.berg@email.com", "Oscar", "Berg", "0705556677" },
                    { 8, "sofia.holm@email.com", "Sofia", "Holm", "0706667788" },
                    { 9, "william.ekstrom@email.com", "William", "Ekström", "0707778899" },
                    { 10, "ella.fors@email.com", "Ella", "Fors", "0708889900" }
                });

            migrationBuilder.InsertData(
                table: "RoomDetails",
                columns: new[] { "Id", "Beds", "MaxAdults", "MaxChildren" },
                values: new object[,]
                {
                    { 4, 1, 2, 1 },
                    { 5, 2, 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "PricePerNight" },
                values: new object[] { new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1799m });

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut", "RoomStatus" },
                values: new object[] { new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomCategoryId", "RoomNr" },
                values: new object[] { 6, 3, 1202 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "GuestId", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1799m },
                    { 4, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, 3499m },
                    { 5, new DateTime(2025, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0, 999m },
                    { 6, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 0, 5397m },
                    { 7, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 1799m },
                    { 8, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 0, 3499m },
                    { 9, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 0, 6998m },
                    { 10, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2, 999m }
                });

            migrationBuilder.InsertData(
                table: "RoomCategories",
                columns: new[] { "Id", "CategoryPrice", "RoomDetailId", "Type" },
                values: new object[,]
                {
                    { 4, 1299m, 4, 0 },
                    { 5, 2199m, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "RoomReservations",
                columns: new[] { "Id", "Adults", "BookingId", "CheckIn", "CheckOut", "Children", "PricePerNight", "RoomId", "RoomStatus" },
                values: new object[,]
                {
                    { 3, 1, 3, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 999m, 1, 1 },
                    { 4, 2, 4, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3499m, 5, 1 },
                    { 5, 1, 5, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 999m, 2, 0 },
                    { 6, 2, 6, new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1799m, 4, 0 },
                    { 7, 2, 7, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1799m, 3, 2 },
                    { 8, 2, 8, new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3499m, 5, 0 },
                    { 9, 2, 9, new DateTime(2026, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3499m, 5, 0 },
                    { 10, 1, 10, new DateTime(2027, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 999m, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomCategoryId", "RoomNr" },
                values: new object[,]
                {
                    { 7, 4, 1301 },
                    { 8, 4, 1302 },
                    { 9, 5, 1401 },
                    { 10, 5, 1402 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RoomDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Status", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3699m });

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "CheckOut", "PricePerNight" },
                values: new object[] { new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1789m });

            migrationBuilder.UpdateData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckIn", "CheckOut", "RoomStatus" },
                values: new object[] { new DateTime(2026, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }
    }
}
