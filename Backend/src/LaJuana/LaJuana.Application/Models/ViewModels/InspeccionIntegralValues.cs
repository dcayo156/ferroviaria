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

            //Inspeccion de train
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenCatorceItem", Fila = 27, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenQuinceItem", Fila = 28, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSeisItem", Fila = 29, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSieteItem", Fila = 30, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciOchoItem", Fila = 31, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciNueveItem", Fila = 32, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeinteItem", Fila = 33, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiUnoItem", Fila = 34, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiDosItem", Fila = 35, Columna = 1 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiTresItem", Fila = 36, Columna = 1 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenCatorceSi", Fila = 27, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenQuinceSi", Fila = 28, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSeisSi", Fila = 29, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSieteSi", Fila = 30, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciOchoSi", Fila = 31, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciNueveSi", Fila = 32, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeinteSi", Fila = 33, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiUnoSi", Fila = 34, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiDosSi", Fila = 35, Columna = 2 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiTresSi", Fila = 36, Columna = 2 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenCatorceNo", Fila = 27, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenQuinceNo", Fila = 28, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSeisNo", Fila = 29, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSieteNo", Fila = 30, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciOchoNo", Fila = 31, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciNueveNo", Fila = 32, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeinteNo", Fila = 33, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiUnoNo", Fila = 34, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiDosNo", Fila = 35, Columna = 3 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiTresNo", Fila = 36, Columna = 3 });

            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenCatorceObservacion", Fila = 27, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenQuinceObservacion", Fila = 28, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSeisObservacion", Fila = 29, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciSieteObservacion", Fila = 30, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciOchoObservacion", Fila = 31, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenDieciNueveObservacion", Fila = 32, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeinteObservacion", Fila = 33, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiUnoObservacion", Fila = 34, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiDosObservacion", Fila = 35, Columna = 4 });
            values.Add(new InspeccionIntegralProperty() { PropertyId = "InspeccionTrenVeintiTresObservacion", Fila = 36, Columna = 4 });

            //Firma Nombre
            values.Add(new InspeccionIntegralProperty() { PropertyId = "ObservacionEvaluador", Fila = 38, Columna = 4 });            
            return values;
        }
    }
    public class InspeccionIntegralProperty
    {
        public string? PropertyId { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
    }

    public class ColumnListInspeccionTren
    {
        public ColumnListInspeccionTren()
        {
            ColumnInspeccionTren = SetValue();
        }
        public List<string> ColumnInspeccionTren { get; set; }

        public List<string> SetValue()
        {
            List<string> values = new List<string>();
            values.Add("CreatedBy");
            values.Add("LastModifiedDate");
            values.Add("SubCategoryId");
            values.Add("Category");
            values.Add("FileName");
            values.Add("File");
            values.Add("CreatedDate");
            values.Add("LastModifiedBy");
            values.Add("SubCategory");
            values.Add("CategoryId");
            values.Add("Status");
            values.Add("Id");
            return values;
        }
    }
    
}
