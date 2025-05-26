using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RecipeApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }
        
        // Navigation property
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}