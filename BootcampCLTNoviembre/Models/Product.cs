using System.ComponentModel.DataAnnotations;

namespace BootcampCLTNoviembre.Models
{
    public sealed record Product(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock);

    public sealed class ProductRequest
    {
        [Required]
        [MinLength(1)]

        public required string Name { get; init; }
        public string? Description { get; init; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; init; }
        [Range(0, int.MaxValue)]
        public int Stock { get; init; }
    }
    public sealed class ProductPatchRequest
    {
        [MinLength(1)]
        public string? Name { get; init; }
        public string? Description { get; init; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Price { get; init; }

        [Range(0, int.MaxValue)]
        public int? Stock { get; init; }
    }
   
}
