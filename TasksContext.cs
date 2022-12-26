using Microsoft.EntityFrameworkCore;
using proyectEF.Models;

namespace proyectEF;

public class TasksContext: DbContext 
{
  public DbSet<Category> Categories {get;set;}

  public DbSet<Tasks> Tasks {get;set;}

  public TasksContext(DbContextOptions<TasksContext> options) :base(options) {}

}