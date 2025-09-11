
using Dapper;
using Microsoft.Data.SqlClient;
using MyRestaurant.Data;
using MyRestaurant.Entitites;

namespace MyRestaurant.Repositories;

public static class MenuRepository
{

    private static readonly string connectionString
        = "Server=localhost;Database=Restaurant;Integrated Security=True;TrustServerCertificate=True;";

    public static void CreateMenu(Menu menu)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var created = connection.Execute(
            sql: @"INSERT INTO Menu([Name],[Price],[Category],[Description])
                       VALUES (@Name,@Price,@Category,@Description)",
            param: new
            {
                Name = menu.Name,
                Price = menu.Price,
                Category = menu.Category,
                Description = menu.Description
            });

        Console.WriteLine(created > 0
            ? $"Блюдо '{menu.Name}' успешно добавлено."
            : "Не удалось добавить блюдо.");
    }
    public static void ShowAllMenu()
    {
        Console.WriteLine("\nВсе блюда меню:\n");
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var allMenu = connection.Query<Menu>(
            sql: @"SELECT * FROM Menu");

        foreach (var menu in allMenu)
        {
            Console.WriteLine(menu.ToString());
        }
    }

    public static void ShowMenuById(int id)
    {
        Console.WriteLine($"\nБлюдо с ID = {id}\n");

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var menu = connection.QueryFirstOrDefault<Menu>(
            sql: @"SELECT * FROM Menu WHERE Id = @Id",
            param: new { Id = id });

        if (menu != null)
        {
            Console.WriteLine(menu.ToString());
        }
        else
        {
            Console.WriteLine("Блюдо с таким ID не найдено.");
        }
    }

    public static void UpdateMenu(Menu menu)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var updated = connection.Execute(
            sql: @"UPDATE Menu
                       SET Name = @Name,
                           Price = @Price,
                           Category = @Category,
                           Description = @Description
                       WHERE Id = @Id",
            param: new
            {
                Id = menu.Id,
                Name = menu.Name,
                Price = menu.Price,
                Category = menu.Category,
                Description = menu.Description
            });

        Console.WriteLine(updated > 0
            ? $"Блюдо с Id = {menu.Id} успешно обновлено."
            : $"Блюдо с Id = {menu.Id} не найдено.");
    }

    public static void DeleteMenu(int id)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var deleted = connection.Execute(
            sql: @"DELETE FROM Menu WHERE Id = @Id",
            param: new { Id = id });

        Console.WriteLine(deleted > 0
            ? $"Блюдо с Id = {id} успешно удалено."
            : $"Блюдо с Id = {id} не найдено.");
    }

    public static void DeleteAllMenu()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var deleted = connection.Execute(
            sql: @"DELETE FROM Menu");

        Console.WriteLine(deleted > 0
            ? $"Все блюда ({deleted}) успешно удалены."
            : "Меню уже пустое.");
    }
}
