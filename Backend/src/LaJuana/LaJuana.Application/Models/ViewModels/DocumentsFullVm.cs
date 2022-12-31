using LaJuana.Domain;

namespace LaJuana.Application.Models.ViewModels
{
    public class DocumentsFullVm
    {
        public string Id { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string SubCategoryId { get; set; } = string.Empty;
    }
}
