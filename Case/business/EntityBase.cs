using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace Case.Data {
  public abstract class EntityBase {
    [Key]
    [JsonIgnore]
    [Ignore]
    public long Id { get; set; }
  }
}
