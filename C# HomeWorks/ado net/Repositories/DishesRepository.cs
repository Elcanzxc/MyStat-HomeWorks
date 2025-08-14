
using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;



static class DishesRepository
{
    private static readonly string connectionString ="Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";


    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Создать блюдо.\n");

        string name = PromptRequired("Введите название блюда: ");
        decimal price = PromptDecimal("Введите цену блюда: ");

        var dish = new Dishes { Name = name, Price = price };

        if (Create(dish))
            Console.WriteLine("✅ Блюдо успешно создано.");
        else
            Console.WriteLine("❌ Ошибка: блюдо не было добавлено.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить блюдо по Id.\n");

        int id = PromptId("Введите Id блюда: ");

        var dish = GetById(id);
        if (dish == null)
            Console.WriteLine($"❌ Блюдо с Id {id} не найдено.");
        else
            Console.WriteLine($"✅ Найдено блюдо:");
            dish.ShowInfo();
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все блюда.\n");

        foreach (var d in GetAll())
            d.ShowInfo();
    }

    public static void UpdateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Обновить блюдо.\n");

        int id = PromptId("Введите Id блюда: ");
        var existing = GetById(id);
        if (existing == null)
        {
            Console.WriteLine($"❌ Блюдо с Id {id} не найдено.");
            return;
        }

        Console.WriteLine($"Текущее название: {existing.Name}");
        string name = PromptRequired("Введите новое название: ");

        Console.WriteLine($"Текущая цена: {existing.Price}");
        decimal price = PromptDecimal("Введите новую цену: ");

        var updated = new Dishes { Id = id, Name = name, Price = price };

        if (Update(updated))
            Console.WriteLine("✅ Блюдо успешно обновлено.");
        else
            Console.WriteLine("❌ Ошибка при обновлении блюда.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить блюдо.\n");

        int id = PromptId("Введите Id блюда: ");
        var existing = GetById(id);

        if (existing == null)
        {
            Console.WriteLine($"❌ Блюдо с Id {id} не найдено.");
            return;
        }

        Console.Write($"Вы уверены, что хотите удалить {existing.Name}? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToLower();
        if (confirm is not ("y" or "yes" or "Y" or "Yes"))
        {
            Console.WriteLine("🚫 Удаление отменено.");
            return;
        }

        if (Delete(id))
            Console.WriteLine("✅ Блюдо удалено.");
        else
            Console.WriteLine("❌ Ошибка при удалении блюда.");
    }

    

    private static bool Create(Dishes dish)
    {
        const string query = @"INSERT INTO Dishes (Name, Price) VALUES (@Name, @Price)";
        return ExecuteNonQuery(query,
            new SqlParameter("@Name", dish.Name),
            new SqlParameter("@Price", dish.Price)) > 0;
    }

    private static Dishes? GetById(int id)
    {
        const string query = @"SELECT DishId, Name, Price FROM Dishes WHERE DishId = @Id";

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Dishes
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            };
        }
        return null;
    }

    private static IEnumerable<Dishes> GetAll()
    {
        const string query = @"SELECT DishId, Name, Price FROM Dishes";
        var list = new List<Dishes>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Dishes
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            });
        }
        return list;
    }

    private static bool Update(Dishes dish)
    {
        const string query = @"UPDATE Dishes SET Name = @Name, Price = @Price WHERE DishId = @Id";
        return ExecuteNonQuery(query,
            new SqlParameter("@Name", dish.Name),
            new SqlParameter("@Price", dish.Price),
            new SqlParameter("@Id", dish.Id)) > 0;
    }

    private static bool Delete(int id)
    {
        const string query = @"DELETE FROM Dishes WHERE DishId = @Id";
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

    private static decimal PromptDecimal(string message)
    {
        Console.Write(message);
        decimal value;
        while (!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
            Console.Write("Ошибка: введите положительное число: ");
        return value;
    }
}
