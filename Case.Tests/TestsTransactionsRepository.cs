using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

using Case.Data;

namespace Case.Tests
{
    public class TestsTransactionsRepository
    {
        private readonly ITransactionsRepository _Repository;

        public TestsTransactionsRepository()
        {
            _Repository = RepositoryProvider.Instance.NewRepository();
        }

        private async Task<List<Transaction>> GetTransactions(TransactionQuery query)
        {
            return (await _Repository.Get(query)).ToList();
        }

        [Fact(DisplayName = "Should contain 200 transaction entries -- All transactions")]
        public async Task AllTransactions()
        {
            var transactions = await GetTransactions(new TransactionQuery());
            Assert.Equal(200, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only Visa transaction entries -- Specific Card Brand")]
        public async Task VisaTransactions()
        {
            var query = new TransactionQuery();
            query.Brand.Add("Visa");
            var transactions = await GetTransactions(query);
            Assert.True(transactions.Where(e => e.CardBrandName == "Visa").Count() == transactions.Count && transactions.Count > 0);
        }

        [Fact(DisplayName = "Should contain only 12 transaction entries -- Specific CNPJ")]
        public async Task SpecificCnpjTransactions()
        {
            var query = new TransactionQuery();
            query.Cnpj.Add("15593743000351");
            query.Cnpj.Add("28176030000172");
            var transactions = await GetTransactions(query);
            Assert.Equal(12, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 113 transaction entries -- Specific Date")]
        public async Task SpecificDateTransactions()
        {
            var query = new TransactionQuery();
            query.Date.Add(DateTime.Parse("2018-03-01"));
            var transactions = await GetTransactions(query);
            Assert.Equal(113, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 131 transaction entries -- Specific Acquirer")]
        public async Task SpecificAcquirerTransactions()
        {
            var query = new TransactionQuery();
            query.Acquirer.Add("Stone");
            query.Acquirer.Add("Cielo");
            var transactions = await GetTransactions(query);
            Assert.Equal(131, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 18 transaction entries -- Specific Status")]
        public async Task SpecificStatusTransactions()
        {
            var query = new TransactionQuery();
            query.Status.Add("Recusada");
            var transactions = await GetTransactions(query);
            Assert.Equal(18, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 7 transaction entries -- Specific Period")]
        public async Task SpecificPeriodTransactions()
        {
            var query = new TransactionQuery();
            query.DateMin = DateTime.Parse("2018-03-01T00:55:36");
            query.DateMax = DateTime.Parse("2018-03-01T01:02:38");
            var transactions = await GetTransactions(query);
            Assert.Equal(7, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 29 transaction entries -- Specific Amount")]
        public async Task SpecificAmountTransactions()
        {
            var query = new TransactionQuery();
            query.AmountMin = 1;
            query.AmountMax = 1000;
            var transactions = await GetTransactions(query);
            Assert.Equal(29, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 30 transaction entries -- Specific Page")]
        public async Task SpecificPageTransactions()
        {
            var query = new TransactionQuery();
            query.Page = 2;
            var transactions = await GetTransactions(query);
            Assert.Equal(30, transactions.Count);
        }

        [Fact(DisplayName = "Should contain only 35 transaction entries -- Specific Page and PageSize")]
        public async Task SpecificPageAndPageSizeTransactions()
        {
            var query = new TransactionQuery();
            query.Page = 2;
            query.PageSize = 35;
            var transactions = await GetTransactions(query);
            Assert.Equal(35, transactions.Count);
        }
    }
}