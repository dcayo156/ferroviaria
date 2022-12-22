using MediatR;

namespace LaJuana.Application.Features.Programs.Commands.UpdatePrograms
{
    public class UpdateProgramsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
