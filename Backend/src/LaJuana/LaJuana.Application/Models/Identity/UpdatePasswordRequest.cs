namespace LaJuana.Application.Models.Identity
{
    public class UpdatePasswordRequest
    {
        public string Id { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}