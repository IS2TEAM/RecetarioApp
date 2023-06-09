﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage;
using RecetarioApp.Models;
using RecetarioApp.Models.ModelDTO;
using Swashbuckle.AspNetCore.Annotations;

namespace RecetarioApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RecipesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Recipes
        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las recetas")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener una receta", Description = "Obtener una receta en especifico")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        [HttpGet("byUser")]
        [SwaggerOperation(Summary = "Obtener recetas por Usuario", Description = "Obtener las receta de un usario con el id de este")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByUser(int idUser)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipessQuery = _context.Recipes.AsQueryable();

            if (idUser > 0)
            {
                recipessQuery = recipessQuery.Where(s => s.UserId.Equals(idUser));
            }
            var recipes = await recipessQuery.ToListAsync();
            return recipes;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modificar todas las caracteristicas de una receta", Description = "Modificar todas las caracteristicas de una receta en el sistema.  \nAgregar el id en ambos campos ya que se necesita para realizar la busqueda y los cambios en la base")]
        public async Task<IActionResult> PutRecipe(int id, RecipeDTO recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);

            if (id != recipe.IdRecipe)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        [HttpPost("uploadImage")]
        public async Task<IActionResult> PostPhotoOnAzure(IFormFile file)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=imagenesmicocinitapp;AccountKey=J7YhFWk6WvglrkoNwmehQC6fgog0NxIeG7bQmk0WbaNPuFOwJPPEoSDtpD10f62F4hBCQ5LUu0Ds+AStQ2DszQ==;EndpointSuffix=core.windows.net";
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            Console.WriteLine("Este es la conexion a azure: " + storageAccount);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var containerName = "imagenes";
            var container = blobClient.GetContainerReference(containerName);
            var blobName = file.FileName.Replace(" ", "_");
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

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(Summary = "Crear una receta", Description = "Crear una receta en el sistema.  \nNo agregar el id ya que este es autoincremental")]
        public async Task<ActionResult<Recipe>> PostRecipe(RecipeDTO recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
          if (_context.Recipes == null)
          {
              return Problem("Entity set 'AppDbContext.Recipes'  is null.");
          }
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.IdRecipe }, recipe);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar una receta", Description = "Eliminar una receta en el sistema.")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var recipeIngredients = await _context.Recipesingredients
                .Where(ri => ri.IdRecipe == id)
                .ToListAsync();

            _context.Recipesingredients.RemoveRange(recipeIngredients);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetLastRecipe")]
        [SwaggerOperation(Summary = "Obtener la última receta")]
        public ActionResult<Recipe> GetLastRecipe()
        {
            var lastRecipe = _context.Recipes.OrderByDescending(x => x.IdRecipe).FirstOrDefault();

            if (lastRecipe == null)
            {
                return NotFound();
            }

            return lastRecipe;
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.IdRecipe == id)).GetValueOrDefault();
        }
    }
}
