using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using RecetarioApp.Models;
using RecetarioApp.Models.ModelDTO;
using Swashbuckle.AspNetCore.Annotations;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public IngredientsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ingredients
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los ingredientes")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            return await _context.Ingredients.ToListAsync();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener un ingrediente.", Description = "Obtener un ingediente en especifico con el id")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> PostPhotoOnAzure(IFormFile file)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=imagenesmicocinita;AccountKey=TqI5keL9jgYtw/xvsJS2wHiJWISpY54/cP11IHWZaWUd0/4hmdNZoXteWoAR9QmwvoRGF0FSnf+d+AStuSA/Iw==;EndpointSuffix=core.windows.net";
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            Console.WriteLine("Este es la conexion a azure: " + storageAccount);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var containerName = "imagenesmicocinita";
            var container = blobClient.GetContainerReference(containerName);
            var blobName = file.FileName.Replace(" ","_");
            var blob = container.GetBlockBlobReference(blobName);
            var memoryStream = new MemoryStream();
            using (var stream = file.OpenReadStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
            }
            await blob.UploadFromStreamAsync(memoryStream);
            var blobUrl = blob.Uri.ToString();
            Console.WriteLine("Este es la ruta en azure:" + blobUrl);
            return Ok(new { blobUrl });
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de un ingrediente", Description = "Modificar todas las caracteristicas de un ingrediente en el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutIngredient(int id, IngredientDTO ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);

            if (id != ingredient.IdIngredient)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Crear un ingrediente", Description = "Crear un ingrediente en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<Ingredient>> PostIngredient(IngredientDTO ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
          if (_context.Ingredients == null)
          {
              return Problem("Entity set 'AppDbContext.Ingredients'  is null.");
          }
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.IdIngredient }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar un ingrediente", Description = "Eliminar un ingrediente en el sistema.")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.IdIngredient == id)).GetValueOrDefault();
        }
    }
}
