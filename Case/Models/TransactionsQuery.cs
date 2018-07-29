using System;
using System.Collections.Generic;

namespace Case.Models
{
    public class TransactionsQuery
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
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public long Id { get; set; }

        /// <summary>
        /// Acceptable values are "date_asc", "date_desc", "amount_asc", and "amount_desc"
        /// </summary>
        public string OrderBy { get; set; }
    }
}
