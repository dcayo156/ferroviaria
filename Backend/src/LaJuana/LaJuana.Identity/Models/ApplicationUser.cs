using Microsoft.AspNetCore.Identity;

namespace LaJuana.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;    
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
