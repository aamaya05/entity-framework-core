namespace proyectEF.Models;

public class Tasks
{
  public Guid TaskId {get;set;}

  public Guid CategoryId {get;set;}

  public string Title {get;set;}

  public string Description {get;set;}

  public Priority PriorityTask {get;set;}

  public DateTime CreationDate {get;set;}

  public virtual Category Category {get;set;}
}

public enum Priority
{
  LOW,
  MEDIUM,
  HIGH
}