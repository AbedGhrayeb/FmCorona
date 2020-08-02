using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Modify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Categories_CategoryId",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Programs_CategoryId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Presenters");

            migrationBuilder.AddColumn<int>(
                name: "DefaultDuration",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Programs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Presenters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Presenters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Presenters",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDate",
                table: "Episodes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Episodes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Guest",
                table: "Episodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Episodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Episodes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Artists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Articals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Articals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropColumn(
                name: "DefaultDuration",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Presenters");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Presenters");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Presenters");

            migrationBuilder.DropColumn(
                name: "Guest",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Articals");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Articals");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Programs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Presenters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShowDate",
                table: "Episodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Episodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArticalId = table.Column<int>(type: "int", nullable: true),
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PresenterId = table.Column<int>(type: "int", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Articals_ArticalId",
                        column: x => x.ArticalId,
                        principalTable: "Articals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Presenters_PresenterId",
                        column: x => x.PresenterId,
                        principalTable: "Presenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CategoryId",
                table: "Programs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ArticalId",
                table: "Photos",
                column: "ArticalId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ArtistId",
                table: "Photos",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PresenterId",
                table: "Photos",
                column: "PresenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProgramId",
                table: "Photos",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Categories_CategoryId",
                table: "Programs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
