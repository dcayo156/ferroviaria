using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Application.Models.ViewModels
{
    public class InspectionTrainBasicAspects
    {
        public string? AspectoBasicoUnoSi { get; set; }
        public string? AspectoBasicoDosSi { get; set; }
        public string? AspectoBasicoTresSi { get; set; }
        public string? AspectoBasicoCuatroSi { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class InspectionTrainTechnicalAspects
    {   
        public string? AspectoTecnicoCincoSi { get; set; }
        public string? AspectoTecnicoSeisSi { get; set; }
        public string? AspectoTecnicoSieteSi { get; set; }
        public string? AspectoTecnicoOchoSi { get; set; }
        public string? AspectoTecnicoNueveSi { get; set; }
        public string? AspectoTecnicoDiezSi { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class InspectionTrainProperHandling
    {
        public string? MenejoAdecuadoOnceSi { get; set; }
        public string? MenejoAdecuadoDoceSi { get; set; }
        public string? MenejoAdecuadoTreceSi { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
