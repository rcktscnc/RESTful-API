using System.Collections.Generic;
using System.Threading.Tasks;

namespace Case.Data {
    public interface IRepository<T> {
        Task<IEnumerable<T>> GetAll();
    }
}
