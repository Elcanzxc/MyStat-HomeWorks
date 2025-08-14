using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;
static class IngredientsRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";

   

    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Создать ингредиент.\n");

        string name = PromptRequired("Введите название ингредиента: ");

        var ingredient = new Ingredients { Name = name };

        if (Create(ingredient))
            Console.WriteLine("✅ Ингредиент успешно добавлен.");
        else
            Console.WriteLine("❌ Ошибка: не удалось добавить ингредиент.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить ингредиент по Id.\n");

        int id = PromptId("Введите Id ингредиента: ");

        var ingredient = GetById(id);
        if (ingredient == null)
            Console.WriteLine($"❌ Ингредиент с Id {id} не найден.");
        else
        {
            Console.WriteLine($"✅ Найден ингредиент: {ingredient.Name} (Id: {ingredient.Id})");
            ingredient.ShowInfo();
        }
            
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все ингредиенты.\n");

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
        Console.WriteLine("Вы выбрали операцию: Обновить ингредиент.\n");

        int id = PromptId("Введите Id ингредиента: ");

        var existing = GetById(id);
        if (existing == null)
        {
            Console.WriteLine($"❌ Ингредиент с Id {id} не найден.");
            return;
        }

        Console.WriteLine($"Текущее название: {existing.Name}");
        string newName = PromptRequired("Введите новое название ингредиента: ");

        var updated = new Ingredients { Id = id, Name = newName };

        if (Update(updated))
            Console.WriteLine("✅ Ингредиент успешно обновлён.");
        else
            Console.WriteLine("❌ Ошибка при обновлении ингредиента.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить ингредиент.\n");

        int id = PromptId("Введите Id ингредиента: ");

        var existing = GetById(id);
        if (existing == null)
        {
            Console.WriteLine($"❌ Ингредиент с Id {id} не найден.");
            return;
        }

        Console.Write($"Вы уверены, что хотите удалить ингредиент {existing.Name}? (д/н): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (confirm is not ("Yes" or "yes" or "Y" or "y"))
        {
            Console.WriteLine("🚫 Удаление отменено.");
            return;
        }

        if (Delete(id))
            Console.WriteLine("✅ Ингредиент успешно удалён.");
        else
            Console.WriteLine("❌ Ошибка при удалении ингредиента.");
    }



    private static bool Create(Ingredients ingredient)
    {
        const string query = @"INSERT INTO Ingredients (Name) VALUES (@Name)";

        return ExecuteNonQuery(query, new SqlParameter("@Name", ingredient.Name)) > 0;
    }

    private static Ingredients? GetById(int id)
    {
        const string query = @"SELECT IngredientId, Name FROM Ingredients WHERE IngredientId = @Id";

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Ingredients
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    private static IEnumerable<Ingredients> GetAll()
    {
        const string query = @"SELECT IngredientId, Name FROM Ingredients";
        var list = new List<Ingredients>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Ingredients
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }

    private static bool Update(Ingredients ingredient)
    {
        const string query = @"UPDATE Ingredients SET Name = @Name WHERE IngredientId = @Id";

        return ExecuteNonQuery(query,
            new SqlParameter("@Id", ingredient.Id),
            new SqlParameter("@Name", ingredient.Name)) > 0;
    }

    private static bool Delete(int id)
    {
        const string query = @"DELETE FROM Ingredients WHERE IngredientId = @Id";

        return ExecuteNonQuery(query, new SqlParameter("@Id", id)) > 0;
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

    private static string PromptRequired(string message)
    {
        Console.Write(message);
        string? input;
        while (string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            Console.Write("Ошибка: значение не может быть пустым. Повторите: ");
        return input.Trim();
    }
}
