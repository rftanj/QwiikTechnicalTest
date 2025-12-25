using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwikkTechnicalTest.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypeatAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateIndex(
                name: "ux_appointment_slot",
                table: "Appointments",
                columns: new[] { "AppointmentDate", "AppointmentTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_appointment_slot",
                table: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
