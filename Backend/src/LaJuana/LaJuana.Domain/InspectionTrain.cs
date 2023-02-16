using LaJuana.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaJuana.Domain
{
    public class InspectionTrain : BaseDomainModel
    {
        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public Guid? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual Category SubCategory { get; set; }
        public int Status { get; set; }
        public string Codigo { get; set; }
        public string? Fecha { get; set; }
        public string? NumeroTren { get; set; }
        public string? Locomotoras { get; set; }
        public string? Maquinista { get; set; }
        public string? AuxiliarMaquina { get; set; }
        //Datos basicos
        public string? AspectoBasicoUnoSi { get; set; }
        public string? AspectoBasicoDosSi { get; set; }
        public string? AspectoBasicoTresSi { get; set; }
        public string? AspectoBasicoCuatroSi { get; set; }

        public string? AspectoBasicoUnoNo { get; set; }
        public string? AspectoBasicoDosNo { get; set; }
        public string? AspectoBasicoTresNo { get; set; }
        public string? AspectoBasicoCuatroNo { get; set; }

        public string? AspectoBasicoUnoObservacion { get; set; }
        public string? AspectoBasicoDosObservacion { get; set; }
        public string? AspectoBasicoTresObservacion { get; set; }
        public string? AspectoBasicoCuatroObservacion { get; set; }
        //Datos Tecnicos
        public string? AspectoTecnicoCincoSi { get; set; }
        public string? AspectoTecnicoSeisSi { get; set; }
        public string? AspectoTecnicoSieteSi { get; set; }
        public string? AspectoTecnicoOchoSi { get; set; }
        public string? AspectoTecnicoNueveSi { get; set; }
        public string? AspectoTecnicoDiezSi { get; set; }

        public string? AspectoTecnicoCincoNo { get; set; }
        public string? AspectoTecnicoSeisNo { get; set; }
        public string? AspectoTecnicoSieteNo { get; set; }
        public string? AspectoTecnicoOchoNo { get; set; }
        public string? AspectoTecnicoNueveNo { get; set; }
        public string? AspectoTecnicoDiezNo { get; set; }

        public string? AspectoTecnicoCincoObservacion { get; set; }
        public string? AspectoTecnicoSeisObservacion { get; set; }
        public string? AspectoTecnicoSieteObservacion { get; set; }
        public string? AspectoTecnicoOchoObservacion { get; set; }
        public string? AspectoTecnicoNueveObservacion { get; set; }
        public string? AspectoTecnicoDiezObservacion { get; set; }

        //Manejo adecuado de trenes y uso de freno
        public string? MenejoAdecuadoOnceSi { get; set; }
        public string? MenejoAdecuadoDoceSi { get; set; }
        public string? MenejoAdecuadoTreceSi { get; set; }

        public string? MenejoAdecuadoOnceNo { get; set; }
        public string? MenejoAdecuadoDoceeNo { get; set; }
        public string? MenejoAdecuadoTreceNo { get; set; }

        public string? MenejoAdecuadoOnceObservacion { get; set; }
        public string? MenejoAdecuadoDoceObservacion { get; set; }
        public string? MenejoAdecuadoTreceObservacion { get; set; }


        public string? ObservacionEvaluador { get; set; }

        //Datos de Documento
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        //

        public string? InspeccionTrenCatorceItem { get; set; }
        public string? InspeccionTrenQuinceItem { get; set; }
        public string? InspeccionTrenDieciSeisItem { get; set; }
        public string? InspeccionTrenDieciSieteItem { get; set; }
        public string? InspeccionTrenDieciOchoItem { get; set; }
        public string? InspeccionTrenDieciNueveItem { get; set; }
        public string? InspeccionTrenVeinteItem { get; set; }
        public string? InspeccionTrenVeintiUnoItem { get; set; }
        public string? InspeccionTrenVeintiDosItem { get; set; }
        public string? InspeccionTrenVeintiTresItem { get; set; }

        public string? InspeccionTrenCatorceSi { get; set; }
        public string? InspeccionTrenQuinceSi { get; set; }
        public string? InspeccionTrenDieciSeisSi { get; set; }
        public string? InspeccionTrenDieciSieteSi { get; set; }
        public string? InspeccionTrenDieciOchoSi { get; set; }
        public string? InspeccionTrenDieciNueveSi { get; set; }
        public string? InspeccionTrenVeinteSi { get; set; }
        public string? InspeccionTrenVeintiUnoSi { get; set; }
        public string? InspeccionTrenVeintiDosSi { get; set; }
        public string? InspeccionTrenVeintiTresSi { get; set; }

        public string? InspeccionTrenCatorceNo { get; set; }
        public string? InspeccionTrenQuinceNo { get; set; }
        public string? InspeccionTrenDieciSeisNo { get; set; }
        public string? InspeccionTrenDieciSieteNo { get; set; }
        public string? InspeccionTrenDieciOchoNo { get; set; }
        public string? InspeccionTrenDieciNueveNo { get; set; }
        public string? InspeccionTrenVeinteNo { get; set; }
        public string? InspeccionTrenVeintiUnoNo { get; set; }
        public string? InspeccionTrenVeintiDosNo { get; set; }
        public string? InspeccionTrenVeintiTresNo { get; set; }

        public string? InspeccionTrenCatorceObservacion { get; set; }
        public string? InspeccionTrenQuinceObservacion { get; set; }
        public string? InspeccionTrenDieciSeisObservacion { get; set; }
        public string? InspeccionTrenDieciSieteObservacion { get; set; }
        public string? InspeccionTrenDieciOchoObservacion { get; set; }
        public string? InspeccionTrenDieciNueveObservacion { get; set; }
        public string? InspeccionTrenVeinteObservacion { get; set; }
        public string? InspeccionTrenVeintiUnoObservacion { get; set; }
        public string? InspeccionTrenVeintiDosObservacion { get; set; }
        public string? InspeccionTrenVeintiTresObservacion { get; set; }
    }
}
