using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Categories.Queries.GetListCategories
{
    public class GetListCategoriesQuery : IRequest<List<CategoriesFullVm>>
    {
        public GetListCategoriesQuery()
        {

        }

    }
}
