using LaJuana.Application.Models;
using LaJuana.Application.Models.Identity;

namespace LaJuana.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);       
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<RegistrationResponse> UpdateRegister(UpdateRegistrationRequest request);
        Task UpdatePassword(UpdatePasswordRequest request);
        Task<AuthResponse> Refresh(TokenApiModel request);
        Task Revoke(string? username);
        Task<User> GetUserById(string userId);
        Task<List<User>> GetUsers();
        Task<User> UpdateRoleAdmin(string id, bool Status);
    }
}
