namespace LaJuana.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInMinutes { get; set; }
        public double RefreshTokenDurationDays { get; set; }
        public string DomainName { get; set; } = string.Empty;
        public string ServerName { get; set; } = string.Empty;
    }
}
