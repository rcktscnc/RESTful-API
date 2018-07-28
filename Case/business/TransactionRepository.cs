using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Case.Data
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly DbContext _Context;

        public TransactionsRepository(DbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Transaction>> Get(TransactionQuery query)
        {
            return await _Context.Set<Transaction>()
                .Where(e => query.Brand.Count == 0 ? true : query.Brand.Contains(e.CardBrandName))
                .Where(e => query.Cnpj.Count == 0 ? true : query.Cnpj.Contains(e.MerchantCnpj))
                .Where(e => query.Date.Count == 0 ? true : query.Date.Contains(e.AcquirerAuthorizationDateTime.Date))
                .Where(e => query.Acquirer.Count == 0 ? true : query.Acquirer.Contains(e.AcquirerName))
                .Where(e => query.Status.Count == 0 ? true : query.Status.Contains(e.Status))
                .Where(e => query.DateMin == default(DateTime) || query.DateMax == default(DateTime) ? true : e.AcquirerAuthorizationDateTime >= query.DateMin && e.AcquirerAuthorizationDateTime <= query.DateMax)
                .Where(e => query.AmountMin == 0 || query.AmountMax == 0 ? true : e.AmountInCent >= query.AmountMin && e.AmountInCent <= query.AmountMax)
                .ToListAsync();
        }
    }
}
