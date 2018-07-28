using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xunit;

using Case.Auth;

namespace Case.Tests
{
    public class JwtProviderTests
    {
        [Fact(DisplayName = "Should match payload")]
        public void JwtProviderTest()
        {
            var policy = "SomePolicy";
            var issuer = "Issuer";
            var audience = "Audience";

            var jwtProvider = new JwtProvider(policy, "1234567890ABCDEFGHI", issuer, audience);
            var tokenString = jwtProvider.GetToken();
            var token = new JwtSecurityTokenHandler().ReadToken(tokenString) as JwtSecurityToken;

            Assert.Equal(policy, token.Claims.First().Value);
            Assert.Equal(issuer, token.Issuer);
            Assert.Equal(audience, token.Audiences.First());
        }
    }
}
