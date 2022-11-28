using LaJuana.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaJuana.Domain
{
    public class TagCategory : BaseDomainModel
    {
        public TagCategory()
        {
            Tags = new HashSet<Tag>();
        }

        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Tag> Tags { get; set; }
        
    }
}
