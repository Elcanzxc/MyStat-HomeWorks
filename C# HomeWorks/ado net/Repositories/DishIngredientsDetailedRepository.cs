using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;

static class DishIngredientsDetailedRepository
{
    private static readonly string connectionString ="Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";


    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все блюда с ингредиентами!\n");

        var detailedList = GetAll();

        if (!detailedList.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
            return;
        }

        foreach (var item in detailedList)
            item.ShowInfo();
    }

    private static IEnumerable<DishIngredientsDetailed> GetAll()
    {
        const string query = @"
            SELECT 
                d.DishId, d.Name, d.Price,
                i.IngredientId, i.Name
            FROM DishIngredients di
            INNER JOIN Dishes d ON di.DishId = d.DishId
            INNER JOIN Ingredients i ON di.IngredientId = i.IngredientId";

        var list = new List<DishIngredientsDetailed>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var dish = new Dishes
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            };

            var ingredient = new Ingredients
            {
                Id = reader.GetInt32(3),
                Name = reader.GetString(4)
            };

            list.Add(new DishIngredientsDetailed
            {
                Dishes = dish,
                Ingredients = ingredient
            });
        }

        return list;
    }
}
