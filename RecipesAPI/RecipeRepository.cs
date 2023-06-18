using Npgsql;
using Dapper;
using dotenv.net;

namespace RecipesAPI;

public class RecipeRepository
{
    private readonly string connectionString;

    public RecipeRepository()
    {
        DotEnv.Load();
        this.connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("PGHOST"),
            Port = Convert.ToInt32(Environment.GetEnvironmentVariable("PGPORT")),
            Database = Environment.GetEnvironmentVariable("PGDATABASE"),
            Username = Environment.GetEnvironmentVariable("PGUSER"),
            Password = Environment.GetEnvironmentVariable("PGPASSWORD"),
        }.ConnectionString;
    }

    public IEnumerable<Recipe> GetAllRecipes()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            return connection.Query<Recipe>("SELECT * FROM recipes");
        }
    }

    public Recipe GetRecipeById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            return connection.QueryFirstOrDefault<Recipe>("SELECT * FROM recipes WHERE id = @Id", new { Id = id });
        }
    }

    public void AddRecipe(Recipe recipe)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            connection.Execute(@"INSERT INTO recipes (title, description, ingredients, instructions, image, category, created_at)
                                    VALUES (@Title, @Description, @Ingredients, @Instructions, @Image, @Category, @CreatedAt)",
                                    recipe);
        }
    }
}
