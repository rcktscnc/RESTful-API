using System.Collections.Generic;
using System.Threading.Tasks;

using Case.Models;

namespace Case.Business
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions(TransactionQuery query);
    }
}
