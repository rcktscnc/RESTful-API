using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using Case.Data;

namespace Case.Tests
{
    public class TransactionsRepositoryTests
    {
        private readonly ITransactionsRepository _Repository;

        public TransactionsRepositoryTests() {
            _Repository = RepositoryFactory.NewRepository();
        }

        [Fact(DisplayName = "Should contain 200 transaction entries")]
        public async Task AllTransactions() {
            var transactions = (await _Repository.Get(new TransactionQuery())).ToList();
            Assert.Equal(200, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only Visa transaction entries")]
        public async Task VisaTransactions() {
            var query = new TransactionQuery();
            query.Brand.Add("Visa");
            var transactions = (await _Repository.Get(query)).ToList();
            Assert.True(transactions.Where(e => e.CardBrandName == "Visa").Count() == transactions.Count); 
        }
    }
}
