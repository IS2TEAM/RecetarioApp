using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetarioApp.Models;
using RecetarioApp.Models.ModelDTO;
using Swashbuckle.AspNetCore.Annotations;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesingredientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RecipesingredientsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Recipesingredients
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los ingredientes recetas")]
        public async Task<ActionResult<IEnumerable<Recipesingredient>>> GetRecipesingredients()
        {
          if (_context.Recipesingredients == null)
          {
              return NotFound();
          }
            return await _context.Recipesingredients.ToListAsync();
        }

        // GET: api/Recipesingredients/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un ingredinete receta", Description = "Obtener un ingrediente receta en especifico con el id")]
        public async Task<ActionResult<Recipesingredient>> GetRecipesingredient(int id)
        {
          if (_context.Recipesingredients == null)
          {
              return NotFound();
          }
            var recipesingredient = await _context.Recipesingredients.FindAsync(id);

            if (recipesingredient == null)
            {
                return NotFound();
            }

            return recipesingredient;
        }

        // PUT: api/Recipesingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de un ingrediente receta", Description = "Modificar todas las caracteristicas de un ingrediente receta en el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutRecipesingredient(int id, RecipesIngredientDTO recipesingredientDto)
        {
            var recipesingredient = _mapper.Map<Recipesingredient>(recipesingredientDto);

            if (id != recipesingredient.IdRecipeIngredient)
            {
                return BadRequest();
            }

            _context.Entry(recipesingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipesingredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipesingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo ingrediente receta", Description = "Crea un nuevo ingrediente receta en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<Recipesingredient>> PostRecipesingredient(RecipesIngredientDTO recipesingredientDto)
        {
            var recipesingredient = _mapper.Map<Recipesingredient>(recipesingredientDto);
            if (_context.Recipesingredients == null)
          {
              return Problem("Entity set 'AppDbContext.Recipesingredients'  is null.");
          }
            _context.Recipesingredients.Add(recipesingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipesingredient", new { id = recipesingredient.IdRecipeIngredient }, recipesingredient);
        }

        // DELETE: api/Recipesingredients/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar un ingrediente receta", Description = "Eliminar un ingrediente receta en el sistema.")]
        public async Task<IActionResult> DeleteRecipesingredient(int id)
        {
            if (_context.Recipesingredients == null)
            {
                return NotFound();
            }
            var recipesingredient = await _context.Recipesingredients.FindAsync(id);
            if (recipesingredient == null)
            {
                return NotFound();
            }

            _context.Recipesingredients.Remove(recipesingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipesingredientExists(int id)
        {
            return (_context.Recipesingredients?.Any(e => e.IdRecipeIngredient == id)).GetValueOrDefault();
        }
    }
}
