﻿using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Documents.Commands.CreateFileDocuments
{
    public class CreateDocumentsFileCommand : IRequest<FileDirectoryResponseVm>
    {
        public string FileName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;    
        public string FilePath { get; set; } = string.Empty;
        public bool IsFile { get; set; } = false;
    }
}
