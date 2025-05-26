using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Quantity { get; set; }
        
        [StringLength(30)]
        public string Unit { get; set; }
        
        // Foreign key
        public int RecipeId { get; set; }
        
        // Navigation property
        public Recipe Recipe { get; set; }
    }
}