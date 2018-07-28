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

        [Fact(DisplayName = "Should match payload")]
        public void TransactionControllerTest() {
            var controller = new TransactionsController(_Repository);
        }
    }
}
