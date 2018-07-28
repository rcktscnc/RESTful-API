using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace Case.Auth {
  public class JwtProvider : IJwtProvider {
    private readonly string _JwtSecretKey;
    private readonly string _Policy;
    private readonly string _Issuer;
    private readonly string _Audience;

    public JwtProvider(string policy, string jwtSecretKey, string issuer, string audience) {
      _JwtSecretKey = jwtSecretKey;
      _Policy = policy;
      _Issuer = issuer;
      _Audience = audience;
    }

    public string GetToken() {
      var claims = new List<Claim> { new Claim(ClaimTypes.Role, _Policy) };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSecretKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: _Issuer,
        audience: _Audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
