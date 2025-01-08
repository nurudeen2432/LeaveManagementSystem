using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LeaveRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveRequestStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestComment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveRequestStatuses_LeaveRequestStatusId",
                        column: x => x.LeaveRequestStatusId,
                        principalTable: "LeaveRequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6010b61c-2a36-4ef7-a446-a306e25c175f", null, "Employee", "EMPLOYEE" },
                    { "8fe08bf0-d35f-4ee2-828e-b07041a26940", null, "Supervisor", "SUPERVISOR" },
                    { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4969fc6f-c322-46ab-bf8b-49da83b718b9", 0, "5cd705f1-1a96-40f8-921b-3103a3691ffe", new DateOnly(1964, 12, 1), "admin@localhost.com", true, "Default", "admin", false, null, "ADMIN@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAEGCLR4R8tRFdgOMzgaHSzF8MAIIzfcfZRZl8O8sc+qgL+IdrRO891+G3JWVA4IcRNA==", null, false, "cc4dabf8-1dc0-4181-a218-ba7edb5abcef", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "LeaveRequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2caff07e-1485-4603-813d-839098f2cc62"), "Pending" },
                    { new Guid("388d0617-2de5-49a2-9408-f9f5819b0d5a"), "Declined" },
                    { new Guid("a07fcf48-e449-4638-94ea-07be6c232f55"), "Approved" },
                    { new Guid("c565c527-ff11-41ef-af5d-6f1d66ba3418"), "Canceled" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", "4969fc6f-c322-46ab-bf8b-49da83b718b9" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveRequestStatusId",
                table: "LeaveRequests",
                column: "LeaveRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ReviewerId",
                table: "LeaveRequests",
                column: "ReviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "LeaveRequestStatuses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6010b61c-2a36-4ef7-a446-a306e25c175f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fe08bf0-d35f-4ee2-828e-b07041a26940");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e", "4969fc6f-c322-46ab-bf8b-49da83b718b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8b48d34-ea1b-4cfc-82c5-fbebb938b92e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4969fc6f-c322-46ab-bf8b-49da83b718b9");

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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df4117f5-e8ba-4e41-81dc-497942b8d686", 0, "dc1eee11-6fbc-4ab0-a5e6-86bb73ca7e32", new DateOnly(1965, 12, 1), "admin@localhost.com", true, "Default", "Admin", false, null, "ADMIN@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAEGibIUxpZpczSwj5nsWFzhsp9YYb6RX5F0NE934NbiCy4awYlapLxMTa54XzT0+zfA==", null, false, "c8e24426-d5e6-46e5-8532-f11080577c40", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3b14c17f-e1b5-4bfb-b690-59cf0b071031", "df4117f5-e8ba-4e41-81dc-497942b8d686" });
        }
    }
}
