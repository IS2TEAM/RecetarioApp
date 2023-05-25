using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetarioApp.Models;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendumsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TiendumsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tiendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiendum>>> GetTienda()
        {
          if (_context.Tienda == null)
          {
              return NotFound();
          }
            return await _context.Tienda.ToListAsync();
        }

        // GET: api/Tiendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tiendum>> GetTiendum(int id)
        {
          if (_context.Tienda == null)
          {
              return NotFound();
          }
            var tiendum = await _context.Tienda.FindAsync(id);

            if (tiendum == null)
            {
                return NotFound();
            }

            return tiendum;
        }

        // PUT: api/Tiendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiendum(int id, Tiendum tiendum)
        {
            if (id != tiendum.IdTienda)
            {
                return BadRequest();
            }

            _context.Entry(tiendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiendumExists(id))
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

        // POST: api/Tiendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tiendum>> PostTiendum(Tiendum tiendum)
        {
          if (_context.Tienda == null)
          {
              return Problem("Entity set 'AppDbContext.Tienda'  is null.");
          }
            _context.Tienda.Add(tiendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiendum", new { id = tiendum.IdTienda }, tiendum);
        }

        // DELETE: api/Tiendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiendum(int id)
        {
            if (_context.Tienda == null)
            {
                return NotFound();
            }
            var tiendum = await _context.Tienda.FindAsync(id);
            if (tiendum == null)
            {
                return NotFound();
            }

            _context.Tienda.Remove(tiendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiendumExists(int id)
        {
            return (_context.Tienda?.Any(e => e.IdTienda == id)).GetValueOrDefault();
        }
    }
}
