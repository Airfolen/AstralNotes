using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AstralNotes.API.Migrations
{
    public partial class AddFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileGuid",
                table: "Notes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Extension = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileGuid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_FileGuid",
                table: "Notes",
                column: "FileGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Files_FileGuid",
                table: "Notes",
                column: "FileGuid",
                principalTable: "Files",
                principalColumn: "FileGuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Files_FileGuid",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Notes_FileGuid",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "FileGuid",
                table: "Notes");
        }
    }
}
