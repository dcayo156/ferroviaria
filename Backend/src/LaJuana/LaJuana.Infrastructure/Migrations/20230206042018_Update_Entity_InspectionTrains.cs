using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaJuana.Infrastructure.Migrations
{
    public partial class Update_Entity_InspectionTrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AspectoTecnicosieteObservacion",
                table: "InspectionTrains",
                newName: "AspectoTecnicoSieteObservacion");

            migrationBuilder.RenameColumn(
                name: "AspectoTecnicosieteNo",
                table: "InspectionTrains",
                newName: "AspectoTecnicoSieteNo");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "InspectionTrains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "InspectionTrains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "InspectionTrains");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "InspectionTrains");

            migrationBuilder.RenameColumn(
                name: "AspectoTecnicoSieteObservacion",
                table: "InspectionTrains",
                newName: "AspectoTecnicosieteObservacion");

            migrationBuilder.RenameColumn(
                name: "AspectoTecnicoSieteNo",
                table: "InspectionTrains",
                newName: "AspectoTecnicosieteNo");
        }
    }
}
