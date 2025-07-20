using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Data;
using Todo.Core.ViewModels;


namespace Todo.Core.Controllers;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("Todos")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        var todos = await context
            .Todos
            .AsNoTracking()
            .ToListAsync();
        return Ok(todos);
    }

    [HttpGet]
    [Route("Todos/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todos = await context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return todos == null ? NotFound() : Ok(todos);
    }

    [HttpPost]
    [Route("Todos")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
        [FromBody] CreateTodoViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = new Models.Todo
        {
            Date = DateTime.Now,
            Done = false,
            Title = model.Title
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("Todos/{id}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
        [FromBody] UptadeTodoViewModel model, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = context.Todos.FirstOrDefault(x => x.Id == id);

        if (todo == null)
            return NotFound();

        try
        {
            todo.Title = model.Title;

            context.Todos.Update(todo);
            await context.SaveChangesAsync();

            return Ok(todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("Todos/{id}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = context.Todos.FirstOrDefault(x => x.Id == id);

        if (todo == null)
            return NotFound();

        try
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}