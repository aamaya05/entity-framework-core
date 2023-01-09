using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectEF;
using projectEF.Models;
using System.Text.Json;

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
  // var highTodos = dbContext.Todos
  //   .Include(todo => todo.Category)
  //   .Where(todo => todo.PriorityTask == projectEF.Models.Priority.HIGH);

  var allTodos = dbContext.Todos.Include(todo => todo.Category);

  return Results.Ok(allTodos);

});

app.MapPost("/api/todos", async([FromServices] TasksContext dbContext, [FromBody] Todo todo) =>
{

  todo.Uuid = Guid.NewGuid();
  string json = JsonSerializer.Serialize<Todo>(todo);
Console.WriteLine($"PEPEE {json}");

  await dbContext.AddAsync(todo);

  await dbContext.SaveChangesAsync();

  return Results.Ok();

});

app.MapPut("/api/todos/{id}", async([FromServices] TasksContext dbContext, [FromBody] Todo todo, [FromRoute] int id) =>
{
  var actualTodo = dbContext.Todos.Find(id);

  Console.WriteLine($"PEPEE {JsonSerializer.Serialize(actualTodo)}");

  if (actualTodo != null)
  {
    actualTodo.CategoryId = todo.CategoryId;
    actualTodo.Title = todo.Title;
    actualTodo.Description = todo.Description;
    actualTodo.PriorityTask = todo.PriorityTask;
    actualTodo.CreationDate = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    return Results.Ok();
  }

  return Results.NotFound();
});


app.MapDelete("/api/todos/{id}", async([FromServices] TasksContext dbContext, [FromRoute] int id) =>
{
  var todo = dbContext.Todos.Find(id);

    if(todo == null)
      return Results.NotFound("Todo not found.");

    dbContext.Remove<Todo>(todo);
    await dbContext.SaveChangesAsync();

    return Results.Ok("Removed!");
});

app.Run();
