using MediatR;

namespace LaJuana.Application.Features.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
