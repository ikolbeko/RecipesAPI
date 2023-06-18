using Microsoft.AspNetCore.Mvc;

namespace RecipesAPI.Controllers;

[Route("api/recipes")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly RecipeRepository recipeRepository;

    public RecipesController(RecipeRepository recipeRepository)
    {
        this.recipeRepository = recipeRepository;
    }


    [HttpGet]
    public IActionResult GetAllRecipes()
    {
        var recipes = recipeRepository.GetAllRecipes();
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public IActionResult GetRecipeById(int id)
    {
        var recipe = recipeRepository.GetRecipeById(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    [HttpPost]
    public IActionResult AddRecipe([FromBody] Recipe recipe)
    {
        recipeRepository.AddRecipe(recipe);
        return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
    }
}
