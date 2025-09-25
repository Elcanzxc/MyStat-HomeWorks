using Async_Await.Entities;
using Async_Await.Models;


namespace UserProfileApp.Repositories
{
    public class UserParallelRepository
    {
        private readonly UserRepositorySync repository = new UserRepositorySync() ;

        public UserParallelRepository(){}

        public Task Create(User user)
        {
            return Task.Run(() => repository.Create(user));
        }

        public Task<User?> GetById(int id)
        {
            return Task.Run(() => repository.GetById(id));
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return Task.Run(() => repository.GetAll());
        }

        public Task Update(User user)
        {
            return Task.Run(() => repository.Update(user));
        }

        public Task Delete(int id)
        {
            return Task.Run(() => repository.Delete(id));
        }
    }
}
