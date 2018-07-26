using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CsvHelper;

namespace Case.Data {
  public class InMemoryContext : DbContext {
    public DbSet<Transaction> Transactions { get; set; }
    
    public InMemoryContext(DbContextOptions<InMemoryContext> options) : base (options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
      using (TextReader fr = File.OpenText("./transactions.csv")) {
        var csv = new CsvReader(fr);
        csv.Configuration.Delimiter = ";";
        var records = csv.GetRecords<Transaction>();
        var id = 1;
        foreach (var record in records) {
          record.Id = id++; // When seeding data, Id must be provided manually
          builder.Entity<Transaction>().HasData(record);
        }
      }
    }
  }
}
