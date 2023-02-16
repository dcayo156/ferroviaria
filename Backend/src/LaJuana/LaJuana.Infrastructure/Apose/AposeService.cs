using Aspose.Cells;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using System.Reflection;

namespace LaJuana.Infrastructure.Apose
{
    public class AposeService : IAposeService
    {
        public AposeService() { }
        public async Task<InspectionTrain> ReadDocInspectionIntegral(string pathFile, InspectionTrain item)
        {
            try
            {
                Workbook wb = new Workbook(pathFile);
                WorksheetCollection collection = wb.Worksheets;
                var inspeccionVauesCells = new InspeccionIntegralValues();
                var inspeccionProperty = new InspeccionIntegralProperty();

                Worksheet worksheet = collection[0];
                foreach (var property in item.GetType().GetProperties())
                {
                    inspeccionProperty = inspeccionVauesCells.InspeccionIntegralList?.FirstOrDefault(x => x.PropertyId == property.Name);
                    if (inspeccionProperty != null)
                    {
                        var value = worksheet.Cells[inspeccionProperty.Fila, inspeccionProperty.Columna].Value?.ToString();

                        Type type = item.GetType();

                        PropertyInfo prop = type.GetProperty(property.Name);

                        prop.SetValue(item, value, null);
                    }
                }
                return item;
            }
            catch (Exception)
            {
                throw new Exception("Error al Guardar el archivo");
            }

        }
        public async Task<string> SaveDocInspectionIntegral(string pathFile, List<InspectionTrain> items)
        {
            try
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;
                //Espacio de Celdas
                for (int i = 0; i < 92; i++)
                {
                    worksheet.Cells.SetColumnWidth(i, 24);
                }  
                int rowsCount = 1, columnCount = 0;
                var request = new InspectionTrain();
                var valuesColumns = new ColumnListInspeccionTren();
                foreach (var item in items)
                {
                    foreach (var property in item.GetType().GetProperties().Where(x => !valuesColumns.ColumnInspeccionTren.Contains(x.Name)))
                    {
                        cells[0, columnCount].PutValue(property.Name);

                        Type type = item.GetType();

                        PropertyInfo prop = type.GetProperty(property.Name);

                        var ass = prop.GetValue(item, null);
                        cells[rowsCount, columnCount].PutValue(ass == null? "": ass.ToString());
                        columnCount++;
                    }
                    columnCount = 0;
                    rowsCount++;
                }
                var filePath = "C:\\Documents\\ListaInspeccionTren.xlsx";
                workbook.Save(filePath, SaveFormat.Xlsx); 
                return filePath;
            }
            catch (Exception)
            {
                throw new Exception("Error al Guardar el archivo");
            }

        }
    }
}
