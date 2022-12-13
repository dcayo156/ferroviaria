using MediatR;

namespace LaJuana.Application.Features.Programs.Commands.DeletePrograms
{
    public class DeleteProgramsCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
