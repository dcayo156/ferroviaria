namespace LaJuana.Application.Models.ViewModels
{
    public class CategoriesChildrenFullVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public virtual IList<CategoriesChildrenFullVm>? Categories { get; set; }
    }
}
