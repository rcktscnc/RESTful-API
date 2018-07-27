using Microsoft.EntityFrameworkCore;
using System;
using Case.Data;

namespace Case.Tests {
  public static class RepositoryFactory {
    private static bool _IsDbEmpty = true;

    public static ITransactionsRepository NewRepository() {
      var builder = new DbContextOptionsBuilder<InMemoryContext>();
      builder.UseInMemoryDatabase("CaseDb");
      var context = new InMemoryContext(builder.Options);

      if (_IsDbEmpty) {
        context.Database.EnsureCreated();
        _IsDbEmpty = false;
      }

      return new TransactionsRepository(context);
    }
  }
}