using System.ComponentModel.DataAnnotations;

namespace TestASP.DTO
{
    public class AddCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
