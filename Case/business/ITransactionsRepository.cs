using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Data
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> Get(TransactionQuery query);
    }
}
