using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> LogIn(AuthRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new Exception($"El usuario con email { request.Email }, no existe");

            var result = await signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, lockoutOnFailure: false);

            if (!result.Succeeded) throw new Exception($"Las credenciales son incorrectas");


            var token = await GenerateToken(user);
            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await userManager.FindByNameAsync(request.Username);
            if (existingUser == null) throw new Exception($"El username ya existe");

            var existingEmail = await userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null) throw new Exception($"El email ya existe");

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded) throw new Exception($"{ result.Errors }");

            await userManager.AddToRoleAsync(user, "Operator");

            var token = await GenerateToken(user);
            return new RegistrationResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                //new Claim("Username", user.UserName)
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials
                    );

            return jwtSecurityToken;
        }
    }
}
