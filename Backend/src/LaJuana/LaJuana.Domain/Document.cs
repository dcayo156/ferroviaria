using LaJuana.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaJuana.Domain
{
    public enum DocumentStatus
    {
        Habilitado = 0,
        Deshabilitado = 1
    }
    public class Document: BaseDomainModel
    {  
        public string FileName { get; set; } =string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string? PhotoName { get; set; } = string.Empty;
        public string? PhotoPath { get; set; } = string.Empty;

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }   
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public Guid? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual Category SubCategory { get; set; }
        public string Code { get; set; } = string.Empty;
        public int Status { get; set; }

    }
}
