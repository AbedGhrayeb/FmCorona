using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Modyfiy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Programs_ProgramId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_AppUserId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Programs_ProgramId",
                table: "ShowTimes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_ProgramId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_Records_AppUserId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "ShowTimes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FirstShowTime",
                table: "ShowTimes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeId",
                table: "ShowTimes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Records",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Records",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Episodes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_EpisodeId",
                table: "ShowTimes",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_AppUserId1",
                table: "Records",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Programs_ProgramId",
                table: "Episodes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_AppUserId1",
                table: "Records",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Episodes_EpisodeId",
                table: "ShowTimes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Programs_ProgramId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_AppUserId1",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Episodes_EpisodeId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_EpisodeId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_Records_AppUserId1",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Records");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FirstShowTime",
                table: "ShowTimes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Records",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Episodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Guest = table.Column<bool>(type: "bit", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_ProgramId",
                table: "ShowTimes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_AppUserId",
                table: "Records",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProgramId",
                table: "Schedules",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Programs_ProgramId",
                table: "Episodes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_AppUserId",
                table: "Records",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Programs_ProgramId",
                table: "ShowTimes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
