using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_E_Commerce_Project.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TotalAmount",
                table: "Orders");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TotalAmount",
                table: "Orders",
                sql: "TotalAmount >= 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TotalAmount",
                table: "Orders");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TotalAmount",
                table: "Orders",
                sql: "TotalAmount > 0");
        }
    }
}
