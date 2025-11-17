using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstApproach.Context;
using DBFirstApproach.Models;

namespace DBFirstApproach.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Example 1: Call stored procedure
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetProductDetails(long id)
        {
            var result = await _context.GetProductDetailsAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Example 2: Use scalar function in LINQ query
        [HttpGet("{id}/final-price")]
        public async Task<IActionResult> GetFinalPrice(long id, long colorId, long sizeId)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    OriginalPrice = p.Price,
                    FinalPrice = _context.GetProductFinalPrice(id, colorId, sizeId),
                    CommentCount = _context.GetProductCommentCount(id)
                })
                .FirstOrDefaultAsync();

            return Ok(product);
        }

        // Example 3: Call SP with no return value
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(long userId, string productIds)
        {
            var rowsAffected = await _context.CreateOrderAsync(userId, productIds);
            return Ok(new { Success = rowsAffected > 0 });
        }

        // Example 4: Query active products from Products table
        [HttpGet("active-products")]
        public async Task<IActionResult> GetActiveProducts()
        {
            // Query active products (not deleted) from the Products table
            var products = await _context.Products
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Id) // Add ordering to avoid the warning
                .Take(10)
                .ToListAsync();

            return Ok(products);
        }
    }
}