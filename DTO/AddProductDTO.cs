using System.ComponentModel.DataAnnotations;

namespace TestASP.DTO
{
    public class AddProductDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(10, 99999)]
        public decimal Price { get; set; }

        public AddProductDTO(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
