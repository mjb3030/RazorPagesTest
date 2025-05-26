using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly RecipeDbContext _context;

        public IndexModel(RecipeDbContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipes { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string IngredientSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public SelectList Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CategorySort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync()
        {
            // For sorting
            NameSort = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            DateSort = SortOrder == "Date" ? "date_desc" : "Date";
            CategorySort = SortOrder == "Category" ? "category_desc" : "Category";

            CurrentFilter = SearchString;

            // Load categories for filter dropdown
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

            // Start with all recipes and include navigation properties
            IQueryable<Recipe> recipesQuery = _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag);

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(SearchString))
            {
                recipesQuery = recipesQuery.Where(r => 
                    r.Name.Contains(SearchString) || 
                    r.Description.Contains(SearchString));
            }

            // Apply category filter if provided
            if (CategoryId.HasValue)
            {
                recipesQuery = recipesQuery.Where(r => r.CategoryId == CategoryId.Value);
            }

            // Apply ingredient search if provided
            if (!string.IsNullOrEmpty(IngredientSearch))
            {
                // Split the ingredient search string by commas or spaces
                var ingredients = IngredientSearch.Split(new char[] { ',', ' ' }, 
                    System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim().ToLower());
                
                // Count how many of the requested ingredients each recipe has
                var recipeMatches = new Dictionary<int, int>();
                
                foreach (var recipe in await recipesQuery.ToListAsync())
                {
                    var recipeIngredients = recipe.Ingredients
                        .Select(i => i.Name.ToLower())
                        .ToList();
                    
                    int matchCount = ingredients.Count(i => recipeIngredients.Any(ri => ri.Contains(i)));
                    
                    if (matchCount > 0)
                    {
                        recipeMatches[recipe.Id] = matchCount;
                    }
                }
                
                // Filter to only recipes with at least one matching ingredient
                recipesQuery = recipesQuery.Where(r => recipeMatches.ContainsKey(r.Id));
                
                // Get the recipes and sort by number of matching ingredients (descending)
                var recipesWithMatches = await recipesQuery.ToListAsync();
                Recipes = recipesWithMatches.OrderByDescending(r => recipeMatches[r.Id]).ToList();
                return;
            }

            // Apply sorting
            recipesQuery = SortOrder switch
            {
                "name_desc" => recipesQuery.OrderByDescending(r => r.Name),
                "Date" => recipesQuery.OrderBy(r => r.DateAdded),
                "date_desc" => recipesQuery.OrderByDescending(r => r.DateAdded),
                "Category" => recipesQuery.OrderBy(r => r.Category.Name),
                "category_desc" => recipesQuery.OrderByDescending(r => r.Category.Name),
                _ => recipesQuery.OrderBy(r => r.Name),
            };

            Recipes = await recipesQuery.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}