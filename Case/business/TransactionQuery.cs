using System;
using System.Collections.Generic;

namespace Case.Data {
  public class TransactionQuery {
    public List<long> Cnpj {get; set; } = new List<long>();
    public List<DateTime> Date { get; set; } = new List<DateTime>();
    public List<string> Brand {get; set; } = new List<string>();
    public List<string> Acquirer {get; set; } = new List<string>();
  }
}
