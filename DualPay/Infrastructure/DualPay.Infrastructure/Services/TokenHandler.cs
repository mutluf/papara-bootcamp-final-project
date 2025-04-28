using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DualPay.Application.Abstraction.Token;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DualPay.Infrastructure.Services;

public class TokenHandler : ITokenHandler
{
    readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    
    public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<Token> CreateTokenAsync(int minute, AppUser user)
    {
        Token token = new Token();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        token.Expiration = DateTime.Now.AddMinutes(minute);
        
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        }
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials,
            claims: claims
        );
        
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}