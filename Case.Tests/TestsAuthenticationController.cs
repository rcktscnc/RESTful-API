using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xunit;

using Case.Controllers;
using Case.Authentication;

namespace Case.Tests
{
    public class TestsAuthenticationController
    {
        [Fact(DisplayName = "Should return valid token")]
        public void GetTest()
        {
            var policy = "SomePolicy";
            var issuer = "Issuer";
            var audience = "Audience";

            var controller = new AuthenticationController(new JwtProvider(policy, "1234567890ABCDEFGHI", issuer, audience));
            var tokenString = controller.Get().Value.Token;
            var token = new JwtSecurityTokenHandler().ReadToken(tokenString) as JwtSecurityToken;

            Assert.Equal(policy, token.Claims.First().Value);
            Assert.Equal(issuer, token.Issuer);
            Assert.Equal(audience, token.Audiences.First());
        }
    }
}
