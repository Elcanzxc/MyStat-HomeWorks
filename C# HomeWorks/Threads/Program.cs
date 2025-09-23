using Threads.Models;

class Program
{
    private static readonly string connectionString
        = "Server=localhost;Database=UserStatistic;Integrated Security=True;TrustServerCertificate=True;";

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var dataRepository = new DataRepository(connectionString);

        string nickName = InputNickName();
        int userMajorId = dataRepository.EnsureUserExistsAndGetId(nickName);

        var userTracker = new UserTracker(nickName, userMajorId, dataRepository);
        userTracker.StartTracking();

        var menu = new Menu(userTracker);
        menu.Run();

        userTracker.StopTrackingAndSave();
        Console.WriteLine("\nСпасибо за использование приложения! Данные сохранены.");
    }

    private static string InputNickName()
    {
        Console.Write("Введите никнейм: ");
        string? nickName = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(nickName))
        {
            Console.WriteLine("Имя либо пустое, либо не было введено! Попробуйте ещё раз.");
            return InputNickName();
        }
        return nickName;
    }
}
