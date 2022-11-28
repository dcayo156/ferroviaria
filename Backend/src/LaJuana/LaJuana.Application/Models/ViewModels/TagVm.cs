
using System.Text.Json.Serialization;

namespace LaJuana.Application.Models.ViewModels
{
    public class TagVm
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TagCategoryDescription { get { return TagCategory?.Description; } }
        [JsonIgnore]
        public TagCategoryVm? TagCategory { get; set; }
        
    }
}
