using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Models;
using System.Collections.Generic;

namespace RecipeApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = new RecipeDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<RecipeDbContext>>());

            // Check if database already has data
            if (context.Recipes.Any())
            {
                return; // Database has been seeded
            }

            // Add categories
            var categories = new List<Category>
            {
                new Category { Name = "Breakfast", Description = "Start your day right with these breakfast recipes" },
                new Category { Name = "Lunch", Description = "Perfect midday meal options" },
                new Category { Name = "Dinner", Description = "Delicious dinner recipes for the whole family" },
                new Category { Name = "Dessert", Description = "Sweet treats to end your meal" },
                new Category { Name = "Appetizer", Description = "Start your meal with these delicious bites" },
                new Category { Name = "Soup", Description = "Warm and comforting soup recipes" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Add tags
            var tags = new List<Tag>
            {
                new Tag { Name = "Quick" },
                new Tag { Name = "Easy" },
                new Tag { Name = "Vegetarian" },
                new Tag { Name = "Vegan" },
                new Tag { Name = "Gluten-Free" },
                new Tag { Name = "Healthy" },
                new Tag { Name = "Low-Carb" },
                new Tag { Name = "Dairy-Free" },
                new Tag { Name = "Spicy" },
                new Tag { Name = "Kid-Friendly" }
            };

            await context.Tags.AddRangeAsync(tags);
            await context.SaveChangesAsync();

            // Add recipes
            var breakfastCategory = await context.Categories.FirstAsync(c => c.Name == "Breakfast");
            var lunchCategory = await context.Categories.FirstAsync(c => c.Name == "Lunch");
            var dinnerCategory = await context.Categories.FirstAsync(c => c.Name == "Dinner");
            var dessertCategory = await context.Categories.FirstAsync(c => c.Name == "Dessert");
            var appetizerCategory = await context.Categories.FirstAsync(c => c.Name == "Appetizer");
            var soupCategory = await context.Categories.FirstAsync(c => c.Name == "Soup");

            var quickTag = await context.Tags.FirstAsync(t => t.Name == "Quick");
            var easyTag = await context.Tags.FirstAsync(t => t.Name == "Easy");
            var vegetarianTag = await context.Tags.FirstAsync(t => t.Name == "Vegetarian");
            var veganTag = await context.Tags.FirstAsync(t => t.Name == "Vegan");
            var glutenFreeTag = await context.Tags.FirstAsync(t => t.Name == "Gluten-Free");
            var healthyTag = await context.Tags.FirstAsync(t => t.Name == "Healthy");
            var lowCarbTag = await context.Tags.FirstAsync(t => t.Name == "Low-Carb");
            var dairyFreeTag = await context.Tags.FirstAsync(t => t.Name == "Dairy-Free");
            var spicyTag = await context.Tags.FirstAsync(t => t.Name == "Spicy");
            var kidFriendlyTag = await context.Tags.FirstAsync(t => t.Name == "Kid-Friendly");

            // Recipe 1: Classic Pancakes
            var pancakes = new Recipe
            {
                Name = "Classic Pancakes",
                Description = "Fluffy and delicious pancakes perfect for a weekend breakfast.",
                PreparationTimeMinutes = 10,
                CookingTimeMinutes = 15,
                Servings = 4,
                CategoryId = breakfastCategory.Id,
                DateAdded = DateTime.Now.AddDays(-10)
            };
            
            await context.Recipes.AddAsync(pancakes);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = pancakes.Id, Name = "All-purpose flour", Quantity = "1 1/2", Unit = "cups" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Baking powder", Quantity = "3 1/2", Unit = "teaspoons" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Salt", Quantity = "1/4", Unit = "teaspoon" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Sugar", Quantity = "1", Unit = "tablespoon" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Milk", Quantity = "1 1/4", Unit = "cups" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Egg", Quantity = "1", Unit = "" },
                new Ingredient { RecipeId = pancakes.Id, Name = "Butter, melted", Quantity = "3", Unit = "tablespoons" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = pancakes.Id, StepNumber = 1, Description = "In a large bowl, sift together the flour, baking powder, salt and sugar." },
                new Instruction { RecipeId = pancakes.Id, StepNumber = 2, Description = "Make a well in the center and pour in the milk, egg and melted butter; mix until smooth." },
                new Instruction { RecipeId = pancakes.Id, StepNumber = 3, Description = "Heat a lightly oiled griddle or frying pan over medium-high heat." },
                new Instruction { RecipeId = pancakes.Id, StepNumber = 4, Description = "Pour or scoop the batter onto the griddle, using approximately 1/4 cup for each pancake." },
                new Instruction { RecipeId = pancakes.Id, StepNumber = 5, Description = "Cook until bubbles form on the surface and the edges are dry, then flip and cook until browned on the other side." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = pancakes.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = pancakes.Id, TagId = kidFriendlyTag.Id }
            );
            
            // Recipe 2: Avocado Toast
            var avocadoToast = new Recipe
            {
                Name = "Avocado Toast",
                Description = "Simple, healthy, and delicious avocado toast, perfect for breakfast or a quick snack.",
                PreparationTimeMinutes = 5,
                CookingTimeMinutes = 5,
                Servings = 2,
                CategoryId = breakfastCategory.Id,
                DateAdded = DateTime.Now.AddDays(-7)
            };
            
            await context.Recipes.AddAsync(avocadoToast);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Whole grain bread", Quantity = "2", Unit = "slices" },
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Ripe avocado", Quantity = "1", Unit = "" },
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Lemon juice", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Red pepper flakes", Quantity = "1/4", Unit = "teaspoon" },
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Salt", Quantity = "To taste", Unit = "" },
                new Ingredient { RecipeId = avocadoToast.Id, Name = "Black pepper", Quantity = "To taste", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = avocadoToast.Id, StepNumber = 1, Description = "Toast the bread slices until golden and crisp." },
                new Instruction { RecipeId = avocadoToast.Id, StepNumber = 2, Description = "Cut the avocado in half, remove the pit, and scoop the flesh into a bowl." },
                new Instruction { RecipeId = avocadoToast.Id, StepNumber = 3, Description = "Add lemon juice, salt, and pepper to the avocado and mash with a fork until desired consistency." },
                new Instruction { RecipeId = avocadoToast.Id, StepNumber = 4, Description = "Spread the avocado mixture onto the toasted bread." },
                new Instruction { RecipeId = avocadoToast.Id, StepNumber = 5, Description = "Sprinkle with red pepper flakes and additional salt and pepper if desired." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = avocadoToast.Id, TagId = quickTag.Id },
                new RecipeTag { RecipeId = avocadoToast.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = avocadoToast.Id, TagId = vegetarianTag.Id },
                new RecipeTag { RecipeId = avocadoToast.Id, TagId = healthyTag.Id }
            );
            
            // Recipe 3: Spaghetti Carbonara
            var carbonara = new Recipe
            {
                Name = "Spaghetti Carbonara",
                Description = "Classic Italian pasta dish with eggs, cheese, pancetta, and black pepper.",
                PreparationTimeMinutes = 10,
                CookingTimeMinutes = 15,
                Servings = 4,
                CategoryId = dinnerCategory.Id,
                DateAdded = DateTime.Now.AddDays(-3)
            };
            
            await context.Recipes.AddAsync(carbonara);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = carbonara.Id, Name = "Spaghetti", Quantity = "1", Unit = "pound" },
                new Ingredient { RecipeId = carbonara.Id, Name = "Pancetta or bacon", Quantity = "8", Unit = "ounces" },
                new Ingredient { RecipeId = carbonara.Id, Name = "Eggs", Quantity = "4", Unit = "large" },
                new Ingredient { RecipeId = carbonara.Id, Name = "Parmesan cheese, grated", Quantity = "1", Unit = "cup" },
                new Ingredient { RecipeId = carbonara.Id, Name = "Black pepper", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = carbonara.Id, Name = "Salt", Quantity = "To taste", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = carbonara.Id, StepNumber = 1, Description = "Bring a large pot of salted water to a boil. Cook the spaghetti according to package directions until al dente." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 2, Description = "Meanwhile, cook the pancetta in a large skillet over medium heat until crispy, about 5 minutes." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 3, Description = "In a bowl, whisk together the eggs, grated Parmesan, and black pepper." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 4, Description = "Drain the spaghetti, reserving about 1/2 cup of pasta water." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 5, Description = "Working quickly, add the hot pasta to the skillet with the pancetta, remove from heat, and pour in the egg mixture. Toss rapidly to coat the pasta and create a creamy sauce." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 6, Description = "If the sauce is too thick, add a splash of the reserved pasta water to thin it out." },
                new Instruction { RecipeId = carbonara.Id, StepNumber = 7, Description = "Serve immediately with additional grated Parmesan and black pepper." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = carbonara.Id, TagId = quickTag.Id }
            );
            
            // Recipe 4: Chocolate Chip Cookies
            var cookies = new Recipe
            {
                Name = "Chocolate Chip Cookies",
                Description = "Classic chocolate chip cookies that are crispy on the outside and chewy on the inside.",
                PreparationTimeMinutes = 15,
                CookingTimeMinutes = 10,
                Servings = 24,
                CategoryId = dessertCategory.Id,
                DateAdded = DateTime.Now.AddDays(-5)
            };
            
            await context.Recipes.AddAsync(cookies);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = cookies.Id, Name = "All-purpose flour", Quantity = "2 1/4", Unit = "cups" },
                new Ingredient { RecipeId = cookies.Id, Name = "Baking soda", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = cookies.Id, Name = "Salt", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = cookies.Id, Name = "Unsalted butter", Quantity = "1", Unit = "cup" },
                new Ingredient { RecipeId = cookies.Id, Name = "Granulated sugar", Quantity = "3/4", Unit = "cup" },
                new Ingredient { RecipeId = cookies.Id, Name = "Brown sugar", Quantity = "3/4", Unit = "cup" },
                new Ingredient { RecipeId = cookies.Id, Name = "Vanilla extract", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = cookies.Id, Name = "Eggs", Quantity = "2", Unit = "large" },
                new Ingredient { RecipeId = cookies.Id, Name = "Chocolate chips", Quantity = "2", Unit = "cups" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = cookies.Id, StepNumber = 1, Description = "Preheat oven to 375°F (190°C)." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 2, Description = "In a small bowl, whisk together the flour, baking soda, and salt." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 3, Description = "In a large bowl, cream together the butter, granulated sugar, and brown sugar until light and fluffy." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 4, Description = "Beat in the vanilla and eggs, one at a time." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 5, Description = "Gradually add the dry ingredients to the wet ingredients, mixing until just combined." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 6, Description = "Fold in the chocolate chips." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 7, Description = "Drop rounded tablespoons of dough onto ungreased baking sheets." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 8, Description = "Bake for 9 to 11 minutes or until golden brown." },
                new Instruction { RecipeId = cookies.Id, StepNumber = 9, Description = "Cool on baking sheets for 2 minutes, then transfer to wire racks to cool completely." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = cookies.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = cookies.Id, TagId = kidFriendlyTag.Id }
            );
            
            // Recipe 5: Vegetable Stir-Fry
            var stirFry = new Recipe
            {
                Name = "Vegetable Stir-Fry",
                Description = "Quick and healthy vegetable stir-fry with a flavorful sauce.",
                PreparationTimeMinutes = 15,
                CookingTimeMinutes = 10,
                Servings = 4,
                CategoryId = dinnerCategory.Id,
                DateAdded = DateTime.Now.AddDays(-2)
            };
            
            await context.Recipes.AddAsync(stirFry);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = stirFry.Id, Name = "Broccoli florets", Quantity = "2", Unit = "cups" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Bell peppers, sliced", Quantity = "2", Unit = "medium" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Carrots, sliced", Quantity = "2", Unit = "medium" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Snow peas", Quantity = "1", Unit = "cup" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Garlic, minced", Quantity = "3", Unit = "cloves" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Ginger, minced", Quantity = "1", Unit = "tablespoon" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Vegetable oil", Quantity = "2", Unit = "tablespoons" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Soy sauce", Quantity = "3", Unit = "tablespoons" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Sesame oil", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Brown sugar", Quantity = "1", Unit = "tablespoon" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Cornstarch", Quantity = "1", Unit = "tablespoon" },
                new Ingredient { RecipeId = stirFry.Id, Name = "Water", Quantity = "1/4", Unit = "cup" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = stirFry.Id, StepNumber = 1, Description = "In a small bowl, whisk together the soy sauce, sesame oil, brown sugar, cornstarch, and water. Set aside." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 2, Description = "Heat vegetable oil in a large wok or skillet over high heat." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 3, Description = "Add the garlic and ginger and stir-fry for 30 seconds until fragrant." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 4, Description = "Add the broccoli and carrots and stir-fry for 2 minutes." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 5, Description = "Add the bell peppers and snow peas and stir-fry for another 2 minutes." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 6, Description = "Pour in the sauce and cook, stirring constantly, until the sauce thickens and the vegetables are coated and crisp-tender, about 2 minutes." },
                new Instruction { RecipeId = stirFry.Id, StepNumber = 7, Description = "Serve hot over rice or noodles." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = stirFry.Id, TagId = quickTag.Id },
                new RecipeTag { RecipeId = stirFry.Id, TagId = vegetarianTag.Id },
                new RecipeTag { RecipeId = stirFry.Id, TagId = veganTag.Id },
                new RecipeTag { RecipeId = stirFry.Id, TagId = healthyTag.Id }
            );
            
            // Recipe 6: Tomato Soup
            var tomatoSoup = new Recipe
            {
                Name = "Homemade Tomato Soup",
                Description = "Comforting and simple tomato soup made from scratch.",
                PreparationTimeMinutes = 10,
                CookingTimeMinutes = 30,
                Servings = 6,
                CategoryId = soupCategory.Id,
                DateAdded = DateTime.Now.AddDays(-15)
            };
            
            await context.Recipes.AddAsync(tomatoSoup);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Olive oil", Quantity = "2", Unit = "tablespoons" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Onion, chopped", Quantity = "1", Unit = "large" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Garlic, minced", Quantity = "3", Unit = "cloves" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Canned crushed tomatoes", Quantity = "28", Unit = "ounces" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Vegetable broth", Quantity = "2", Unit = "cups" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Sugar", Quantity = "1", Unit = "tablespoon" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Dried basil", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Heavy cream", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = tomatoSoup.Id, Name = "Salt and pepper", Quantity = "To taste", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 1, Description = "Heat the olive oil in a large pot over medium heat." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 2, Description = "Add the onion and cook until soft, about 5 minutes." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 3, Description = "Add the garlic and cook for 1 more minute until fragrant." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 4, Description = "Add the crushed tomatoes, vegetable broth, sugar, and dried basil. Stir to combine." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 5, Description = "Bring to a boil, then reduce heat and simmer for 15-20 minutes." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 6, Description = "Allow to cool slightly, then purée using an immersion blender or in batches using a regular blender." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 7, Description = "Return to the pot, stir in the heavy cream, and heat gently. Season with salt and pepper to taste." },
                new Instruction { RecipeId = tomatoSoup.Id, StepNumber = 8, Description = "Serve hot with grilled cheese sandwiches or crusty bread." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = tomatoSoup.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = tomatoSoup.Id, TagId = vegetarianTag.Id }
            );
            
            // Recipe 7: Caesar Salad
            var caesarSalad = new Recipe
            {
                Name = "Classic Caesar Salad",
                Description = "Fresh and crisp Caesar salad with homemade dressing.",
                PreparationTimeMinutes = 15,
                CookingTimeMinutes = 10,
                Servings = 4,
                CategoryId = lunchCategory.Id,
                DateAdded = DateTime.Now.AddDays(-8)
            };
            
            await context.Recipes.AddAsync(caesarSalad);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Romaine lettuce", Quantity = "2", Unit = "heads" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Parmesan cheese, grated", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Croutons", Quantity = "2", Unit = "cups" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Garlic, minced", Quantity = "2", Unit = "cloves" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Anchovy paste", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Lemon juice", Quantity = "2", Unit = "tablespoons" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Dijon mustard", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Worcestershire sauce", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Egg yolk", Quantity = "1", Unit = "" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Olive oil", Quantity = "1/3", Unit = "cup" },
                new Ingredient { RecipeId = caesarSalad.Id, Name = "Salt and pepper", Quantity = "To taste", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 1, Description = "Wash and dry the romaine lettuce leaves. Tear into bite-sized pieces and place in a large bowl." },
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 2, Description = "In a small bowl, whisk together the garlic, anchovy paste, lemon juice, Dijon mustard, Worcestershire sauce, and egg yolk." },
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 3, Description = "Slowly drizzle in the olive oil while whisking continuously to form an emulsion." },
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 4, Description = "Season with salt and pepper to taste." },
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 5, Description = "Pour the dressing over the lettuce and toss to coat evenly." },
                new Instruction { RecipeId = caesarSalad.Id, StepNumber = 6, Description = "Add the grated Parmesan cheese and croutons, toss lightly, and serve immediately." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = caesarSalad.Id, TagId = quickTag.Id }
            );
            
            // Recipe 8: Spinach and Artichoke Dip
            var spinachDip = new Recipe
            {
                Name = "Spinach and Artichoke Dip",
                Description = "Creamy and delicious spinach and artichoke dip, perfect for parties.",
                PreparationTimeMinutes = 10,
                CookingTimeMinutes = 25,
                Servings = 8,
                CategoryId = appetizerCategory.Id,
                DateAdded = DateTime.Now.AddDays(-12)
            };
            
            await context.Recipes.AddAsync(spinachDip);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = spinachDip.Id, Name = "Frozen spinach, thawed", Quantity = "10", Unit = "ounces" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Artichoke hearts, chopped", Quantity = "14", Unit = "ounces" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Cream cheese", Quantity = "8", Unit = "ounces" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Sour cream", Quantity = "1/4", Unit = "cup" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Mayonnaise", Quantity = "1/4", Unit = "cup" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Garlic, minced", Quantity = "2", Unit = "cloves" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Parmesan cheese, grated", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Mozzarella cheese, shredded", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = spinachDip.Id, Name = "Salt and pepper", Quantity = "To taste", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 1, Description = "Preheat oven to 350°F (175°C)." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 2, Description = "Squeeze excess moisture from the thawed spinach." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 3, Description = "In a large bowl, mix the cream cheese, sour cream, mayonnaise, garlic, and Parmesan cheese until smooth." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 4, Description = "Fold in the spinach, artichoke hearts, and half the mozzarella cheese. Season with salt and pepper." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 5, Description = "Transfer to a baking dish and sprinkle the remaining mozzarella cheese on top." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 6, Description = "Bake for 20-25 minutes until bubbly and golden on top." },
                new Instruction { RecipeId = spinachDip.Id, StepNumber = 7, Description = "Serve hot with tortilla chips, crackers, or bread." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = spinachDip.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = spinachDip.Id, TagId = vegetarianTag.Id }
            );
            
            // Recipe 9: Beef Tacos
            var beefTacos = new Recipe
            {
                Name = "Beef Tacos",
                Description = "Delicious and simple beef tacos with all the toppings.",
                PreparationTimeMinutes = 15,
                CookingTimeMinutes = 15,
                Servings = 6,
                CategoryId = dinnerCategory.Id,
                DateAdded = DateTime.Now.AddDays(-6)
            };
            
            await context.Recipes.AddAsync(beefTacos);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = beefTacos.Id, Name = "Ground beef", Quantity = "1", Unit = "pound" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Onion, diced", Quantity = "1", Unit = "medium" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Garlic, minced", Quantity = "2", Unit = "cloves" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Taco seasoning", Quantity = "2", Unit = "tablespoons" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Water", Quantity = "1/3", Unit = "cup" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Taco shells", Quantity = "12", Unit = "" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Lettuce, shredded", Quantity = "2", Unit = "cups" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Tomatoes, diced", Quantity = "2", Unit = "medium" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Cheddar cheese, shredded", Quantity = "1", Unit = "cup" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Sour cream", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Salsa", Quantity = "1", Unit = "cup" },
                new Ingredient { RecipeId = beefTacos.Id, Name = "Avocado, sliced", Quantity = "1", Unit = "" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 1, Description = "In a large skillet over medium-high heat, brown the ground beef and onion until the beef is no longer pink, about 5-7 minutes." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 2, Description = "Add the garlic and cook for 1 minute more." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 3, Description = "Drain excess fat if necessary." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 4, Description = "Stir in the taco seasoning and water. Simmer for 3-4 minutes until slightly thickened." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 5, Description = "Heat the taco shells according to package instructions." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 6, Description = "Fill the shells with the beef mixture and top with lettuce, tomatoes, cheese, sour cream, salsa, and avocado." },
                new Instruction { RecipeId = beefTacos.Id, StepNumber = 7, Description = "Serve immediately." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = beefTacos.Id, TagId = quickTag.Id },
                new RecipeTag { RecipeId = beefTacos.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = beefTacos.Id, TagId = kidFriendlyTag.Id }
            );
            
            // Recipe 10: Banana Bread
            var bananaBread = new Recipe
            {
                Name = "Banana Bread",
                Description = "Moist and delicious banana bread, perfect for using up overripe bananas.",
                PreparationTimeMinutes = 15,
                CookingTimeMinutes = 60,
                Servings = 10,
                CategoryId = dessertCategory.Id,
                DateAdded = DateTime.Now.AddDays(-9)
            };
            
            await context.Recipes.AddAsync(bananaBread);
            await context.SaveChangesAsync();
            
            await context.Ingredients.AddRangeAsync(
                new Ingredient { RecipeId = bananaBread.Id, Name = "Overripe bananas", Quantity = "3", Unit = "large" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "All-purpose flour", Quantity = "2", Unit = "cups" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Baking soda", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Salt", Quantity = "1/4", Unit = "teaspoon" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Butter", Quantity = "1/2", Unit = "cup" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Brown sugar", Quantity = "3/4", Unit = "cup" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Eggs", Quantity = "2", Unit = "large" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Vanilla extract", Quantity = "1", Unit = "teaspoon" },
                new Ingredient { RecipeId = bananaBread.Id, Name = "Walnuts, chopped", Quantity = "1/2", Unit = "cup" }
            );
            
            await context.Instructions.AddRangeAsync(
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 1, Description = "Preheat oven to 350°F (175°C). Grease a 9x5 inch loaf pan." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 2, Description = "In a medium bowl, combine flour, baking soda, and salt." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 3, Description = "In a large bowl, cream together butter and brown sugar until light and fluffy." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 4, Description = "Beat in the eggs one at a time, then stir in vanilla extract." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 5, Description = "Mash the bananas and add to the butter mixture." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 6, Description = "Add the flour mixture to the banana mixture and stir just until combined." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 7, Description = "Fold in the walnuts." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 8, Description = "Pour the batter into the prepared loaf pan." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 9, Description = "Bake for 60 minutes, or until a toothpick inserted into the center comes out clean." },
                new Instruction { RecipeId = bananaBread.Id, StepNumber = 10, Description = "Let bread cool in pan for 10 minutes, then turn out onto a wire rack to cool completely." }
            );
            
            await context.RecipeTags.AddRangeAsync(
                new RecipeTag { RecipeId = bananaBread.Id, TagId = easyTag.Id },
                new RecipeTag { RecipeId = bananaBread.Id, TagId = kidFriendlyTag.Id },
                new RecipeTag { RecipeId = bananaBread.Id, TagId = vegetarianTag.Id }
            );
            
            await context.SaveChangesAsync();
        }
    }
}