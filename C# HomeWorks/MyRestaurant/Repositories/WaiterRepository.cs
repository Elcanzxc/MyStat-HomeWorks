using Dapper;
using MyRestaurant.Entitites;
using System;
using Microsoft.Data.SqlClient;

namespace MyRestaurant.Repositories
{
    public static class WaiterRepository
    {
        private static readonly string connectionString
            = "Server=localhost;Database=Restaurant;Integrated Security=True;TrustServerCertificate=True;";

        public static void CreateWaiter(Waiter waiter)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var created = connection.Execute(
                sql: @"INSERT INTO Waiters([FullName],[HireDate],[Salary])
                       VALUES (@FullName,@HireDate,@Salary)",
                param: new
                {
                    FullName = waiter.FullName,
                    HireDate = waiter.HireDate,
                    Salary = waiter.Salary
                });

            Console.WriteLine(created > 0
                ? $"Официант '{waiter.FullName}' успешно добавлен."
                : "Не удалось добавить официанта.");
        }


        public static void ShowAllWaiters()
        {
            Console.WriteLine("\nВсе официанты:\n");
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var allWaiters = connection.Query<Waiter>(
                sql: @"SELECT * FROM Waiters");

            foreach (var waiter in allWaiters)
            {
                Console.WriteLine(waiter.ToString());
            }
        }

  
        public static void ShowWaiterById(int id)
        {
            Console.WriteLine($"\nОфициант с ID = {id}\n");

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var waiter = connection.QueryFirstOrDefault<Waiter>(
                sql: @"SELECT * FROM Waiters WHERE Id = @Id",
                param: new { Id = id });

            if (waiter != null)
            {
                Console.WriteLine(waiter.ToString());
            }
            else
            {
                Console.WriteLine("Официант с таким ID не найден.");
            }
        }

        public static Waiter WaiterById(int id)
        {


            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var waiter = connection.QueryFirstOrDefault<Waiter>(
                sql: @"SELECT * FROM Waiters WHERE Id = @Id",
                param: new { Id = id });

            if (waiter == null)
            {
                throw new Exception("Waiter by this id not found");
            }

            return waiter;
        } 


        public static void UpdateWaiter(Waiter waiter)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var updated = connection.Execute(
                sql: @"UPDATE Waiters
                       SET FullName = @FullName,
                           HireDate = @HireDate,
                           Salary = @Salary
                       WHERE Id = @Id",
                param: new
                {
                    Id = waiter.Id,
                    FullName = waiter.FullName,
                    HireDate = waiter.HireDate,
                    Salary = waiter.Salary
                });

            Console.WriteLine(updated > 0
                ? $"Официант с Id = {waiter.Id} успешно обновлён."
                : $"Официант с Id = {waiter.Id} не найден.");
        }


        public static void DeleteWaiter(int id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var deleted = connection.Execute(
                sql: @"DELETE FROM Waiters WHERE Id = @Id",
                param: new { Id = id });

            Console.WriteLine(deleted > 0
                ? $"Официант с Id = {id} успешно удалён."
                : $"Официант с Id = {id} не найден.");
        }


        public static void DeleteAllWaiters()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var deleted = connection.Execute(
                sql: @"DELETE FROM Waiters");

            Console.WriteLine(deleted > 0
                ? $"Все официанты ({deleted}) успешно удалены."
                : "Список официантов уже пуст.");
        }
    }
}
