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
        [Route("festivals/{id:int}")]
        public async Task<ActionResult> GetByIdAsync(
            [FromServices] DataContext context,
            [FromRoute] int id)
        {
            var festival = await context
                .Festivals
                .FirstOrDefaultAsync(f => f.Id == id);

            return festival == null ? NotFound("Canal não encontrado.") : Ok(festival);
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
        [Route("festivals/{id:int}")]
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
        [Route("festivals/{id:int}")]
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
