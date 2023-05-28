using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetarioApp.Mappers;
using RecetarioApp.Models;
using RecetarioApp.Models.ModelDTO;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingingredientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingingredientsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Shoppingingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoppingingredient>>> GetShoppingingredients()
        {
          if (_context.Shoppingingredients == null)
          {
              return NotFound();
          }
            return await _context.Shoppingingredients.ToListAsync();
        }

        // GET: api/Shoppingingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoppingingredient>> GetShoppingingredient(int id)
        {
          if (_context.Shoppingingredients == null)
          {
              return NotFound();
          }
            var shoppingingredient = await _context.Shoppingingredients.FindAsync(id);

            if (shoppingingredient == null)
            {
                return NotFound();
            }

            return shoppingingredient;
        }

        // PUT: api/Shoppingingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingingredient(int id, Shoppingingredient shoppingingredient)
        {
            if (id != shoppingingredient.IdShoppingIngredients)
            {
                return BadRequest();
            }

            _context.Entry(shoppingingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingingredientExists(id))
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

        // POST: api/Shoppingingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoppingingredient>> PostShoppingingredient(ShoppingingredientDTO shoppingingredientDto)
        {
            var shoppingingredient = _mapper.Map<Shoppingingredient>(shoppingingredientDto);

          if (_context.Shoppingingredients == null)
          {
              return Problem("Entity set 'AppDbContext.Shoppingingredients'  is null.");
          }
            _context.Shoppingingredients.Add(shoppingingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingingredient", new { id = shoppingingredient.IdShoppingIngredients }, shoppingingredient);
        }

        // DELETE: api/Shoppingingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingingredient(int id)
        {
            if (_context.Shoppingingredients == null)
            {
                return NotFound();
            }
            var shoppingingredient = await _context.Shoppingingredients.FindAsync(id);
            if (shoppingingredient == null)
            {
                return NotFound();
            }

            _context.Shoppingingredients.Remove(shoppingingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingingredientExists(int id)
        {
            return (_context.Shoppingingredients?.Any(e => e.IdShoppingIngredients == id)).GetValueOrDefault();
        }
    }
}
