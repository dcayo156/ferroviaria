using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Categories.Queries.GetListChildrenCategories
{
    public class GetListChildrenCategoriesQuery : IRequest<List<CategoriesChildrenFullVm>>
    {
        public GetListChildrenCategoriesQuery()
        {

        }
    }
}
