using Microsoft.AspNetCore.Mvc;
using RaveFlixAPI.Models;
using RaveFlixAPI.ViewModel;

namespace RaveFlixAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult> GetAllAsync(
            [FromServices] DataContext context)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("users/{id:int}")]
        public async Task<ActionResult> GetByIdAsync(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(g => g.Id == id);

            return user == null ? NotFound("Usuário não encontrado.") : Ok(user);
        }

        [HttpPost]
        [Route("users")]
        public async Task<ActionResult> PostAsync(
           [FromServices] DataContext context,
           [FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"v1/users/{user.Id}", user);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("users/{id:int}")]
        public async Task<ActionResult> PutAsync(
           [FromServices] DataContext context,
           [FromBody] CreateUserViewModel model,
           [FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await context
                .Users
                .FirstOrDefaultAsync(g => g.Id == id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            };

            try
            {
                user.Username = model.Username;
                user.Password = model.Password;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("users/{id:int}")]
        public async Task<ActionResult> DeleteAsync(
           [FromServices] DataContext context,
           [FromRoute] int id)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(g => g.Id == id);

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok("Usuário removido com sucesso.");
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

    }
}
