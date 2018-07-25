using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Business
{
    public class CsvRepository : IRepository
    {
        private readonly IDatabase _Database;

        public CsvRepository(IDatabase database)
        {
            _Database = database;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _Database.GetAllTransactions();
        }
    }
}