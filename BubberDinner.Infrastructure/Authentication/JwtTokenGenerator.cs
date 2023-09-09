using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BubberDinner.Application.Common.Interfaces.Authentication;
using BubberDinner.Application.Common.Interfaces.Services;

using Microsoft.IdentityModel.Tokens;

namespace BubberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly IDateTimeProvider _dateTimeProvider;

  public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
  {
    _dateTimeProvider = dateTimeProvider;
  }

  public string GenerateToken(Guid userId, string firstName, string lastName)
  {
    //* the length of the super secret key will turn into byte array
    //* the length of the byte array will be depending on the SecurityAlgorithm its use
    //* HmacSHA256 will need 256 bits minimum == 32 bytes
    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nxoafxzxabwkgllyvrkdbfbkccsjtkup")),
      SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
          new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
          new Claim(JwtRegisteredClaimNames.GivenName, firstName),
          new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

    //* Issuer ben cap phep cho jwt securitytoken
    var securityToken = new JwtSecurityToken(
      issuer: "BubberDinner",
      expires: _dateTimeProvider.UtcNow.AddMinutes(60),
      claims: claims,
      signingCredentials: signingCredentials);

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
