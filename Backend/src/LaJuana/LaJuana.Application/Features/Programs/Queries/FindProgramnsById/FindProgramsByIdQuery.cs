using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Programs.Queries.FindProgramsById
{
    public class FindProgramsByIdQuery : IRequest<ProgramsFullVm>
    {
        public Guid Id { get; set; }

        public FindProgramsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
