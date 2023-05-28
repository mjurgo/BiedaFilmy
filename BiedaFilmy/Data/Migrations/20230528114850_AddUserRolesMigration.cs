using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiedaFilmy.Data.Migrations
{
    public partial class AddUserRolesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString(), "User", "USER" },
                    { Guid.NewGuid().ToString(), "Editor", "EDITOR" },
                    { Guid.NewGuid().ToString(), "Admin", "ADMIN" },
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Name", "NormalizedName" },
                keyValues: new object[,]
                {
                    { "User", "USER" },
                    { "Editor", "EDITOR" },
                    { "Admin", "ADMIN" },
                }
            );
        }
    }
}
