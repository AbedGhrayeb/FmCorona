using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Modify3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Titile",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultDuration",
                table: "Programs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShowTimeId",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Episodes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(nullable: false),
                    Guest = table.Column<bool>(nullable: false),
                    GuestName = table.Column<string>(nullable: true),
                    ProgramId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    PresenterId = table.Column<int>(nullable: true),
                    ProgramId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Presenters_PresenterId",
                        column: x => x.PresenterId,
                        principalTable: "Presenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ShowTimeId",
                table: "Programs",
                column: "ShowTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProgramId",
                table: "Schedules",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_PresenterId",
                table: "SocialMedias",
                column: "PresenterId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_ProgramId",
                table: "SocialMedias",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_ShowTimes_ShowTimeId",
                table: "Programs",
                column: "ShowTimeId",
                principalTable: "ShowTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_ShowTimes_ShowTimeId",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ShowTimeId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Episodes");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "ShowTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DefaultDuration",
                table: "Programs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Titile",
                table: "Episodes",
                type: "nvarchar(max)",
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
    }
}
