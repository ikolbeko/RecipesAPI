using Microsoft.Data.Sqlite;
using Dapper;

namespace RecipesAPI;

public class RecipeRepository
{
    private readonly string connectionString;

    public RecipeRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            return connection.Query<Recipe>("SELECT * FROM Recipes");
        }
    }

    public Recipe GetRecipeById(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Recipe>("SELECT * FROM Recipes WHERE Id = @Id", new { Id = id });
        }
    }

    public void AddRecipe(Recipe recipe)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            connection.Execute(@"
                INSERT INTO Recipes (Name, Ingredients, Instructions, PreparationTime, Category, ImageUrl)
                VALUES (@Name, @Ingredients, @Instructions, @PreparationTime, @Category, @ImageUrl)",
                recipe);
        }
    }
}

