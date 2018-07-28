using Microsoft.EntityFrameworkCore;
using System;
using Case.Data;

namespace Case.Tests {
  public static class RepositoryProvider {
    private static bool _IsDbEmpty = true;
    private static object _Lock = new object();

    public static ITransactionsRepository NewRepository() {
      lock (_Lock) {
        var builder = new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("CaseDb");
        var context = new InMemoryContext(builder.Options);

        if (_IsDbEmpty) {
          context.Database.EnsureCreated();
          _IsDbEmpty = false;
        }

        return new TransactionsRepository(context);
      }
    }
  }
}
