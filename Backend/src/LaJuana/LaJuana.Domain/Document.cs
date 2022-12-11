using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public class Document: BaseDomainModel
    {  
        public string FileName { get; set; } =string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public Guid? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual Category SubCategory { get; set; }    

    }
}
