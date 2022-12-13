using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Programs.Queries.GetListPrograms
{
    public class GetListProgramsQuery : IRequest<List<ProgramsFullVm>>
    {
        public GetListProgramsQuery()
        {

        }

    }
}
