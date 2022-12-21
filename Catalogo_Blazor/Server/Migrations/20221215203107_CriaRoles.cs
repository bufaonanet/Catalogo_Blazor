using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo_Blazor.Server.Migrations
{
    public partial class CriaRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1bd8a06d-537d-4e2e-8af2-591191dce828", "ed4b601f-752f-41f3-ab01-1c32f5e7501d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80106b96-71f8-4c68-b811-df44fee8013e", "343ed36f-6f2e-48a9-9c70-6bece084bb59", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bd8a06d-537d-4e2e-8af2-591191dce828");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80106b96-71f8-4c68-b811-df44fee8013e");
        }
    }
}
