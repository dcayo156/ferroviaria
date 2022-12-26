namespace LaJuana.Application.Models.ViewModels
{
    public class CategoriesFullVm
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public CategoriesFullVm ParentCategory { get; set; }
    }
}
