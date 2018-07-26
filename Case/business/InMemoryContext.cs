using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CsvHelper;

namespace Case.Data {
  public class InMemoryContext : DbContext {
    public DbSet<Transaction> Transactions { get; set; }
    
    public InMemoryContext(DbContextOptions<InMemoryContext> options) : base (options) {
      if (Set<Transaction>().ToListAsync().Result.Count > 0 ) {
        return;
      }

      using (TextReader fr = File.OpenText("./transactions.csv")) {
        var csv = new CsvReader(fr);
        csv.Configuration.Delimiter = ";";
        var records = csv.GetRecords<Transaction>();
        AddRange(records);
        SaveChanges();
      }
    }
  }
}
