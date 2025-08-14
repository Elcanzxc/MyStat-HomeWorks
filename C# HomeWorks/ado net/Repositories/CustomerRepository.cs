using ado_net.Entities;
using ado_net.Extensions;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;






static class CustomersRepository
{
    private static readonly string connectionString = "Server=localhost;Database=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;";



    public static void CreateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Создать клиента.\n");

        string fullName = PromptRequired("Введите полное имя клиента: ");
        string phone = PromptPhone("Введите номер телефона (+994-XXX-XXX-XX-XX) или оставьте пустым: ");

        var customer = new Customers { FullName = fullName, Phone = phone };

        if (Create(customer))
            Console.WriteLine("✅ Клиент успешно создан.");
        else
            Console.WriteLine("❌ Ошибка: клиент не был добавлен.");
    }

    public static void GetByIdInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Получить клиента по Id.\n");

        int id = PromptId("Введите Id клиента: ");

        var customer = GetById(id);
        if (customer == null)
            Console.WriteLine($"❌ Клиент с Id {id} не найден.");
        else
            Console.WriteLine($"✅ Найден клиент: ");
            customer.ShowInfo();
    }

    public static void GetAllInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Показать всех клиентов.\n");

        foreach (var c in GetAll())
            c.ShowInfo();
    }

    public static void UpdateInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Обновить клиента.\n");

        int id = PromptId("Введите Id клиента: ");

        var existing = GetById(id);
        if (existing == null)
        {
            Console.WriteLine($"❌ Клиент с Id {id} не найден.");
            return;
        }

        Console.WriteLine($"Текущее имя: {existing.FullName}");
        string newName = PromptRequired("Введите новое имя: ");

        Console.WriteLine($"Текущий телефон: {existing.Phone ?? "не указан"}");
        string newPhone = PromptPhone("Введите новый телефон (+994-XXX-XXX-XX-XX) или оставьте пустым: ");

        var updated = new Customers { Id = id, FullName = newName, Phone = newPhone };

        if (Update(updated))
            Console.WriteLine("✅ Клиент успешно обновлён.");
        else
            Console.WriteLine("❌ Ошибка при обновлении клиента.");
    }

    public static void DeleteInteractive()
    {
        Console.WriteLine("Вы выбрали операцию: Удалить клиента.\n");

        int id = PromptId("Введите Id клиента: ");
        var existing = GetById(id);

        if (existing == null)
        {
            Console.WriteLine($"❌ Клиент с Id {id} не найден.");
            return;
        }

        Console.Write($"Вы уверены, что хотите удалить {existing.FullName}? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToLower();
        if (confirm is not ("y" or "Yes" or "Y" or "yes"))
        {
            Console.WriteLine("🚫 Удаление отменено.");
            return;
        }

        if (Delete(id))
            Console.WriteLine("✅ Клиент удалён.");
        else
            Console.WriteLine("❌ Ошибка при удалении клиента.");
    }



    private static bool Create(Customers customer)
    {
        const string query = @"INSERT INTO Customers (FullName, Phone) 
                               VALUES (@FullName, @Phone)";

        return ExecuteNonQuery(query,
            new SqlParameter("@FullName", customer.FullName),
            new SqlParameter("@Phone", customer.Phone)) > 0;
    }
    private static Customers? GetById(int id)
    {
        const string query = @"SELECT CustomerId, FullName, Phone FROM Customers WHERE CustomerId = @Id";

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Customers
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Phone = reader.GetString(2)
            };
        }
        return null;
    }
    private static IEnumerable<Customers> GetAll()
    {
        const string query = @"SELECT CustomerId, FullName, Phone FROM Customers";
        var list = new List<Customers>();

        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand(query, connection);

        connection.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Customers
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Phone = reader.GetString(2)
            });
        }
        return list;
    }
    private static bool Update(Customers customer)
    {
        const string query = @"UPDATE Customers 
                               SET FullName = @FullName, Phone = @Phone 
                               WHERE CustomerId = @Id";

        return ExecuteNonQuery(query,
            new SqlParameter("@FullName", customer.FullName),
            new SqlParameter("@Phone", customer.Phone),
            new SqlParameter("@Id", customer.Id)) > 0;
    }
    private static bool Delete(int id)
    {
        const string query = @"DELETE FROM Customers WHERE CustomerId = @Id";

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

    private static string? PromptPhone(string message)
    {
        Console.Write(message);
        string? phone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(phone))
            return null;

        string pattern = @"^\+994-\d{3}-\d{3}-\d{2}-\d{2}$";
        while (!Regex.IsMatch(phone, pattern))
        {
            Console.Write("Ошибка: формат номера должен быть +994-XXX-XXX-XX-XX. Повторите: ");
            phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone))
                return null;
        }
        return phone;
    }

}
