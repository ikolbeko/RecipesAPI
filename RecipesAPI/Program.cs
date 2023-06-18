using Microsoft.Data.Sqlite;

namespace RecipesAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var port = builder.Configuration["PORT"];

        // Add services to the container.
        builder.Services.AddControllers();
        builder.WebHost.UseUrls($"http://*:{port};http://localhost:3000");
        builder.Services.AddScoped<RecipeRepository>(provider => new RecipeRepository("Data Source=recipes.db"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}


/*
var connectionString = "Data Source=recipes.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var createTableQuery = @"
        CREATE TABLE IF NOT EXISTS Recipes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Ingredients TEXT NOT NULL,
            Instructions TEXT NOT NULL,
            PreparationTime TEXT NOT NULL,
            Category TEXT,
            ImageUrl TEXT
        );";

            using (var command = new SqliteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }*/