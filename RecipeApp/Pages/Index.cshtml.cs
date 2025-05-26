using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Data;
using RecipeApp.Models;
using System.Linq;

namespace RecipeApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly RecipeDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, RecipeDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int RecipeCount { get; set; }
    public int CategoryCount { get; set; }
    public Recipe LatestRecipe { get; set; }

    public IActionResult OnGet()
    {
        RecipeCount = _context.Recipes.Count();
        CategoryCount = _context.Categories.Count();
        LatestRecipe = _context.Recipes
            .OrderByDescending(r => r.DateAdded)
            .FirstOrDefault();

        return Page();
    }
}
