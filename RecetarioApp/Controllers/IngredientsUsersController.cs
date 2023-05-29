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
    public class IngredientsUsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IngredientsUsersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/IngredientsUsers
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los ingredientes de usuarios")]
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
        [SwaggerOperation(Summary = "Obtiene un ingredientes de un usuario", Description = "Obtiene un ingredientes de un usuario en especifico con el id")]
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
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de un ingrediente de usuario", Description = "Modificar todas las caracteristicas de un ingrediente de usuarioen el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutIngredientsUser(int id, IngredientsUserDTO ingredientsUserDto)
        {
            var ingredientsUser = _mapper.Map<IngredientsUser>(ingredientsUserDto);
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
        [SwaggerOperation(Summary = "Crear un nuevo ingrediente de ususario", Description = "Crea un ingrediente usuario en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<IngredientsUser>> PostIngredientsUser(IngredientsUserDTO ingredientsUserDto)
        {
            var ingredientsUser = _mapper.Map<IngredientsUser>(ingredientsUserDto);
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
        [SwaggerOperation(Summary = "Eliminar un ingrediente usuario", Description = "Eliminar un ingrediente usuario en el sistema.")]
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
