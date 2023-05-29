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
