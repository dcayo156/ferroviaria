using MediatR;

namespace LaJuana.Application.Features.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string File { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
