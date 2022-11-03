using Microsoft.AspNetCore.Mvc;
using RaveFlixAPI.Models;
using RaveFlixAPI.ViewModel;

namespace RaveFlixAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FestivalController : ControllerBase
    {
        [HttpGet]
        [Route("festivals")]
        public async Task<ActionResult> GetAllAsync(
            [FromServices] DataContext context)
        {
            var festivals = await context
                .Festivals
                .AsNoTracking()
                .ToListAsync();

            return Ok(festivals);
        }

        [HttpGet]
        [Route("festivals/id/{id:int}")]
        public async Task<ActionResult> GetByIdAsync(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var festival = await context
                .Festivals
                .FirstOrDefaultAsync(f => f.Id == id);

            return festival == null ? NotFound("Canal não encontrado.") : Ok(festival);
        }

        [HttpGet]
        [Route("festivals/name/{name}")]
        public async Task<ActionResult> GetByNameAsync(
          [FromServices] DataContext context,
          [FromRoute] string name)
        {
            var festival = await context
                .Festivals
                .FirstOrDefaultAsync(f => f.Name == name);

            return festival == null ? NotFound("Canal não encontrado.") : Ok(festival);
        }

        [HttpGet]
        [Route("festivals/country/{country}")]
        public async Task<ActionResult> GetByCountryAsync(
          [FromServices] DataContext context,
          [FromRoute] string country)
        {
            var festivals = await context
                .Festivals
                .Where(f => f.Country == country)
                .OrderBy( f => f.Name)
                .ToListAsync(); 

            return festivals == null ? NotFound("Canal não encontrado.") : Ok(festivals);
        }

        [HttpPost]
        [Route("festivals")]
        public async Task<ActionResult> PostAsync(
           [FromServices] DataContext context,
           [FromBody] CreateFestivalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var festival = new Festival
            {
                Name = model.Name,
                Country = model.Country
            };

            try
            {
                await context.Festivals.AddAsync(festival);
                await context.SaveChangesAsync();
                return Created($"v1/festivals/{festival.Id}", festival);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("festivals/id/{id:int}")]
        public async Task<ActionResult> PutAsync(
           [FromServices] DataContext context,
           [FromBody] CreateFestivalViewModel model,
           [FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var festival = await context
                .Festivals
                .FirstOrDefaultAsync(f => f.Id == id);

            if (festival == null)
            {
                return NotFound("Canal não encontrado.");
            };

            try
            {
                festival.Name = model.Name;
                festival.Country = model.Country;

                context.Festivals.Update(festival);
                await context.SaveChangesAsync();

                return Ok("Festival atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("festivals/id/{id:int}")]
        public async Task<ActionResult> DeleteAsync(
           [FromServices] DataContext context,
           [FromRoute] int id)
        {
            var festival = await context
                .Festivals
                .FirstOrDefaultAsync(f => f.Id == id);

            try
            {
                context.Festivals.Remove(festival);
                await context.SaveChangesAsync();

                return Ok("Festival removido com sucesso.");
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }
           
        }

    }
}
