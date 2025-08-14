using ado_net.Entities;
using ado_net.Extensions;
using ado_net.Interfaces;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;

static class OrdersRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";

    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Создать заказ.\n");

        int customerId = PromptId("Введите Id клиента: ");
        DateTime orderDate = DateTime.Now;
        Console.WriteLine($"Дата заказа будет установлена автоматически: {orderDate}");

        var order = new Orders { CustomerId = customerId, OrderDate = orderDate };

        if (Create(order))
            Console.WriteLine("✅ Заказ успешно создан.");
        else
            Console.WriteLine("❌ Ошибка: не удалось создать заказ.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить заказ по Id.\n");

        int id = PromptId("Введите Id заказа: ");
        var order = GetById(id);

        if (order == null)
            Console.WriteLine($"❌ Заказ с Id = {id} не найден.");
        else
        {
            Console.WriteLine($"✅ Заказ Id: {order.Id}, Id клиента: {order.CustomerId} Найден:");
            order.ShowInfo();
        }
            
            
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все заказы.\n");

        var list = GetAll();
        if (!list.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
            return;
        }

        foreach (var order in list)
            order.ShowInfo();
    }

    public static void UpdateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Обновить заказ.\n");

        int id = PromptId("Введите Id заказа для обновления: ");
        var existingOrder = GetById(id);
        if (existingOrder == null)
        {
            Console.WriteLine($"❌ Заказ с Id = {id} не найден.");
            return;
        }

        int customerId = PromptId($"Введите новый Id клиента (текущий: {existingOrder.CustomerId}): ");
        DateTime orderDate = DateTime.Now;
        Console.WriteLine($"Дата заказа будет обновлена автоматически: {orderDate}");

        var orderToUpdate = new Orders { Id = id, CustomerId = customerId, OrderDate = orderDate };

        if (Update(orderToUpdate))
            Console.WriteLine("✅ Заказ успешно обновлён.");
        else
            Console.WriteLine("❌ Ошибка: не удалось обновить заказ.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить заказ.\n");

        int id = PromptId("Введите Id заказа для удаления: ");
        var existingOrder = GetById(id);
        if (existingOrder == null)
        {
            Console.WriteLine($"❌ Заказ с Id = {id} не найден.");
            return;
        }

        Console.Write("Вы уверены, что хотите удалить заказ? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
        if (confirm is not ("Yes" or "yes" or "Y" or "y"))
        {
            Console.WriteLine("🚫 Операция удаления отменена.");
            return;
        }

        if (Delete(id))
            Console.WriteLine("✅ Заказ успешно удалён.");
        else
            Console.WriteLine("❌ Ошибка: не удалось удалить заказ.");
    }

    private static bool Create(Orders order)
    {
        const string query = @"INSERT INTO Orders (CustomerId, OrderDate)
                               VALUES (@CustomerId, @OrderDate)";
        return ExecuteNonQuery(query,
            new SqlParameter("@CustomerId", order.CustomerId),
            new SqlParameter("@OrderDate", order.OrderDate)) > 0;
    }

    private static Orders? GetById(int id)
    {
        const string query = @"SELECT OrderId, CustomerId, OrderDate FROM Orders
                               WHERE OrderId = @Id";
        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
            return new Orders
            {
                Id = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                OrderDate = reader.GetDateTime(2)
            };
        return null;
    }

    private static IEnumerable<Orders> GetAll()
    {
        const string query = "SELECT OrderId, CustomerId, OrderDate FROM Orders";
        var list = new List<Orders>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new Orders
            {
                Id = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                OrderDate = reader.GetDateTime(2)
            });

        return list;
    }

    private static bool Update(Orders order)
    {
        const string query = @"UPDATE Orders
                               SET CustomerId = @CustomerId, OrderDate = @OrderDate
                               WHERE OrderId = @Id";

        return ExecuteNonQuery(query,
            new SqlParameter("@Id", order.Id),
            new SqlParameter("@CustomerId", order.CustomerId),
            new SqlParameter("@OrderDate", order.OrderDate)) > 0;
    }

    private static bool Delete(int id)
    {
        const string query = @"DELETE FROM Orders WHERE OrderId = @Id";
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
}
