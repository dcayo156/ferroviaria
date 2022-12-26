using MediatR;

namespace LaJuana.Application.Features.Categories.Commands.CreateCategories
{
    public class CreateCategoriesCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; } = null;
    }
}
