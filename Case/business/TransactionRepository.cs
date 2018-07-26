using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Case.Data {
  public class TransactionRepository : IRepository<Transaction> {
    private readonly DbContext _Context;
    
    public TransactionRepository(DbContext context) {
      _Context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAll() {
      return await _Context.Set<Transaction>().ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> Get(TransactionQuery query) {
      return await _Context.Set<Transaction>()
        .Where(e => query.Brand.Count == 0 ? true : query.Brand.Contains(e.CardBrandName))
        .Where(e => query.Cnpj.Count == 0 ? true : query.Cnpj.Contains(e.MerchantCnpj))
        .Where(e => query.Date.Count == 0 ? true : query.Date.Contains(e.AcquirerAuthorizationDateTime.Date))
        .Where(e => query.Acquirer.Count == 0 ? true : query.Acquirer.Contains(e.AcquirerName))
        .ToListAsync();
    }
  }
}
