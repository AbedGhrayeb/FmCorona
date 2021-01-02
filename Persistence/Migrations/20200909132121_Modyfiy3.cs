using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Modyfiy3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Schedules_ScheduleId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_ScheduleId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Episodes");

            migrationBuilder.AddColumn<bool>(
                name: "Guest",
                table: "Schedules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Schedules",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProgramId",
                table: "Schedules",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Programs_ProgramId",
                table: "Schedules",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Programs_ProgramId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ProgramId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Guest",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Episodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_ScheduleId",
                table: "Episodes",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Schedules_ScheduleId",
                table: "Episodes",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
