using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        async public Task<IActionResult> GetTodoListAsync([FromServices] AppDbContext context)
        {
            var todos = await context.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        async public Task<IActionResult> GetTodoByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == id);

            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        async public Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created("v1/{todo.Id}", todo);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> UpdateDoneAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (todo == null)
            {
                return BadRequest();
            }

            try
            {
                todo.Done = true;
                context.Todos.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete("todos/{id}")]
        async public Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if (todo == null)
            {
                return BadRequest();
            }

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPut("todos/updateTitle/{id}")]
        async public Task<IActionResult> UpdateTitleAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model, int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);

            if(!ModelState.IsValid || todo == null)
            {
                return BadRequest();
            }

            try
            {
                todo.Title = model.Title;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
        }
    }
}