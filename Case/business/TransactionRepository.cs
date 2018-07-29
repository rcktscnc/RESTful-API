using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

using Case.Models;

namespace Case.Business
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly DbContext _Context;

        public TransactionsRepository(DbContext context)
        {
            _Context = context;
        }

        // I/O tasks are done asynchronously to improve performance
        public async Task<IEnumerable<Transaction>> GetTransactions(TransactionsQuery query)
        {
            if (query.Id != 0)
            {
                return await _Context.Set<Transaction>().Where(e => e.TransactionId == query.Id).ToListAsync();
            }
            
            var queriable = _Context.Set<Transaction>()
                .Where(e => query.Brand.Count == 0 ? true : query.Brand.Contains(e.CardBrandName))
                .Where(e => query.Cnpj.Count == 0 ? true : query.Cnpj.Contains(e.MerchantCnpj))
                .Where(e => query.Date.Count == 0 ? true : query.Date.Contains(e.AcquirerAuthorizationDateTime.Date))
                .Where(e => query.Acquirer.Count == 0 ? true : query.Acquirer.Contains(e.AcquirerName))
                .Where(e => query.Status.Count == 0 ? true : query.Status.Contains(e.Status))
                .Where(e => query.DateMin == default(DateTime) ? true :  e.AcquirerAuthorizationDateTime >= query.DateMin)
                .Where(e => query.DateMax == default(DateTime) ? true :  e.AcquirerAuthorizationDateTime <= query.DateMax)
                .Where(e => query.AmountMin == 0 ? true : e.AmountInCent >= query.AmountMin)
                .Where(e => query.AmountMax == 0 ? true : e.AmountInCent <= query.AmountMax);

            switch (query.OrderBy)
            {
                case "date_asc":
                    queriable = queriable.OrderBy(t => t.AcquirerAuthorizationDateTime);
                    break;
                case "date_desc":
                    queriable = queriable.OrderByDescending(t => t.AcquirerAuthorizationDateTime);
                    break;
                case "amount_asc":
                    queriable = queriable.OrderBy(t => t.AmountInCent);
                    break;
                case "amount_desc":
                    queriable = queriable.OrderByDescending(t => t.AmountInCent);
                    break;
            }

            return await queriable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }
    }
}
