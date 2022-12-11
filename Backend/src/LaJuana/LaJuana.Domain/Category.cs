using LaJuana.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaJuana.Domain
{
    public class Category: BaseDomainModel
    {
        [Required(ErrorMessage = "Error field"), MaxLength(128, ErrorMessage = "Maximo the caracter is 128")]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("ParentCategory")]
        public Guid? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual Category? ParentCategory { get; set; }

        [NotMapped]
        public virtual IList<Category>? Categories { get; set; }
    }
}
