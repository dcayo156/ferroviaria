namespace LaJuana.Application.Models.ViewModels
{
    public class CategoriesFullVm
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ParentCategoryId { get; set; }
    }
}
