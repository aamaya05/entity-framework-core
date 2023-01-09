using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace projectEF.Models;

public class Category
{
  public int CategoryId {get;set;}

  public Guid Uuid {get;set;}

  public string Name {get;set;}

  public string? Description {get;set;}

  public int Point {get; set;}

  [JsonIgnore]
  public virtual ICollection<Todo> Todos {get;set;}
}