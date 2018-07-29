using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xunit;

using Case.Models;
using Case.Business;
using Case.Controllers;


namespace Case.Tests
{
    public class TestsTransactionsController
    {
        private readonly ITransactionsRepository _Repository;

        public TestsTransactionsController()
        {
            _Repository = RepositoryProvider.Instance.NewRepository();
        }

        [Fact(DisplayName = "Should return 20 transaction entries")]
        public async Task GetTest()
        {
            var controller = new TransactionsController(_Repository);
            var value = (await controller.Get(new TransactionsQuery())).Value;
            Assert.Equal(20, value.Results.Count);
        }

        [Fact(DisplayName = "Should return 20 transaction entries")]
        public async Task GetSecureTest()
        {
            var controller = new TransactionsController(_Repository);
            var value = (await controller.GetSecure(new TransactionsQuery())).Value;
            Assert.Equal(20, value.Results.Count);
        }
    }
}
