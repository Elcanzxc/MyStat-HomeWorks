

using ado_net.Entities;
using ado_net.Extensions;
using ado_net.Interfaces;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;


static class OrderDishesRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";

    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Добавить блюдо в заказ.\n");

        int orderId = PromptId("Введите Id заказа: ");
        int dishId = PromptId("Введите Id блюда: ");
        int quantity = PromptId("Введите количество: ");

        var od = new OrderDishes { OrderId = orderId, DishId = dishId, Quantity = quantity };

        if (Create(od))
            Console.WriteLine("✅ Блюдо успешно добавлено в заказ.");
        else
            Console.WriteLine("❌ Ошибка: не удалось добавить блюдо в заказ.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить блюдо из заказа.\n");

        int orderId = PromptId("Введите Id заказа: ");
        int dishId = PromptId("Введите Id блюда: ");

        var od = GetById(orderId, dishId);
        if (od == null)
            Console.WriteLine("❌ Блюдо в заказе не найдено.");
        else
        {
            Console.WriteLine($"✅ Найдено: Заказ Id {od.OrderId}, Блюдо Id {od.DishId}, Количество: {od.Quantity}");
            od.ShowInfo();
        }
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все блюда в заказах.\n");

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
        Console.WriteLine("Вы выбрали операцию: Обновить количество блюда в заказе.\n");

        int orderId = PromptId("Введите Id заказа: ");
        int dishId = PromptId("Введите Id блюда: ");
        int quantity = PromptId("Введите новое количество: ");

        if (Update(orderId, dishId, quantity))
            Console.WriteLine("✅ Количество блюда успешно обновлено.");
        else
            Console.WriteLine("❌ Ошибка: не удалось обновить количество.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить блюдо из заказа.\n");

        int orderId = PromptId("Введите Id заказа: ");
        int dishId = PromptId("Введите Id блюда: ");

        Console.Write("Вы уверены, что хотите удалить блюдо из заказа? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (confirm is not ("Yes" or "yes" or "Y" or "y"))
        {
            Console.WriteLine("🚫 Удаление отменено.");
            return;
        }

        if (Delete(orderId, dishId))
            Console.WriteLine("✅ Блюдо успешно удалено из заказа.");
        else
            Console.WriteLine("❌ Ошибка: не удалось удалить блюдо.");
    }

    private static bool Create(OrderDishes od)
    {
        const string query = @"INSERT INTO OrderDishes (OrderId, DishId, Quantity)
                               VALUES (@OrderId, @DishId, @Quantity)";

        return ExecuteNonQuery(query,
            new SqlParameter("@OrderId", od.OrderId),
            new SqlParameter("@DishId", od.DishId),
            new SqlParameter("@Quantity", od.Quantity)) > 0;
    }

    private static OrderDishes? GetById(int orderId, int dishId)
    {
        const string query = @"SELECT OrderId, DishId, Quantity FROM OrderDishes
                               WHERE OrderId = @OrderId AND DishId = @DishId";

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@OrderId", orderId);
        cmd.Parameters.AddWithValue("@DishId", dishId);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
            return new OrderDishes
            {
                OrderId = reader.GetInt32(0),
                DishId = reader.GetInt32(1),
                Quantity = reader.GetInt32(2)
            };
        return null;
    }

    private static IEnumerable<OrderDishes> GetAll()
    {
        const string query = @"SELECT OrderId, DishId, Quantity FROM OrderDishes";
        var list = new List<OrderDishes>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new OrderDishes
            {
                OrderId = reader.GetInt32(0),
                DishId = reader.GetInt32(1),
                Quantity = reader.GetInt32(2)
            });

        return list;
    }

    private static bool Update(int orderId, int dishId, int quantity)
    {
        const string query = @"UPDATE OrderDishes
                               SET Quantity = @Quantity
                               WHERE OrderId = @OrderId AND DishId = @DishId";

        return ExecuteNonQuery(query,
            new SqlParameter("@OrderId", orderId),
            new SqlParameter("@DishId", dishId),
            new SqlParameter("@Quantity", quantity)) > 0;
    }

    private static bool Delete(int orderId, int dishId)
    {
        const string query = @"DELETE FROM OrderDishes
                               WHERE OrderId = @OrderId AND DishId = @DishId";

        return ExecuteNonQuery(query,
            new SqlParameter("@OrderId", orderId),
            new SqlParameter("@DishId", dishId)) > 0;
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
