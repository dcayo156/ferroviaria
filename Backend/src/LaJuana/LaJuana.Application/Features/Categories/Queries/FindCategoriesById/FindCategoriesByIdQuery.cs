using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Categories.Queries.FindCategoriesById
{
    public class FindCategoriesByIdQuery : IRequest<CategoriesFullVm>
    {
        public Guid Id { get; set; }

        public FindCategoriesByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
