using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Linq;

namespace Case.Data
{
    public class InMemoryContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            using (TextReader fr = File.OpenText("./transactions.csv"))
            {
                var csv = new CsvReader(fr);
                csv.Configuration.Delimiter = ";";
                var records = csv.GetRecords<Transaction>().ToList();

                var id = 1;
                foreach (var record in records)
                {
                    record.Id = id++; // When seeding, primary key must be set manually
                }

                builder.Entity<Transaction>().HasData(records.ToArray());
            }
        }
    }
}
