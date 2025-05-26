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
    public class EditModel : PageModel
    {
        private readonly RecipeDbContext _context;

        public EditModel(RecipeDbContext context)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.RecipeTags)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Recipe == null)
            {
                return NotFound();
            }
            
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name");
            
            // Populate ingredient inputs
            foreach (var ingredient in Recipe.Ingredients)
            {
                IngredientInputs.Add(new IngredientInput
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit
                });
            }
            
            // Add empty ingredient input if none exists
            if (!IngredientInputs.Any())
            {
                IngredientInputs.Add(new IngredientInput());
            }
            
            // Populate instruction inputs
            foreach (var instruction in Recipe.Instructions.OrderBy(i => i.StepNumber))
            {
                InstructionInputs.Add(new InstructionInput
                {
                    Id = instruction.Id,
                    StepNumber = instruction.StepNumber,
                    Description = instruction.Description
                });
            }
            
            // Add empty instruction input if none exists
            if (!InstructionInputs.Any())
            {
                InstructionInputs.Add(new InstructionInput { StepNumber = 1 });
            }
            
            // Get selected tag IDs
            SelectedTagIds = Recipe.RecipeTags.Select(rt => rt.TagId).ToList();
            
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

            var recipeToUpdate = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .Include(r => r.RecipeTags)
                .FirstOrDefaultAsync(r => r.Id == Recipe.Id);

            if (recipeToUpdate == null)
            {
                return NotFound();
            }

            // Update recipe basic properties
            recipeToUpdate.Name = Recipe.Name;
            recipeToUpdate.Description = Recipe.Description;
            recipeToUpdate.PreparationTimeMinutes = Recipe.PreparationTimeMinutes;
            recipeToUpdate.CookingTimeMinutes = Recipe.CookingTimeMinutes;
            recipeToUpdate.Servings = Recipe.Servings;
            recipeToUpdate.CategoryId = Recipe.CategoryId;
            
            // Update ingredients
            // First, remove all existing ingredients
            _context.Ingredients.RemoveRange(recipeToUpdate.Ingredients);
            
            // Then add all ingredients from the form
            foreach (var ingredientInput in IngredientInputs)
            {
                if (!string.IsNullOrWhiteSpace(ingredientInput.Name))
                {
                    _context.Ingredients.Add(new Ingredient
                    {
                        RecipeId = recipeToUpdate.Id,
                        Name = ingredientInput.Name,
                        Quantity = ingredientInput.Quantity,
                        Unit = ingredientInput.Unit
                    });
                }
            }
            
            // Update instructions
            // First, remove all existing instructions
            _context.Instructions.RemoveRange(recipeToUpdate.Instructions);
            
            // Then add all instructions from the form
            foreach (var instructionInput in InstructionInputs.OrderBy(i => i.StepNumber))
            {
                if (!string.IsNullOrWhiteSpace(instructionInput.Description))
                {
                    _context.Instructions.Add(new Instruction
                    {
                        RecipeId = recipeToUpdate.Id,
                        StepNumber = instructionInput.StepNumber,
                        Description = instructionInput.Description
                    });
                }
            }
            
            // Update tags
            // First, remove all existing recipe tags
            _context.RecipeTags.RemoveRange(recipeToUpdate.RecipeTags);
            
            // Then add all tags from the form
            foreach (var tagId in SelectedTagIds)
            {
                _context.RecipeTags.Add(new RecipeTag
                {
                    RecipeId = recipeToUpdate.Id,
                    TagId = tagId
                });
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
        
        public class IngredientInput
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Quantity { get; set; }
            public string Unit { get; set; }
        }
        
        public class InstructionInput
        {
            public int Id { get; set; }
            public int StepNumber { get; set; }
            public string Description { get; set; }
        }
    }
}