using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public class InspectionTrain : BaseDomainModel
    {
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
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

    }
}
