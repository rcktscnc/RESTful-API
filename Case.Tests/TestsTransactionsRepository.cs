using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

using Case.Models;
using Case.Business;

namespace Case.Tests
{
    public class TestsTransactionsRepository
    {
        private readonly ITransactionsRepository _Repository;

        public TestsTransactionsRepository()
        {
            _Repository = RepositoryProvider.Instance.NewRepository();
        }

        private async Task<List<Transaction>> GetTransactions(TransactionsQuery query)
        {
            return (await _Repository.GetTransactions(query)).ToList();
        }

        [Fact(DisplayName = "All transactions")]
        public async Task AllTransactions()
        {
            var transactions = await GetTransactions(new TransactionsQuery() { PageSize = 200 });
            Assert.Equal(200, transactions.Count);
        }

        [Fact(DisplayName = "Specific Card Brand")]
        public async Task VisaTransactions()
        {
            var query = new TransactionsQuery();
            query.Brand.Add("Visa");
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.True(transactions.Where(e => e.CardBrandName == "Visa").Count() == transactions.Count && transactions.Count > 0);
        }

        [Fact(DisplayName = "Specific CNPJ")]
        public async Task SpecificCnpjTransactions()
        {
            var query = new TransactionsQuery();
            query.Cnpj.Add("15593743000351");
            query.Cnpj.Add("28176030000172");
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(12, transactions.Count);
        }

        [Fact(DisplayName = "Specific Date")]
        public async Task SpecificDateTransactions()
        {
            var query = new TransactionsQuery();
            query.Date.Add(DateTime.Parse("2018-03-01"));
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(113, transactions.Count);
        }

        [Fact(DisplayName = "Specific Acquirer")]
        public async Task SpecificAcquirerTransactions()
        {
            var query = new TransactionsQuery();
            query.Acquirer.Add("Stone");
            query.Acquirer.Add("Cielo");
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(131, transactions.Count);
        }

        [Fact(DisplayName = "Specific Status")]
        public async Task SpecificStatusTransactions()
        {
            var query = new TransactionsQuery();
            query.Status.Add("Recusada");
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(18, transactions.Count);
        }

        [Fact(DisplayName = "Specific Period")]
        public async Task SpecificPeriodTransactions()
        {
            var query = new TransactionsQuery();
            query.DateMin = DateTime.Parse("2018-03-01T00:55:36");
            query.DateMax = DateTime.Parse("2018-03-01T01:02:38");
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(7, transactions.Count);
        }

        [Fact(DisplayName = "Specific Amount")]
        public async Task SpecificAmountTransactions()
        {
            var query = new TransactionsQuery();
            query.AmountMin = 1;
            query.AmountMax = 1000;
            query.PageSize = 200;
            var transactions = await GetTransactions(query);
            Assert.Equal(29, transactions.Count);
        }

        [Fact(DisplayName = "Specific Page")]
        public async Task SpecificPageTransactions()
        {
            var query = new TransactionsQuery();
            query.Page = 2;
            var transactions = await GetTransactions(query);
            Assert.Equal(20, transactions.Count);
        }

        [Fact(DisplayName = "Specific Page and PageSize")]
        public async Task SpecificPageAndPageSizeTransactions()
        {
            var query = new TransactionsQuery();
            query.Page = 2;
            query.PageSize = 35;
            var transactions = await GetTransactions(query);
            Assert.Equal(35, transactions.Count);
        }

        [Fact(DisplayName = "Specific OrderBy DateAsc")]
        public async Task SpecificOrderByDateAscTransactions()
        {
            var query = new TransactionsQuery();
            query.OrderBy = "date_asc";
            var transactions = await GetTransactions(query);
            Assert.True(transactions.First().AcquirerAuthorizationDateTime == DateTime.Parse("2018-02-28T22:43:56"));
        }

        [Fact(DisplayName = "Specific OrderBy DateDesc")]
        public async Task SpecificOrderByDateDescTransactions()
        {
            var query = new TransactionsQuery();
            query.OrderBy = "date_desc";
            var transactions = await GetTransactions(query);
            Assert.True(transactions.First().AcquirerAuthorizationDateTime == DateTime.Parse("2018-03-01T01:03:19"));
        }

        [Fact(DisplayName = "Specific OrderBy AmountAsc")]
        public async Task SpecificOrderByAmountAscTransactions()
        {
            var query = new TransactionsQuery();
            query.OrderBy = "amount_asc";
            var transactions = await GetTransactions(query);
            Assert.True(transactions.First().AmountInCent == 300);
        }

        [Fact(DisplayName = "Specific OrderBy AmountDesc")]
        public async Task SpecificOrderByAmountDescTransactions()
        {
            var query = new TransactionsQuery();
            query.OrderBy = "amount_desc";
            var transactions = await GetTransactions(query);
            Assert.True(transactions.First().AmountInCent == 267330);
        }

        [Fact(DisplayName = "Specific Id")]
        public async Task SpecificIdTransactions()
        {
            var query = new TransactionsQuery();
            query.Id = 20;
            var transactions = await GetTransactions(query);
            
            Assert.True(transactions.First().AmountInCent == 24000);
            Assert.True(transactions.First().CardBrandName == "Visa");
            Assert.True(transactions.First().AcquirerName == "Getnet");
        }
    }
}
