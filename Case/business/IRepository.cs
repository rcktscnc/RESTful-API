using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Data {
  public interface IRepository<T> where T : EntityBase {
    Task<IEnumerable<T>> GetAll();
  }
}
