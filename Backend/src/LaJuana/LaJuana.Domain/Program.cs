using LaJuana.Domain.Common;

namespace LaJuana.Domain
{
    public class Program: BaseDomainModel
    {
        public string Name { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

    }
}
