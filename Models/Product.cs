using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TestASP.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(10,99999)]
        public decimal Price { get; set; }

        // Foreign Key
        public int? CategoryId { get; set; }
        public Category? Category { get; private set; }

        public Product(string name, decimal price, int category)
        {
            Name = name;
            Price = price;
            CategoryId = category;
        }

        private Product() { } // For EF Core
    }
}
