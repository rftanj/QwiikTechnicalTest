using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwikkTechnicalTest.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Appointments",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "enum('Scheduled','Completed','Cancelled')",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Scheduled','InProgress','Completed','Cancelled')")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Appointments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "enum('Scheduled','InProgress','Completed','Cancelled')",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Scheduled','Completed','Cancelled')")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
