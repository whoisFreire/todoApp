using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("signup")]
        async public Task<IActionResult> CreateUserAsync([FromServices] AppDbContext context, [FromBody] CreateUserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created("users/{user.Id}", user);
            }
            catch (System.Exception)
            {
                
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteAccount")]
        async public Task<IActionResult> DeleteUserAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if(user == null)
            {
                return BadRequest();
            }

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {
                
                throw;
            }

        }
    }
}