using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestASP.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Category(string name)
        {
            Name = name;
        }
         private Category() { } // For EF Core
    }
}
