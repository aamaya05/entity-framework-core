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
    modelBuilder.Entity<Category>(category =>
    {
      category
        .ToTable("category");
      category
        .HasKey(p => p.CategoryId);
      category
        .Property(p => p.Name)
        .IsRequired()
        .HasColumnName("name")
        .HasMaxLength(150);
      category
        .Property(p => p.Description)
        .HasColumnName("description");
      category
        .Property(c => c.Point);
    });

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
    });
  }
}