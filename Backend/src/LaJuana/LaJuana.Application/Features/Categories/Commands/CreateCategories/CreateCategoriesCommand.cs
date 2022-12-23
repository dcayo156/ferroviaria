using MediatR;

namespace LaJuana.Application.Features.Categories.Commands.CreateCategories
{
    public class CreateCategoriesCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;    
        public string File { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
