using WhoWantToBeAMillionaire_2._0.Entities;


namespace WhoWantToBeAMillionaire_2._0.Models
{


    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Login(string loginName)
        {
            var user = userRepository.GetUserByLoginName(loginName);

            if (user == null)
            {
                var newUser = new User { LoginName = loginName };
                userRepository.CreateUser(newUser);

                user = userRepository.GetUserByLoginName(loginName);
            }

            return user;
        }
    }
}
