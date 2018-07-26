using Microsoft.EntityFrameworkCore;

namespace Case.Data {
  public class InMemoryContext : DbContext {
    public DbSet<Transaction> Transactions { get; set; }
    public InMemoryContext(DbContextOptions<InMemoryContext> options) : base (options) {
      Add(new Transaction());
      Add(new Transaction());
      Add(new Transaction());
      Add(new Transaction());
      SaveChanges();
    }
  }
}
