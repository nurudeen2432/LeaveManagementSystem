using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "359b9788-3655-409b-b575-8746486057c7", "AQAAAAIAAYagAAAAEBGvYoCYK4WLsEJTn5OZHQjf/IQ4iRSS7eLkDZnsdanjUPZmqsu8AMFvTFKlumNzgw==", "b41e51f3-8a2f-4fe2-a4b4-e236321e64d2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5cd705f1-1a96-40f8-921b-3103a3691ffe", "AQAAAAIAAYagAAAAEGCLR4R8tRFdgOMzgaHSzF8MAIIzfcfZRZl8O8sc+qgL+IdrRO891+G3JWVA4IcRNA==", "cc4dabf8-1dc0-4181-a218-ba7edb5abcef" });
        }
    }
}
