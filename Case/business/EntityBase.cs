using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Case.Data {
  public abstract class EntityBase {
    [Key]
    [JsonIgnore]
    public long Id { get; protected set; }
  }
}
