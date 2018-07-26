using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Case.Data {
  public class Repository<T> : IRepository<T> where T : EntityBase {
    private readonly DbContext _Context;
    
    public Repository(DbContext context) {
      _Context = context;
    }

    public async Task<IEnumerable<T>> GetAll() {
      return await _Context.Set<T>().ToListAsync();
    }
  }
}
