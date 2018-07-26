using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Case.Data {
  public class TransactionsRepository : IRepository<Transaction> {
    private readonly InMemoryContext _Context; // should be interface
    public TransactionsRepository(InMemoryContext context) {
      _Context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAll() {
      return await _Context.Set<Transaction>().ToListAsync();
    }
  }
}