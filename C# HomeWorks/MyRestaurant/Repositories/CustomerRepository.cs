using Microsoft.Data.SqlClient;
using Dapper;
using MyRestaurant.Entitites;

namespace MyRestaurant.Repositories;
public static class CustomerRepository
{
    private static readonly string connectionString
        = "Server=localhost;Database=Restaurant;Integrated Security=True;TrustServerCertificate=True;";


    public static void CreateCustomer(Customer customer)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        Console.WriteLine(customer.ToString());

        var createCustomer = connection.Execute(
            sql:@$"INSERT INTO Customers([FullName],[PhoneNumber])
                   VALUES (@FullName,@PhoneNumber)"
            ,param: new
            {
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            });

    }

    public static void ShowAllCustomers()
    {
        Console.WriteLine("\nВсе клиенты\n");
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var allCustomers = connection.Query<Customer>(
            sql:@"SELECT * FROM Customers");

        foreach (var customer in allCustomers)
        {
            Console.WriteLine(customer.ToString());
        }
    }

    public static void ShowCustomerById(int id)
    {
        Console.WriteLine($"\nКлиент с ID = {id}\n");

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var customer = connection.QueryFirstOrDefault<Customer>(
            sql: @"SELECT * FROM Customers WHERE Id = @Id",
            param: new { Id = id });

        if (customer != null)
        {
            Console.WriteLine(customer.ToString());
        }
        else
        {
            Console.WriteLine("Клиент с таким ID не найден.");
        }
    }

    public static Customer CustomerById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var customer = connection.QueryFirstOrDefault<Customer>(
            sql: @"SELECT * FROM Customers WHERE Id = @Id",
            param: new { Id = id });
        if (customer == null)
        {
            throw new Exception("Custmoer by this id not found");
        }
        return customer;
    }

    public static void UpdateCustomer(Customer customer)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var updated = connection.Execute(
            sql: @"UPDATE Customers
               SET FullName = @FullName,
                   PhoneNumber = @PhoneNumber
               WHERE Id = @Id",
            param: new
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            });

        if (updated > 0)
        {
            Console.WriteLine($"Клиент с Id = {customer.Id} успешно обновлён.");
        }
        else
        {
            Console.WriteLine($"Клиент с Id = {customer.Id} не найден.");
        }
    }

    public static void DeleteCustomer(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var deleted = connection.Execute(
            sql: @"DELETE FROM Customers WHERE Id = @Id",
            param: new { Id = id });

    }

    public static void DeleteAllCustomers()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var deleted = connection.Execute(
            sql: @"DELETE FROM Customers");

        Console.WriteLine(deleted > 0
            ? $"Все клиенты ({deleted}) успешно удалены."
            : "Список клиентов уже пуст.");
    }
}
