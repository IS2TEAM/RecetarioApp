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
        [SwaggerOperation(Summary = "Obtener todas las tiendas")]
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
        [SwaggerOperation(Summary = "Obtiene una tienda", Description = "Obtener una tienda en especifico con el id")]
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
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de una tienda", Description = "Modificar todas las caracteristicas de una tienda en el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutTiendum(int id, Tiendum tiendum)
        {
            if (id != tiendum.IdTiendum)
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
        [SwaggerOperation(Summary = "Crear una nueva tienda", Description = "Crea una nueva tienda en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<Tiendum>> PostTiendum(Tiendum tiendum)
        {
          if (_context.Tienda == null)
          {
              return Problem("Entity set 'AppDbContext.Tienda'  is null.");
          }
            _context.Tienda.Add(tiendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiendum", new { id = tiendum.IdTiendum }, tiendum);
        }

        // DELETE: api/Tiendums/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar una tienda", Description = "Eliminar una tienda en el sistema.")]
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
            return (_context.Tienda?.Any(e => e.IdTiendum == id)).GetValueOrDefault();
        }
    }
}
