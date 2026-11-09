using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace BootcampCLTNoviembre.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProductsController : Controller
    {
        private static readonly List<Product> Products =
        [
            new(1, "Notebook", "Notebook para desarrollo", 1200.00m, 10),
            new(2, "Mouse", "Mouse inalámbrico", 25.50m, 30)
        ];

        private static int _nextId = 2;

        [HttpGet]
        [ProducesResponseType<IEnumerable<Product>>(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(Products);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetById(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);
            return product is null ? NotFound() : Ok(product);
        }

        public sealed record Product(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock);
    }
}
