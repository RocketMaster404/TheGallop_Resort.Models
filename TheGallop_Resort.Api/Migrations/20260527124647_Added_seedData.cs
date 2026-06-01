using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheGallop_Resort.Api.Migrations
{
    /// <inheritdoc />
    public partial class Added_seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "erik.johansson@email.com", "Erik", "Johansson", "0701234567" },
                    { 2, "anna.svensson@email.com", "Anna", "Svensson", "0709876543" }
                });

            migrationBuilder.InsertData(
                table: "RoomDetails",
                columns: new[] { "Id", "Beds", "MaxAdults", "MaxChildren" },
                values: new object[,]
                {
                    { 1, 1, 1, 0 },
                    { 2, 2, 2, 2 },
                    { 3, 3, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "GuestId", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 3598m },
                    { 2, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 3499m }
                });

            migrationBuilder.InsertData(
                table: "RoomCategories",
                columns: new[] { "Id", "CategoryPrice", "RoomDetailId", "Type" },
                values: new object[,]
                {
                    { 1, 999m, 1, 0 },
                    { 2, 1799m, 2, 1 },
                    { 3, 3499m, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomCategoryId", "RoomNr" },
                values: new object[,]
                {
                    { 1, 1, 1001 },
                    { 2, 1, 1002 },
                    { 3, 2, 1101 },
                    { 4, 2, 1102 },
                    { 5, 3, 1201 }
                });

            migrationBuilder.InsertData(
                table: "RoomReservations",
                columns: new[] { "Id", "Adults", "BookingId", "CheckIn", "CheckOut", "Children", "PricePerNight", "RoomId", "RoomStatus" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1799m, 3, 0 },
                    { 2, 2, 2, new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3499m, 5, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomReservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomDetailId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_RoomDetails_RoomDetailId",
                        column: x => x.RoomDetailId,
                        principalTable: "RoomDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_RoomDetailId",
                table: "Amenities",
                column: "RoomDetailId");
        }
    }
}
