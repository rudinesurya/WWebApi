using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWebApi.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_Sensors_SensorId",
                table: "WeatherData");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorId",
                table: "WeatherData",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_Sensors_SensorId",
                table: "WeatherData",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherData_Sensors_SensorId",
                table: "WeatherData");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorId",
                table: "WeatherData",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherData_Sensors_SensorId",
                table: "WeatherData",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id");
        }
    }
}
