using LaJuana.Application.Constants;
using LaJuana.Application.Contracts.Identity;
using LaJuana.Application.Models;
using LaJuana.Application.Models.Identity;
using LaJuana.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LaJuana.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthWindowsServerService _authWindowsServerService; 
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);


            return jwtSecurityToken;
        }

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, RoleManager<IdentityRole> roleManager, IAuthWindowsServerService authWindowsServerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _authWindowsServerService= authWindowsServerService;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            //var existUser = _authWindowsServerService.IsAuthenticated(_jwtSettings.DomainName, request.Email, request.Password);
            //if (!existUser)
            //    throw new Exception($"El usuario con email {request.Email} no existe");

            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                var registrationRequest = new RegistrationRequest()
                {
                    Email = request.Email,
                    Username = request.Email,
                    Nombre = request.Email,
                    Apellidos = "",
                    Password = request.Password
                };
                var userNew = await Register(registrationRequest);
               
                user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new Exception($"El usuario con email {request.Email} no existe");
            }            

            //var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            //if (!resultado.Succeeded)
            //    throw new Exception($"Las credenciales son incorrectas");

            var token = await GenerateToken(user);
            var refreshToken = GenerateRefreshToken();            
            
            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName,
                RefreshToken = refreshToken
            };
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDurationDays);
            

            var principal = GetPrincipalFromExpiredToken(authResponse.Token);
            var role = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            authResponse.Role = role;
            await _userManager.UpdateAsync(user);
            return authResponse;
        }
        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                throw new Exception($"El username ya fue tomado por otra cuenta");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new Exception($"El email ya fue tomado por otra cuenta");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                var token = await GenerateToken(user);
                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id,
                    Username = user.UserName,
                };
            }

            throw new Exception($"{result.Errors}");

        }
        public async Task<RegistrationResponse> UpdateRegister(UpdateRegistrationRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null && existingUser.Id != request.Id)
            {
                throw new Exception($"El username ya fue tomado por otra cuenta");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null && existingEmail.Id != request.Id)
            {
                throw new Exception($"El email ya fue tomado por otra cuenta");
            }
            user.Email=request.Email;
            user.Apellidos=request.Apellidos;
            user.Nombre=request.Nombre;
            user.UserName=request.Username;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {   
                throw new Exception($"{result.Errors.First().Description}");
            }
            var token = await GenerateToken(user);
            return new RegistrationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = user.UserName,
            };
        }
        public async Task UpdatePassword(UpdatePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            var resultChangePassword = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!resultChangePassword.Succeeded)
            {
                throw new Exception($"{resultChangePassword.Errors}");
            }
        }
        public async Task<AuthResponse> Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                throw new Exception("Invalid client request");

            string? accessToken = tokenApiModel.AccessToken;
            string? refreshToken = tokenApiModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; //principal.Identity!.Name;
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("Invalid client request");

            var newAccessToken = await GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);
            return new AuthResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
        public async Task Revoke(string username)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                throw new Exception();
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task<User> GetUserById(string userId)
        {
            
            var user = _userManager.Users.SingleOrDefault(u => u.Id.Equals(userId));
            
            if (user == null)
            {
                throw new Exception("No se encontro al usuario");
            }
            var userResult = await GetUser(user);
            return await Task.FromResult(userResult);
        }

        private async Task<User> GetUser(ApplicationUser user)
        {
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            return new User()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Email = user.Email,
                Username = user.UserName,
                Admin = roles.Contains("ADMIN"),
            };

        }

        public async Task<List<User>> GetUsers()
        {
            var result = new List<User>();

            foreach (var user in _userManager.Users.ToList())
            {
                result.Add(await GetUser(user));
            }
            return result;
        }




        public async Task<User> UpdateRoleAdmin(string id, bool Status)
        {

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("El usuario no existe");
            }

            var userResult = await GetUser(usuario);


            if (Status)
            {

                var resultado = await _userManager.AddToRoleAsync(usuario, "ADMIN");
                if (resultado.Succeeded)
                {
                    userResult.Admin = true;
                }

                if (resultado.Errors.Any())
                {
                    if (resultado.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        userResult.Admin = true;
                    }
                }
            }
            else
            {

                var resultado = await _userManager.RemoveFromRoleAsync(usuario, "ADMIN");
                if (resultado.Succeeded)
                {
                    userResult.Admin = false;
                }
            }


            return userResult;

        }
    }
}
