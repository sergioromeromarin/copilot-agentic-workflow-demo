using CopilotDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CopilotDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new();
        private readonly ProductCatalogOptions _options;

        public ProductsController(IOptions<ProductCatalogOptions> options)
        {
            _options = options.Value;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var currency = string.IsNullOrWhiteSpace(dto.Currency) ? _options.DefaultCurrency : dto.Currency;
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Currency = currency!
            };
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }

    public class ProductCatalogOptions
    {
        public string DefaultCurrency { get; set; } = "USD";
    }
}
