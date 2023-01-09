using Microsoft.EntityFrameworkCore;
using projectEF.Models;

namespace projectEF;

public class TasksContext : DbContext
{
  public DbSet<Category> Categories { get; set; }

  public DbSet<Todo> Todos { get; set; }

  public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    List<Category> categoriesInit = new List<Category>
    {
      new Category() { CategoryId = 1, Uuid = Guid.Parse("9cfab1c9-c234-42e4-a64f-78be3eb8e12d"), Name = "Actividades Pendientes", Point = 20 },
      new Category() { CategoryId = 2 ,Uuid = Guid.Parse("9cfab1c9-c234-42e4-a64f-78be3eb8e56d"), Name = "Actividades Personales", Point = 20 }
    };

    modelBuilder.Entity<Category>(category =>
    {
      category
        .ToTable("category");
      category
        .Property(p => p.CategoryId)
        .HasColumnName("id")
        .ValueGeneratedOnAdd();
      category
        .Property(p => p.Name)
        .IsRequired()
        .HasColumnName("name")
        .HasMaxLength(150);
      category
        .Property(p => p.Description)
        .IsRequired(false)
        .HasColumnName("description");
      category
        .Property(c => c.Point);
      category
        .HasData(categoriesInit);
    });

    List<Todo> todosInit = new List<Todo>
    {
      new Todo() { TodoId = 1, Uuid = Guid.Parse("34d722eb-5fda-4e1f-b838-150140ba4e30"), CategoryId = 1, PriorityTask = Priority.LOW, Title = "Pago de servicios publicos" },
      new Todo() { TodoId = 2, Uuid = Guid.Parse("34d722eb-5fda-4e1f-b838-150140ba4e11"), CategoryId = 2, PriorityTask = Priority.LOW, Title = "Pago de cuota" }
    };

    modelBuilder.Entity<Todo>(todo =>
    {
      todo
        .ToTable("todo");
      todo
        .Property(p => p.TodoId)
        .HasColumnName("id")
        .ValueGeneratedOnAdd();
      todo
        .Property(p => p.Uuid)
        .HasColumnName("uuid");
      todo
        .HasAlternateKey(p => p.Uuid)
        .HasName("todo_uuid_unique");
      todo
        .HasOne(p => p.Category)
        .WithMany(p => p.Todos)
        .HasForeignKey(p => p.CategoryId);
      todo
        .Property(p => p.Title)
        .IsRequired()
        .HasColumnName("title")
        .HasMaxLength(200);
      todo
        .Property(p => p.Description)
        .HasColumnName("description")
        .IsRequired(false);
      todo
        .Property(p => p.PriorityTask)
        .HasColumnName("priority_task")
        .HasDefaultValue(Priority.HIGH);
      todo
        .Property(p => p.CreationDate)
        .HasColumnName("creation_date")
        .HasDefaultValueSql("NOW()");
      todo
        .Ignore(p => p.Resume);
      todo
        .HasData(todosInit);
    });
  }
}