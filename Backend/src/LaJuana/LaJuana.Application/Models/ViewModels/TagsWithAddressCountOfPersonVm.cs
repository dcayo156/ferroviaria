using System.Text.Json;
using LaJuana.Domain;
namespace LaJuana.Application.Models.ViewModels
{
    public class TagsWithAddressCountOfPersonVm
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int NumberOfPeople { get; set; }

    }
    public class TagItemVm
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int NumberOfPeople { get; set; }
    }
    public class CategoryWithTagsVm
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public TagItemVm[] Tags { get; set; }
    }
}
