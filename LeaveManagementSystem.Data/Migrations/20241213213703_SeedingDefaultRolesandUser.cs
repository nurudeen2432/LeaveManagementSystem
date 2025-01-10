using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0550cad7-5e34-41f9-ae0a-47684276b90e", null, "Supervisor", "SUPERVISOR" },
                    { "3b14c17f-e1b5-4bfb-b690-59cf0b071031", null, "Administrator", "ADMINISTRATOR" },
                    { "58be2e73-bfb7-4ea1-93a3-77cd3fb709d4", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df4117f5-e8ba-4e41-81dc-497942b8d686", 0, "6572d82c-12e0-4949-ae3c-dd68cfd7a0de", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAELndhOOBpIqT2zhdB/2pIPpHJqt3ty9xEsRtKayKKGtMdvw1iWNTCiqPrBhD+Q8BVg==", null, false, "4d0fc9c7-c168-4711-98b5-913f2dbb63e0", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3b14c17f-e1b5-4bfb-b690-59cf0b071031", "df4117f5-e8ba-4e41-81dc-497942b8d686" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0550cad7-5e34-41f9-ae0a-47684276b90e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58be2e73-bfb7-4ea1-93a3-77cd3fb709d4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3b14c17f-e1b5-4bfb-b690-59cf0b071031", "df4117f5-e8ba-4e41-81dc-497942b8d686" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b14c17f-e1b5-4bfb-b690-59cf0b071031");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4117f5-e8ba-4e41-81dc-497942b8d686");
        }
    }
}
