using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetarioApp.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppinglistsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ShoppinglistsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Shoppinglists
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los ingredientes de la lista de compra")]
        public async Task<ActionResult<IEnumerable<Shoppinglist>>> GetShoppinglists()
        {
          if (_context.Shoppinglists == null)
          {
              return NotFound();
          }
            return await _context.Shoppinglists.ToListAsync();
        }

        // GET: api/Shoppinglists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoppinglist>> GetShoppinglist(int id)
        {
          if (_context.Shoppinglists == null)
          {
              return NotFound();
          }
            var shoppinglist = await _context.Shoppinglists.FindAsync(id);

            if (shoppinglist == null)
            {
                return NotFound();
            }

            return shoppinglist;
        }

        // PUT: api/Shoppinglists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppinglist(int id, Shoppinglist shoppinglist)
        {
            if (id != shoppinglist.IdList)
            {
                return BadRequest();
            }

            _context.Entry(shoppinglist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppinglistExists(id))
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

        // POST: api/Shoppinglists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoppinglist>> PostShoppinglist(Shoppinglist shoppinglist)
        {
          if (_context.Shoppinglists == null)
          {
              return Problem("Entity set 'AppDbContext.Shoppinglists'  is null.");
          }
            _context.Shoppinglists.Add(shoppinglist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppinglistExists(shoppinglist.IdList))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppinglist", new { id = shoppinglist.IdList }, shoppinglist);
        }

        // DELETE: api/Shoppinglists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppinglist(int id)
        {
            if (_context.Shoppinglists == null)
            {
                return NotFound();
            }
            var shoppinglist = await _context.Shoppinglists.FindAsync(id);
            if (shoppinglist == null)
            {
                return NotFound();
            }

            _context.Shoppinglists.Remove(shoppinglist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppinglistExists(int id)
        {
            return (_context.Shoppinglists?.Any(e => e.IdList == id)).GetValueOrDefault();
        }
    }
}
