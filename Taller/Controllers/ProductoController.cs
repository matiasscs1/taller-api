using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Models;

namespace Taller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BrandContext _dbContext;

        public ProductoController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Producto>>> GetProducts()

        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            return await _dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Producto>> GetProduct(int ID)

        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            var producto = await _dbContext.Products.FindAsync(ID);

            if (producto == null)
            {
                return NotFound();
            }
            return producto;


        }

        [HttpPost]

        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _dbContext.Products.Add(producto);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = producto.Id }, producto);
        }

        [HttpPut]

        public async Task<ActionResult> PutBrand(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(producto).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();


        }


        private bool BrandAvailable(int id)
        {
            return (_dbContext.Products?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.Products.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
