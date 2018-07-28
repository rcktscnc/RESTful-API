using System;
using System.Collections.Generic;

namespace Case.Data
{
    public class TransactionQuery
    {
        public List<string> Cnpj { get; set; } = new List<string>();
        public List<DateTime> Date { get; set; } = new List<DateTime>();
        public List<string> Brand { get; set; } = new List<string>();
        public List<string> Acquirer { get; set; } = new List<string>();
        public List<string> Status { get; set; } = new List<string>();
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }
        public long AmountMin { get; set; }
        public long AmountMax { get; set; }
    }
}
