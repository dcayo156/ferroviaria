using Microsoft.AspNetCore.Http;

namespace LaJuana.Application.Models.ViewModels
{
    public class ProgramsFullVm
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public FormFile? File { get; set; } 
        public string Url { get; set; } = string.Empty;
    }
}
