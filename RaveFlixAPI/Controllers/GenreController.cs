using Microsoft.AspNetCore.Mvc;
using RaveFlixAPI.Models;
using RaveFlixAPI.ViewModel;

namespace RaveFlixAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        [Route("genres")]
        public async Task<ActionResult> GetAllAsync(
            [FromServices] DataContext context)
        {
            var genres = await context
                .Genres
                .AsNoTracking()
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet]
        [Route("genres/id/{id:int}")]
        public async Task<ActionResult> GetByIdAsync(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var genre = await context
                .Genres
                .FirstOrDefaultAsync(g => g.Id == id);

            return genre == null ? NotFound("Gênero não encontrado.") : Ok(genre);
        }

        [HttpGet]
        [Route("genres/name/{name}")]
        public async Task<ActionResult> GetByNameAsync(
        [FromServices] DataContext context,
        [FromRoute] string name)
        {
            var genres = await context
                .Genres
                .FirstOrDefaultAsync(g => g.Name == name);

            return genres == null ? NotFound("Canal não encontrado.") : Ok(genres);
        }

        [HttpPost]
        [Route("genres")]
        public async Task<ActionResult> PostAsync(
           [FromServices] DataContext context,
           [FromBody] CreateGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = new Genre
            {
                Name = model.Name,
            };

            try
            {
                await context.Genres.AddAsync(genre);
                await context.SaveChangesAsync();
                return Created($"v1/genres/{genre.Id}", genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("genres/{id:int}")]
        public async Task<ActionResult> PutAsync(
           [FromServices] DataContext context,
           [FromBody] CreateGenreViewModel model,
           [FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await context
                .Genres
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound("Gênero não encontrado.");
            };

            try
            {
                genre.Name = model.Name;

                context.Genres.Update(genre);
                await context.SaveChangesAsync();

                return Ok("Gênero atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("genres/{id:int}")]
        public async Task<ActionResult> DeleteAsync(
           [FromServices] DataContext context,
           [FromRoute] int id)
        {
            var genre = await context
                .Genres
                .FirstOrDefaultAsync(g => g.Id == id);

            try
            {
                context.Genres.Remove(genre);
                await context.SaveChangesAsync();

                return Ok("Gênero removido com sucesso.");
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

    }
}
