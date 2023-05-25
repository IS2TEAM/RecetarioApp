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
    public class IngredienteTiendumsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredienteTiendumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IngredienteTiendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteTiendum>>> GetIngredienteTienda()
        {
          if (_context.IngredienteTienda == null)
          {
              return NotFound();
          }
            return await _context.IngredienteTienda.ToListAsync();
        }

        // GET: api/IngredienteTiendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredienteTiendum>> GetIngredienteTiendum(int id)
        {
          if (_context.IngredienteTienda == null)
          {
              return NotFound();
          }
            var ingredienteTiendum = await _context.IngredienteTienda.FindAsync(id);

            if (ingredienteTiendum == null)
            {
                return NotFound();
            }

            return ingredienteTiendum;
        }

        // PUT: api/IngredienteTiendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredienteTiendum(int id, IngredienteTiendum ingredienteTiendum)
        {
            if (id != ingredienteTiendum.IdIngredientTiendum)
            {
                return BadRequest();
            }

            _context.Entry(ingredienteTiendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteTiendumExists(id))
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

        // POST: api/IngredienteTiendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredienteTiendum>> PostIngredienteTiendum(IngredienteTiendum ingredienteTiendum)
        {
          if (_context.IngredienteTienda == null)
          {
              return Problem("Entity set 'AppDbContext.IngredienteTienda'  is null.");
          }
            _context.IngredienteTienda.Add(ingredienteTiendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredienteTiendum", new { id = ingredienteTiendum.IdIngredientTiendum }, ingredienteTiendum);
        }

        // DELETE: api/IngredienteTiendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredienteTiendum(int id)
        {
            if (_context.IngredienteTienda == null)
            {
                return NotFound();
            }
            var ingredienteTiendum = await _context.IngredienteTienda.FindAsync(id);
            if (ingredienteTiendum == null)
            {
                return NotFound();
            }

            _context.IngredienteTienda.Remove(ingredienteTiendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredienteTiendumExists(int id)
        {
            return (_context.IngredienteTienda?.Any(e => e.IdIngredientTiendum == id)).GetValueOrDefault();
        }
    }
}
