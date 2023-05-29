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
    public class IngredientTiendumsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IngredientTiendumsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/IngredientTiendums
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los ingredinetes tienda")]
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
        [SwaggerOperation(Summary = "Obtiene un ingrediente tienda", Description = "Obtener un ingrediente tineda en especifico con el id")]
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
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de un ingrediente tienda", Description = "Modificar todas las caracteristicas de un ingrediente tienda en el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutIngredientTiendum(int id, IngredientTiendumDTO ingredientTiendumDto)
        {
            var ingredientTiendum = _mapper.Map<IngredientTiendum>(ingredientTiendumDto);
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
        [SwaggerOperation(Summary = "Crear un nuevo ingrediente tienda", Description = "Crea un nuevo ingrediente tienda en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<IngredientTiendum>> PostIngredientTiendum(IngredientTiendumDTO ingredientTiendumDto)
        {
            var ingredientTiendum = _mapper.Map<IngredientTiendum>(ingredientTiendumDto);
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
        [SwaggerOperation(Summary = "Eliminar un ingrediente tienda", Description = "Eliminar un ingrediente tienda en el sistema.")]
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
