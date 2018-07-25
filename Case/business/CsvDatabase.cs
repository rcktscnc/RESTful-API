using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Business
{
    public class CsvDatabase : IDatabase
    {
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
          var t = new Transaction();
          return new List<Transaction>() { t };
        }
    }
}