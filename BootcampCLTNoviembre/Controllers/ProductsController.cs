using Asp.Versioning;
using BootcampCLTNoviembre.Models;
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
        //get
        [HttpGet("{id:int}")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetById(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);
            return product is null ? NotFound() : Ok(product);
        }

        //post
        [HttpPost]
        [ProducesResponseType<Product>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create(ProductRequest request)
        {
            var product = new Product(
                ++_nextId,
                request.Name.Trim(),
                request.Description,
                request.Price,
                request.Stock);
            Products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        //put
        [HttpPut("{id:int}")] //recibimos id
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Update(int id, ProductRequest request) //recibimos id y los datos nuevos en el request
        {
            var index = Products.FindIndex(product => product.Id == id); //buscamos la posicion del producto

            if (index == -1) //si su posicion retorna -1 es que no encontró
                return NotFound();

            var product = Products[index]; //asignamos/obtenemos a product el producto actual

            var updatedProduct = product with  //creamos el producto actualizado, with crea una copia y cambia solo las propiedades que le indicamos
            {
                Name = request.Name.Trim(),
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            Products[index] = updatedProduct; //reemplazamos el producto en la posicion que le corresponde

            return Ok(updatedProduct); //devolvemos el resultado 
        }
        // patch
        [HttpPatch("{id:int}")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Patch(int id, ProductPatchRequest request) //usamos product patch ya que lo preparamos para poder enviar datos parciales permitiendo valores que no cambiaran con ?
        {
            var index = Products.FindIndex(product => product.Id == id);

            if (index == -1)
                return NotFound();

            var product = Products[index];

            var updatedProduct = product with
            {
                Name = request.Name?.Trim() ?? product.Name,
                Description = request.Description ?? product.Description,
                Price = request.Price ?? product.Price, // con ?? le indicamos que request.Price tiene un valor, utilizamos ese valor, pero si request.Price es null, conservamos el precio anterior
                Stock = request.Stock ?? product.Stock
            };

            Products[index] = updatedProduct; //guardamos cambios

            return Ok(updatedProduct); //devolvemos producto actualizado
        }
        //delete
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) //recibimos id
        {
            var product = Products.FirstOrDefault(product => product.Id == id);   //buscamos el producto por el id

            if (product is null)   //comprobamos si existe, si no devuelve 404
                return NotFound();

            Products.Remove(product); //si existe elimina el producto

            return NoContent();   //devolvemos no content (204) eso nos indica que la operación fue exitosa y no necesitamos devolver contenido
        }

        public sealed record Product(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock);
    }
}
