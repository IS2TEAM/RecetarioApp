using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetarioApp.Models;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientTiendumsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientTiendumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IngredientTiendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientTiendum>>> GetIngredientTiendum()
        {
          if (_context.IngredientTiendum == null)
          {
              return NotFound();
          }
            return await _context.IngredientTiendum.ToListAsync();
        }

        // GET: api/IngredientTiendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientTiendum>> GetIngredientTiendum(int id)
        {
          if (_context.IngredientTiendum == null)
          {
              return NotFound();
          }
            var ingredientTiendum = await _context.IngredientTiendum.FindAsync(id);

            if (ingredientTiendum == null)
            {
                return NotFound();
            }

            return ingredientTiendum;
        }

        // PUT: api/IngredientTiendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientTiendum(int id, IngredientTiendum ingredientTiendum)
        {
            if (id != ingredientTiendum.IdIngredientTiendum)
            {
                return BadRequest();
            }

            _context.Entry(ingredientTiendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientTiendumExists(id))
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

        // POST: api/IngredientTiendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientTiendum>> PostIngredientTiendum(IngredientTiendum ingredientTiendum)
        {
          if (_context.IngredientTiendum == null)
          {
              return Problem("Entity set 'AppDbContext.IngredientTiendum'  is null.");
          }
            _context.IngredientTiendum.Add(ingredientTiendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientTiendum", new { id = ingredientTiendum.IdIngredientTiendum }, ingredientTiendum);
        }

        // DELETE: api/IngredientTiendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientTiendum(int id)
        {
            if (_context.IngredientTiendum == null)
            {
                return NotFound();
            }
            var ingredientTiendum = await _context.IngredientTiendum.FindAsync(id);
            if (ingredientTiendum == null)
            {
                return NotFound();
            }

            _context.IngredientTiendum.Remove(ingredientTiendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientTiendumExists(int id)
        {
            return (_context.IngredientTiendum?.Any(e => e.IdIngredientTiendum == id)).GetValueOrDefault();
        }
    }
}
