using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Programs.Queries.FindPromansFileById
{
    public class FindProgramsFileByIdQuery : IRequest<string>
    {
        public Guid Id { get; set; }

        public FindProgramsFileByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
