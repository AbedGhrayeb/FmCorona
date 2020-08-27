using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ShowTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_ShowTimes_ShowTimeId",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ShowTimeId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "RepatetShowTime",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StartigDate",
                table: "Programs");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "ShowTimes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_ProgramId",
                table: "ShowTimes",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Programs_ProgramId",
                table: "ShowTimes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Programs_ProgramId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_ProgramId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "ShowTimes");

            migrationBuilder.AddColumn<DateTime>(
                name: "RepatetShowTime",
                table: "ShowTimes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShowTimeId",
                table: "Programs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartigDate",
                table: "Programs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ShowTimeId",
                table: "Programs",
                column: "ShowTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_ShowTimes_ShowTimeId",
                table: "Programs",
                column: "ShowTimeId",
                principalTable: "ShowTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
