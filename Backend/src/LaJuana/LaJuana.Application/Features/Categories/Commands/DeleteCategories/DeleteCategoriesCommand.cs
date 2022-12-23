using MediatR;

namespace LaJuana.Application.Features.Categories.Commands.DeleteCategories
{
    public class DeleteCategoriesCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
