using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace Case.Models
{
    /// <summary>
    /// Base class for all EF Framework entities
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        [Ignore]
        [JsonIgnore]
        public long Id { get; set; }
    }
}
