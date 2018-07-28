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

/* namespace Case.Tests {
    public sealed class RepositoryProvider
    {
        private static readonly RepositoryProvider _Instance = new RepositoryProvider();
        private static bool _IsDbEmpty = true;
        private static object _Lock = new object();

        static RepositoryProvider() { }

        private RepositoryProvider() { }

        public static RepositoryProvider Instance { get { return _Instance; } }

        public ITransactionsRepository NewRepository() {
          lock (_Lock) {
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
} */