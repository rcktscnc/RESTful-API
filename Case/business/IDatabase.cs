using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Business
{
    public interface IDatabase
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}