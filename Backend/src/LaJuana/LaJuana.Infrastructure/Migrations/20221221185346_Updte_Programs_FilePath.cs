using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaJuana.Infrastructure.Migrations
{
    public partial class Updte_Programs_FilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Programs",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Programs",
                newName: "FileName");
        }
    }
}
