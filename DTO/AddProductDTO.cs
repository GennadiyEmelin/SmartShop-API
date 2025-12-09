using System.ComponentModel.DataAnnotations;

namespace TestASP.DTO
{
    public class AddProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(10, 99999)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
