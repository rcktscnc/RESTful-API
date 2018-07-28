using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xunit;

using Case.Controllers;
using Case.Data;

namespace Case.Tests
{
    public class TransactionsControllerTests
    {
        private readonly ITransactionsRepository _Repository;

        public TransactionsControllerTests() {
            _Repository = RepositoryProvider.NewRepository();
        }

        [Fact(DisplayName = "Should return 200 transaction entries")]
        public async Task GetTest() {
            var controller = new TransactionsController(_Repository);
            var value = (await controller.Get(new TransactionQuery())).Value;
            Assert.Equal(200, value.Results.Count);
        }

        [Fact(DisplayName = "Should return 200 transaction entries")]
        public async Task GetSecureTest() {
            var controller = new TransactionsController(_Repository);
            var value = (await controller.GetSecure(new TransactionQuery())).Value;
            Assert.Equal(200, value.Results.Count);
        }
    }
}
