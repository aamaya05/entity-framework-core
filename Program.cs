using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectEF;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("testDB"));
builder.Services.AddNpgsql<TasksContext>(builder.Configuration.GetConnectionString("development"));

var app = builder.Build();

app.MapGet("/dbconnection", async ([FromServices] TasksContext dbContext) =>
{
  dbContext.Database.EnsureCreated();
  return Results.Ok($"Database created in memory: {dbContext.Database.IsInMemory()}");
});

app.MapGet("/api/todos", async([FromServices] TasksContext dbContext) =>
{

  return Results.Ok(dbContext.Todos
    .Include(todo => todo.Category)
    .Where(todos => todos.PriorityTask == projectEF.Models.Priority.HIGH));

});

app.Run();
