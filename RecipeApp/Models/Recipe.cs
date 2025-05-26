using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Recipe Name")]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Display(Name = "Preparation Time (minutes)")]
        public int PreparationTimeMinutes { get; set; }
        
        [Display(Name = "Cooking Time (minutes)")]
        public int CookingTimeMinutes { get; set; }
        
        [Display(Name = "Total Time (minutes)")]
        [NotMapped]
        public int TotalTimeMinutes => PreparationTimeMinutes + CookingTimeMinutes;
        
        [Range(1, 100)]
        public int Servings { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        
        // Foreign keys
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        // Navigation properties
        [Display(Name = "Category")]
        public Category Category { get; set; }
        
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        
        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        
        public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
    }
}