using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly RecipeDbContext _context;

        public CreateModel(RecipeDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }
        
        [BindProperty]
        public List<IngredientInput> IngredientInputs { get; set; } = new List<IngredientInput>();
        
        [BindProperty]
        public List<InstructionInput> InstructionInputs { get; set; } = new List<InstructionInput>();
        
        [BindProperty]
        public List<int> SelectedTagIds { get; set; } = new List<int>();
        
        public SelectList Categories { get; set; }
        public SelectList Tags { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Add 3 empty ingredient inputs
            for (int i = 0; i < 3; i++)
            {
                IngredientInputs.Add(new IngredientInput());
            }
            
            // Add 3 empty instruction inputs
            for (int i = 0; i < 3; i++)
            {
                InstructionInputs.Add(new InstructionInput { StepNumber = i + 1 });
            }
            
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
                Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name");
                return Page();
            }

            // Set date added
            Recipe.DateAdded = DateTime.Now;
            
            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();
            
            // Add ingredients
            foreach (var ingredientInput in IngredientInputs)
            {
                if (!string.IsNullOrWhiteSpace(ingredientInput.Name))
                {
                    _context.Ingredients.Add(new Ingredient
                    {
                        RecipeId = Recipe.Id,
                        Name = ingredientInput.Name,
                        Quantity = ingredientInput.Quantity,
                        Unit = ingredientInput.Unit
                    });
                }
            }
            
            // Add instructions
            foreach (var instructionInput in InstructionInputs.OrderBy(i => i.StepNumber))
            {
                if (!string.IsNullOrWhiteSpace(instructionInput.Description))
                {
                    _context.Instructions.Add(new Instruction
                    {
                        RecipeId = Recipe.Id,
                        StepNumber = instructionInput.StepNumber,
                        Description = instructionInput.Description
                    });
                }
            }
            
            // Add recipe tags
            foreach (var tagId in SelectedTagIds)
            {
                _context.RecipeTags.Add(new RecipeTag
                {
                    RecipeId = Recipe.Id,
                    TagId = tagId
                });
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
        
        public class IngredientInput
        {
            public string Name { get; set; }
            public string Quantity { get; set; }
            public string Unit { get; set; }
        }
        
        public class InstructionInput
        {
            public int StepNumber { get; set; }
            public string Description { get; set; }
        }
    }
}