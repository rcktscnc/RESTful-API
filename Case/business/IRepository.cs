using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Business
{
    public interface IRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}