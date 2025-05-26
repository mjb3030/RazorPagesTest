using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RecipeApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        // Navigation property
        public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
    }
}