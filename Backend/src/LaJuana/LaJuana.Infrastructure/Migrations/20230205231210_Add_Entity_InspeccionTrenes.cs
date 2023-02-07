using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaJuana.Infrastructure.Migrations
{
    public partial class Add_Entity_InspeccionTrenes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InspectionTrains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTren = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locomotoras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maquinista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuxiliarMaquina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoUnoSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoDosSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoTresSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoCuatroSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoUnoNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoDosNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoTresNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoCuatroNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoUnoObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoDosObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoTresObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoBasicoCuatroObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoCincoSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoSeisSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoSieteSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoOchoSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoNueveSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoDiezSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoCincoNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoSeisNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicosieteNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoOchoNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoNueveNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoDiezNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoCincoObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoSeisObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicosieteObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoOchoObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoNueveObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AspectoTecnicoDiezObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoOnceSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoDoceSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoTreceSi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoOnceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoDoceeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoTreceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoOnceObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoDoceObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenejoAdecuadoTreceObservacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObservacionEvaluador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionTrains", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionTrains");
        }
    }
}
