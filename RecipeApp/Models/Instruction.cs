using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        
        [Required]
        public int StepNumber { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        // Foreign key
        public int RecipeId { get; set; }
        
        // Navigation property
        public Recipe Recipe { get; set; }
    }
}