using System;
using System.Linq;
using System.Threading.Tasks;
using Shivonet.MobileShop.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shivonet.MobileShop.API.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CatalogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? 
                throw new ArgumentNullException(nameof(appDbContext));

            appDbContext.ChangeTracker.QueryTrackingBehavior = 
                QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Products([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var items = await _appDbContext.Products
                              .OrderBy(p => p.Name)
                              .Include(p => p.Category)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ProductsOfTheWeek()
        {
            var items = await _appDbContext.Products.Where(p => p.IsProductOfTheWeek).OrderBy(p => p.Name)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("products/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var product = await _appDbContext.Products.Where(p => p.ProductId == id).SingleOrDefaultAsync();
            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}
