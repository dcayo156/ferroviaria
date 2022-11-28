
namespace LaJuana.Application.Models.ViewModels
{
    public class TagCategoryVm
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<TagFullVm>? Tags { get; set; }
    }
}
