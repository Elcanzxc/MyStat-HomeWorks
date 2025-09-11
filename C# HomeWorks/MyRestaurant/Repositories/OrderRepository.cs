using Dapper;
using MyRestaurant.Entitites;
using Microsoft.Data.SqlClient;


namespace MyRestaurant.Repositories
{
    public static class OrderRepository
    {
        private static readonly string connectionString
            = "Server=localhost;Database=Restaurant;Integrated Security=True;TrustServerCertificate=True;";

        public static void CreateOrder(Order order)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var created = connection.Execute(
                sql: @"INSERT INTO Orders([OrderTime],[Status],[TotalPrice],[CustomerId],[WaiterId])
                       VALUES (@OrderTime,@Status,@TotalPrice,@CustomerId,@WaiterId)",
                param: new
                {
                    OrderTime = order.OrderTime,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    CustomerId = order.CustomerId,
                    WaiterId = order.WaiterId
                });

            Console.WriteLine(created > 0
                ? $"Заказ успешно создан (Сумма: {order.TotalPrice})."
                : "Не удалось создать заказ.");
        }

        public static void ShowAllOrders()
        {
            Console.WriteLine("\nВсе заказы:\n");
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var allOrders = connection.Query<Order>(
                sql: @"SELECT * FROM Orders");

            foreach (var order in allOrders)
            {
                Console.WriteLine($"Id: {order.Id}, " +
                                  $"Дата: {order.OrderTime}, " +
                                  $"Статус: {order.Status}, " +
                                  $"Сумма: {order.TotalPrice}, " +
                                  $"CustomerId: {order.CustomerId}, " +
                                  $"WaiterId: {order.WaiterId}");
            }
        }

        public static void ShowOrderById(int id)
        {
            Console.WriteLine($"\nЗаказ с ID = {id}\n");

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var order = connection.QueryFirstOrDefault<Order>(
                sql: @"SELECT * FROM Orders WHERE Id = @Id",
                param: new { Id = id });

            if (order != null)
            {
                Console.WriteLine($"Id: {order.Id}, " +
                                  $"Дата: {order.OrderTime}, " +
                                  $"Статус: {order.Status}, " +
                                  $"Сумма: {order.TotalPrice}, " +
                                  $"CustomerId: {order.CustomerId}, " +
                                  $"WaiterId: {order.WaiterId}");
            }
            else
            {
                Console.WriteLine("Заказ с таким ID не найден.");
            }
        }

        public static void UpdateOrder(Order order)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var updated = connection.Execute(
                sql: @"UPDATE Orders
                       SET OrderTime = @OrderTime,
                           Status = @Status,
                           TotalPrice = @TotalPrice,
                           CustomerId = @CustomerId,
                           WaiterId = @WaiterId
                       WHERE Id = @Id",
                param: new
                {
                    Id = order.Id,
                    OrderTime = order.OrderTime,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    CustomerId = order.CustomerId,
                    WaiterId = order.WaiterId
                });

            Console.WriteLine(updated > 0
                ? $"Заказ с Id = {order.Id} успешно обновлён."
                : $"Заказ с Id = {order.Id} не найден.");
        }

        public static void DeleteOrder(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var deleted = connection.Execute(
                sql: @"DELETE FROM Orders WHERE Id = @Id",
                param: new { Id = id });

            Console.WriteLine(deleted > 0
                ? $"Заказ с Id = {id} успешно удалён."
                : $"Заказ с Id = {id} не найден.");
        }

        public static void DeleteAllOrders()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var deleted = connection.Execute(
                sql: @"DELETE FROM Orders");

            Console.WriteLine(deleted > 0
                ? $"Все заказы ({deleted}) успешно удалены."
                : "Список заказов уже пуст.");
        }
    }
}
