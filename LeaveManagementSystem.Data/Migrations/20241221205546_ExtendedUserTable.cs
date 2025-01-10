using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4117f5-e8ba-4e41-81dc-497942b8d686",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71839a4d-84cb-4549-8af0-6f040387759e", new DateOnly(1965, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAELAgqdmvPSe64GaXfZvzI/KL359dquRFJBc7lfj4h8hahFKAj3Mz/s2K5gg1eCgvKA==", "ca017db3-9cd4-48d5-a518-3b95e4a1a80c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4117f5-e8ba-4e41-81dc-497942b8d686",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6572d82c-12e0-4949-ae3c-dd68cfd7a0de", "AQAAAAIAAYagAAAAELndhOOBpIqT2zhdB/2pIPpHJqt3ty9xEsRtKayKKGtMdvw1iWNTCiqPrBhD+Q8BVg==", "4d0fc9c7-c168-4711-98b5-913f2dbb63e0" });
        }
    }
}
