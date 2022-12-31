using MediatR;

namespace LaJuana.Application.Features.Documents.Commands.CreateFileDocuments
{
    public class CreateDocumentsFileCommand : IRequest<string>
    {
        public string FileName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;    
        public string FilePath { get; set; } = string.Empty;
        public bool IsFile { get; set; } = false;
    }
}
