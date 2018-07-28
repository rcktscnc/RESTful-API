using Microsoft.EntityFrameworkCore;
using System;
using Case.Data;

namespace Case.Tests
{
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
