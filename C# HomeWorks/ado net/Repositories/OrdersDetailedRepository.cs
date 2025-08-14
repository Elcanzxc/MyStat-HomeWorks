using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;


namespace ado_net.Models;

static class OrdersDetailedRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все заказы с информацией о клиентах.\n");

        var orders = GetAll();
        if (!orders.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
            return;
        }

        foreach (var order in orders)
            order.ShowInfo();
    }

    private static IEnumerable<OrdersDetailed> GetAll()
    {
        const string query = @"
            SELECT 
                o.OrderId, 
                o.OrderDate, 
                c.CustomerId, 
                c.FullName, 
                c.Phone
            FROM Orders o
            LEFT JOIN Customers c ON o.CustomerId = c.CustomerId";

        var result = new List<OrdersDetailed>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var customer = new Customers
            {
                Id = reader.GetInt32(2),
                FullName = reader.GetString(3),
                Phone = reader.IsDBNull(4) ? null : reader.GetString(4)
            };

            var order = new OrdersDetailed
            {
                Id = reader.GetInt32(0),
                OrderDate = reader.GetDateTime(1),
                Customer = customer
            };

            result.Add(order);
        }

        return result;
    }
}
