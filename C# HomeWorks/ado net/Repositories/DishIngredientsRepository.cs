using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;


static class DishIngredientsRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";



    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Добавить ингредиент к блюду.\n");

        int dishId = PromptId("Введите Id блюда: ");
        int ingredientId = PromptId("Введите Id ингредиента: ");

        var di = new DishIngredients { DishId = dishId, IngredientId = ingredientId };

        if (Create(di))
            Console.WriteLine("✅ Ингредиент успешно добавлен к блюду.");
        else
            Console.WriteLine("❌ Ошибка: не удалось добавить ингредиент.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить связь блюда с ингредиентом.\n");

        int dishId = PromptId("Введите Id блюда: ");
        int ingredientId = PromptId("Введите Id ингредиента: ");

        var di = GetById(dishId, ingredientId);
        if (di == null)
            Console.WriteLine("❌ Связь блюда с ингредиентом не найдена.");
        else
        {
            Console.WriteLine($"✅ Найдена связь: Блюдо Id {di.DishId}, Ингредиент Id {di.IngredientId}");
            di.ShowInfo();
        }
           
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все связи блюд с ингредиентами.\n");

        var list = GetAll();
        if (!list.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
            return;
        }

        foreach (var item in list)
            item.ShowInfo();
    }

    public static void UpdateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Обновить ингредиент блюда.\n");

        int dishId = PromptId("Введите Id блюда: ");
        int oldIngredientId = PromptId("Введите текущий Id ингредиента: ");
        int newIngredientId = PromptId("Введите новый Id ингредиента: ");

        if (Update(dishId, oldIngredientId, newIngredientId))
            Console.WriteLine("✅ Ингредиент блюда успешно обновлён.");
        else
            Console.WriteLine("❌ Ошибка: не удалось обновить ингредиент блюда.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить ингредиент из блюда.\n");

        int dishId = PromptId("Введите Id блюда: ");
        int ingredientId = PromptId("Введите Id ингредиента: ");

        Console.Write("Вы уверены, что хотите удалить эту связь? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (confirm is not ("Yes" or "yes" or "Y" or "y"))
        {
            Console.WriteLine("🚫 Удаление отменено.");
            return;
        }

        if (Delete(dishId, ingredientId))
            Console.WriteLine("✅ Связь успешно удалена.");
        else
            Console.WriteLine("❌ Ошибка: не удалось удалить связь.");
    }


    private static bool Create(DishIngredients di)
    {
        const string query = @"INSERT INTO DishIngredients (DishId, IngredientId)
                               VALUES (@DishId, @IngredientId)";

        return ExecuteNonQuery(query,
            new SqlParameter("@DishId", di.DishId),
            new SqlParameter("@IngredientId", di.IngredientId)) > 0;
    }

    private static DishIngredients? GetById(int dishId, int ingredientId)
    {
        const string query = @"SELECT DishId, IngredientId FROM DishIngredients
                               WHERE DishId = @DishId AND IngredientId = @IngredientId";

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@DishId", dishId);
        cmd.Parameters.AddWithValue("@IngredientId", ingredientId);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
            return new DishIngredients
            {
                DishId = reader.GetInt32(0),
                IngredientId = reader.GetInt32(1)
            };
        return null;
    }

    private static IEnumerable<DishIngredients> GetAll()
    {
        const string query = @"SELECT DishId, IngredientId FROM DishIngredients";
        var list = new List<DishIngredients>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new DishIngredients
            {
                DishId = reader.GetInt32(0),
                IngredientId = reader.GetInt32(1)
            });

        return list;
    }

    private static bool Update(int dishId, int oldIngredientId, int newIngredientId)
    {
        const string query = @"UPDATE DishIngredients
                               SET IngredientId = @NewIngredientId
                               WHERE DishId = @DishId AND IngredientId = @OldIngredientId";

        return ExecuteNonQuery(query,
            new SqlParameter("@NewIngredientId", newIngredientId),
            new SqlParameter("@DishId", dishId),
            new SqlParameter("@OldIngredientId", oldIngredientId)) > 0;
    }

    private static bool Delete(int dishId, int ingredientId)
    {
        const string query = @"DELETE FROM DishIngredients
                               WHERE DishId = @DishId AND IngredientId = @IngredientId";

        return ExecuteNonQuery(query,
            new SqlParameter("@DishId", dishId),
            new SqlParameter("@IngredientId", ingredientId)) > 0;
    }


    private static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);

            connection.Open();
            return cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            return 0;
        }
    }

    private static int PromptId(string message)
    {
        Console.Write(message);
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            Console.Write("Ошибка: введите положительное целое число: ");
        return id;
    }
}
