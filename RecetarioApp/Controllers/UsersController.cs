using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public UsersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los usuarios")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un usuario", Description = "Obtener un usuario en especifico con el id")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de un usuario", Description = "Modificar todas las caracteristicas de un usuario sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo usuario", Description = "Crea un nuevo usuario en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<User>> PostUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (_context.Users == null)
          {
              return Problem("Entity set 'AppDbContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar un usuario", Description = "Eliminar un usuario en el sistema.")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Login")]
        [SwaggerOperation(Summary = "Verificar las credenciales de inicio de sesión", Description = "Verifica si las credenciales de inicio de sesión son válidas.")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailUser == email && u.Password == password);

            if (user == null)
            {
                return NotFound(new { validation = false});
            }

            return Ok(new { validacion = true});
        }


        [HttpGet("getIdByCredentials")]
        [SwaggerOperation(Summary = "Verificar las credenciales de inicio de sesión", Description = "Verifica si las credenciales de inicio de sesión son válidas.")]
        public async Task<ActionResult<int>> getIdByCredentials(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailUser == email && u.Password == password);

            if (user != null)
            {
                return Ok(new { id = user.IdUser });
            }

            return NotFound(new { id = -1});
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
