namespace LaJuana.Application.Models.ViewModels
{
    public class InspeccionIntegralValues
    {
        public InspeccionIntegralValues()
        {
            if (InspeccionIntegralList == null)
            {
                InspeccionIntegralList = SetValue();
            }
        }      
        public List<InspeccionIntegralProperty> InspeccionIntegralList { get; set; }

        public List<InspeccionIntegralProperty> SetValue()
        {
            var values = new List<InspeccionIntegralProperty>();
            values.Add(new InspeccionIntegralProperty() { PropertyId = "Fecha", Fila = 2, Columna = 2 });//Fecha
            values.Add(new InspeccionIntegralProperty() { PropertyId = "NumeroTren", Fila = 3, Columna = 2 });//
            values.Add(new InspeccionIntegralProperty() { PropertyId = "Locomotoras", Fila = 4, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "Maquinista", Fila = 5, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AuxiliarMaquina", Fila = 6, Columna = 2 });

            //Datos basicos
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoUnoSi", Fila = 10, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoDosSi", Fila = 11, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoTresSi", Fila = 12, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoCuatroSi", Fila = 13, Columna = 2 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoUnoNo", Fila = 10, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoDosNo", Fila = 11, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoTresNo", Fila = 12, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoCuatroNo", Fila = 13, Columna = 3 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoUnoObservacion", Fila = 10, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoDosObservacion", Fila = 11, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoTresObservacion", Fila = 12, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoBasicoCuatroObservacion", Fila = 13, Columna = 4 });

            //Datos Tecnicos
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoCincoSi", Fila = 16, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSeisSi", Fila = 17, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSieteSi", Fila = 18, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoOchoSi", Fila = 19, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoNueveSi", Fila = 20, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoDiezSi", Fila = 21, Columna = 2 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoCincoNo", Fila = 16, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSeisNo", Fila = 17, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSieteNo", Fila = 18, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoOchoNo", Fila = 19, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoNueveNo", Fila = 20, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoDiezNo", Fila = 31, Columna = 3 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoCincoObservacion", Fila = 16, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSeisObservacion", Fila = 17, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoSieteObservacion", Fila = 18, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoOchoObservacion", Fila = 19, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoNueveObservacion", Fila = 20, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "AspectoTecnicoDiezObservacion", Fila = 21, Columna = 4 });

            //Manejo adecuado de trenes y uso de freno
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoOnceSi", Fila = 23, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoDoceSi", Fila = 24, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoTreceSi", Fila = 25, Columna = 2 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoOnceNo", Fila = 23, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoDoceeNo", Fila = 24, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoTreceNo", Fila = 25, Columna = 3 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoOnceObservacion", Fila = 23, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoDoceObservacion", Fila = 24, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "MenejoAdecuadoTreceObservacion", Fila = 25, Columna = 4 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "ObservacionEvaluador", Fila = 35, Columna = 1 });

            return values;
        }
    }
    public class InspeccionIntegralProperty
    {
        public string? PropertyId { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
}
