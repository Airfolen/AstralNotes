using Microsoft.EntityFrameworkCore.Migrations;

namespace AstralNotes.API.Migrations
{
    public partial class DeleteDiscriptionFromNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Notes",
                nullable: false,
                defaultValue: "");
        }
    }
}
