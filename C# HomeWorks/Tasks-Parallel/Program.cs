using Async_Await.Entities;
using Tasks_Parallel.Models;
using UserProfileApp.Repositories;

class Program
    {
        static async Task Main(string[] args)
        {

          var users1 = new List<User>
            {
                new User { Name = "Kemale", Surname = "Samirova", BirthDate = new DateTime(2006, 5, 5) },
                new User { Name = "Leyla", Surname = "Mammadova", BirthDate = new DateTime(2005, 8, 12) }
            };


        var entitySyncSqlRepository = new EntitySyncSqlRepository();


            entitySyncSqlRepository.BulkInsert(users1);
               
            Console.ReadLine();
         
            var allSyncUsers = entitySyncSqlRepository.GetAll();
            foreach (var u in allSyncUsers)
            {
                Console.WriteLine($"User -> {u.Id}: {u.Name} {u.Surname} {u.BirthDate:yyyy-MM-dd}");
            }


           Console.ReadLine();



        var users2 = new List<User>
            {
                new User { Name = "Anton", Surname = "xzz", BirthDate = new DateTime(2006, 5, 5) },
                new User { Name = "Sanya", Surname = "tojexz", BirthDate = new DateTime(2005, 8, 12) }
            };
        var entityAsyncSqlRepository = new EntityAsyncSqlRepository();

          var usersTasks = entityAsyncSqlRepository.BulkInsertParallel(users2);
    

            Console.ReadLine();
            var allAsyncUsers =  entityAsyncSqlRepository.GetAll();
            var allUsers = allAsyncUsers.Result;

            foreach (var u in allUsers)
            {
                Console.WriteLine($"User -> {u.Id}: {u.Name} {u.Surname} {u.BirthDate:yyyy-MM-dd}");
            }


        }
    }
