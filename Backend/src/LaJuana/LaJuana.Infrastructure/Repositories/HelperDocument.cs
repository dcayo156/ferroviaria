using LaJuana.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Infrastructure.Repositories
{
    public class HelperDocument: IHelpersDocument
    {
        public void CheckDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en CheckDirectory");
            }           
        }
        public async Task SaveFile(string file, string filePath) 
        {
            try
            {
                var fileStreem = new MemoryStream(Convert.FromBase64String(file));           
                using (var fileStream = File.Create(filePath))
                {
                    fileStreem.Seek(0, SeekOrigin.Begin);
                    await fileStreem.CopyToAsync(fileStream);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al Guardar el archivo");
            }            
        }
    }
}
