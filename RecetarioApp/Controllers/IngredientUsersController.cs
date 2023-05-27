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
    public class IngredientUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IngredientUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientUser>>> GetIngredientsUser()
        {
          if (_context.IngredientsUser == null)
          {
              return NotFound();
          }
            return await _context.IngredientsUser.ToListAsync();
        }

        // GET: api/IngredientUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientUser>> GetIngredientUser(int id)
        {
          if (_context.IngredientsUser == null)
          {
              return NotFound();
          }
            var ingredientUser = await _context.IngredientsUser.FindAsync(id);

            if (ingredientUser == null)
            {
                return NotFound();
            }

            return ingredientUser;
        }

        // PUT: api/IngredientUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientUser(int id, IngredientUser ingredientUser)
        {
            if (id != ingredientUser.IdIngredient)
            {
                return BadRequest();
            }

            _context.Entry(ingredientUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientUserExists(id))
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

        // POST: api/IngredientUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientUser>> PostIngredientUser(IngredientUser ingredientUser)
        {
          if (_context.IngredientsUser == null)
          {
              return Problem("Entity set 'AppDbContext.IngredientsUser'  is null.");
          }
            _context.IngredientsUser.Add(ingredientUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientUser", new { id = ingredientUser.IdIngredient }, ingredientUser);
        }

        // DELETE: api/IngredientUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientUser(int id)
        {
            if (_context.IngredientsUser == null)
            {
                return NotFound();
            }
            var ingredientUser = await _context.IngredientsUser.FindAsync(id);
            if (ingredientUser == null)
            {
                return NotFound();
            }

            _context.IngredientsUser.Remove(ingredientUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientUserExists(int id)
        {
            return (_context.IngredientsUser?.Any(e => e.IdIngredient == id)).GetValueOrDefault();
        }
    }
}
