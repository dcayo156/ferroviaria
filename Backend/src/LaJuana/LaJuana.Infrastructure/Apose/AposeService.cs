using Aspose.Cells;
using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;
using System.Reflection;

namespace LaJuana.Infrastructure.Apose
{
    public class AposeService: IAposeService
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
    }
}
