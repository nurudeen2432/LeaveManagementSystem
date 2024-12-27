using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveAllocation_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4117f5-e8ba-4e41-81dc-497942b8d686",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eaceef4-9e55-4147-86eb-3b833ecb9e4c", "AQAAAAIAAYagAAAAEJJQQm1/wzaEQulmvLQ0CHjEzC16zEergckC7LDdkM7xMQrfurqQ7N1a1u80BSp8LA==", "9b8d2ac7-a5e6-4041-b430-96550e3bd145" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df4117f5-e8ba-4e41-81dc-497942b8d686",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71839a4d-84cb-4549-8af0-6f040387759e", "AQAAAAIAAYagAAAAELAgqdmvPSe64GaXfZvzI/KL359dquRFJBc7lfj4h8hahFKAj3Mz/s2K5gg1eCgvKA==", "ca017db3-9cd4-48d5-a518-3b95e4a1a80c" });
        }
    }
}
