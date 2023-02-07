using LaJuana.Application.Contracts.Infrastructure;
using LaJuana.Application.Models;
using Microsoft.Extensions.Options;

namespace LaJuana.Infrastructure.Directories
{
    public class DocumentService : IDocumentService
    {
        public DirectoryIconSettings _directoryIconSettings { get; }
        public DocumentService() { }
        public DocumentService(IOptions<DirectoryIconSettings> directoryIconSettings)
        {
            _directoryIconSettings = directoryIconSettings.Value;
        }
        public async Task<string> SaveIcon(string iconName, string file)
        {
            try
            {
                var base64 = file.Split(',')[1];
                var pathComplete = Path.Combine(_directoryIconSettings.IconDirectory, iconName);
                CheckDirectory(_directoryIconSettings.IconDirectory);
                await SaveFile(base64, pathComplete);
                return pathComplete;
            }
            catch (Exception)
            {
                throw new Exception("Error al Guardar el archivo");
            }

        }
        public async Task<string> SaveDocument(string path, string documentName, string file, bool isDocument, bool isNew)
        {
            try
            {
                var directory = Path.Combine(
                    isDocument ? _directoryIconSettings.FileDirectory : _directoryIconSettings.PhotoDirectory,
                    path);

                CheckDirectory(directory);
                if (isNew)
                {
                    var extension = Path.GetExtension(documentName);
                    var nameFile = System.IO.Path.GetFileNameWithoutExtension(documentName);
                    documentName = nameFile + "_" + DateTime.Now.ToString("MM-dd-yyyy H-mm") + extension;
                }
                var filePath = Path.Combine(directory, documentName);

                //if (File.Exists(filePath))
                //{
                //    throw new Exception("Ya existe un documento con el directorio con el mismo nombre");
                //}

                var base64 = string.Empty;
                var a = file.Split(',').Length;
                if (file.Split(',').Length > 1)
                {
                    base64 = file.Split(',')[1];
                }

                await SaveFile(base64 == string.Empty ? file : base64, filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
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
