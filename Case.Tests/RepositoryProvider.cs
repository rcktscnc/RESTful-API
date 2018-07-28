using Microsoft.EntityFrameworkCore;
using System;

using Case.Data;

namespace Case.Tests
{
    // I decided to use a singleton class because I needed the Database.EnsureCreated() method
    // to be called only once during the test's life time. And the Singleton Pattern seemed
    // to be the most elegant approach in this case. This singleton is thread safe.

    /// <summary>
    /// Provides data repositories that access a test DB already populated with test data
    /// </summary>
    public sealed class RepositoryProvider
    {
        private static readonly RepositoryProvider _Instance = new RepositoryProvider();

        static RepositoryProvider() { }

        private RepositoryProvider()
        {
            CreateContext().Database.EnsureCreated();
        }

        public static RepositoryProvider Instance { get { return _Instance; } }

        public ITransactionsRepository NewRepository()
        {
            return new TransactionsRepository(CreateContext());
        }

        private InMemoryContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder<InMemoryContext>().UseInMemoryDatabase("CaseDb");
            return new InMemoryContext(builder.Options);
        }
    }
}
