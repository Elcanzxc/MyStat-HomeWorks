using Async_Await.Entities;
using UserProfileApp.Repositories;

// Вроде норм
class Program
{
    static async Task Main()
    {
        var newUser = new User
        {
            Name = "Samir",
            Surname = "Kamranov",
            BirthDate = new DateTime(2005, 4, 3)
        };

        UserParallelRepository userParallelRepository = new UserParallelRepository();

        userParallelRepository.Create(newUser);


        Console.ReadLine();
        var parallelUsersTask = userParallelRepository.GetAll();
        var parallelUsers = parallelUsersTask.Result;
        foreach (var item in parallelUsers)
        {
            Console.WriteLine($"User -> {item.Id} {item.Name} {item.Surname}");
        }

        Console.WriteLine("--------------------------------------------------------");
        Console.ReadLine();

        var newUser2 = new User
        {
            Name = "Salman",
            Surname = "Aliyev",
            BirthDate = new DateTime(2007, 11, 11)
        };

        UserAsyncRepository userAsyncRepository = new UserAsyncRepository();
        await userAsyncRepository.CreateAsync(newUser2);

        var asyncUsers = await userAsyncRepository.GetAllAsync();
        foreach (var item in asyncUsers)
        {
            Console.WriteLine($"User -> {item.Id} {item.Name} {item.Surname}");
        }

        Console.ReadLine();
    }
}