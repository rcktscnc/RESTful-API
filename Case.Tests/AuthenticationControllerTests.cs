using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xunit;

using Case.Controllers;
using Case.Auth;

namespace Case.Tests
{
    public class AuthenticationControllerTests
    {
        [Fact(DisplayName = "Should return 200 transaction entries")]
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
