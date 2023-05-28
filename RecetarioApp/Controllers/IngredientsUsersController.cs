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
    public class IngredientsUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientsUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IngredientsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientsUser>>> GetIngredientsUsers()
        {
          if (_context.IngredientsUsers == null)
          {
              return NotFound();
          }
            return await _context.IngredientsUsers.ToListAsync();
        }

        // GET: api/IngredientsUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientsUser>> GetIngredientsUser(int id)
        {
          if (_context.IngredientsUsers == null)
          {
              return NotFound();
          }
            var ingredientsUser = await _context.IngredientsUsers.FindAsync(id);

            if (ingredientsUser == null)
            {
                return NotFound();
            }

            return ingredientsUser;
        }

        // PUT: api/IngredientsUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientsUser(int id, IngredientsUser ingredientsUser)
        {
            if (id != ingredientsUser.IdIngredient)
            {
                return BadRequest();
            }

            _context.Entry(ingredientsUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientsUserExists(id))
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

        // POST: api/IngredientsUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientsUser>> PostIngredientsUser(IngredientsUser ingredientsUser)
        {
          if (_context.IngredientsUsers == null)
          {
              return Problem("Entity set 'AppDbContext.IngredientsUsers'  is null.");
          }
            _context.IngredientsUsers.Add(ingredientsUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientsUser", new { id = ingredientsUser.IdIngredient }, ingredientsUser);
        }

        // DELETE: api/IngredientsUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientsUser(int id)
        {
            if (_context.IngredientsUsers == null)
            {
                return NotFound();
            }
            var ingredientsUser = await _context.IngredientsUsers.FindAsync(id);
            if (ingredientsUser == null)
            {
                return NotFound();
            }

            _context.IngredientsUsers.Remove(ingredientsUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientsUserExists(int id)
        {
            return (_context.IngredientsUsers?.Any(e => e.IdIngredient == id)).GetValueOrDefault();
        }
    }
}
