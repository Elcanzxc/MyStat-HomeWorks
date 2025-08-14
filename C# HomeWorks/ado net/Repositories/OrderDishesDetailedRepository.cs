using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;

namespace ado_net.Models;


static class OrderDishesDetailedRepository
{
    private static readonly string connectionString ="Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";


    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать все заказы с блюдами.\n");

        var list = GetAll();
        if (!list.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
            return;
        }

        foreach (var item in list)
            item.ShowInfo();
    }

    private static IEnumerable<OrderDishesDetailed> GetAll()
    {
        const string query = @"
            SELECT 
                o.OrderId, o.CustomerId, o.OrderDate,
                d.DishId, d.Name, d.Price,
                od.Quantity
            FROM OrderDishes od
            INNER JOIN Orders o ON od.OrderId = o.OrderId
            INNER JOIN Dishes d ON od.DishId = d.DishId";

        var result = new List<OrderDishesDetailed>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var order = new Orders
            {
                Id = reader.GetInt32(0),
                CustomerId = reader.GetInt32(1),
                OrderDate = reader.GetDateTime(2)
            };

            var dish = new Dishes
            {
                Id = reader.GetInt32(3),
                Name = reader.GetString(4),
                Price = reader.GetDecimal(5)
            };

            var orderDishDetailed = new OrderDishesDetailed
            {
                Order = order,
                Dishes = dish,
                Quantity = reader.GetInt32(6)
            };

            result.Add(orderDishDetailed);
        }

        return result;
    }
}
