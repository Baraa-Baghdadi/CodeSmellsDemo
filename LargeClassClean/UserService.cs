using CodeSmellsDemo.LargeClassSmell;

namespace CodeSmellsDemo.LargeClassClean
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();
        private readonly List<string> _userSessions = new();

        public bool RegisterUser(string name, string email, string password)
        {
            if (_users.Any(u => u.Email == email)) return false;
            _users.Add(new User { Name = name, Email = email, Password = password });
            return true;
        }

        public User LoginUser(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null) _userSessions.Add(user.Email);
            return user;
        }

        public void LogoutUser(string email)
        {
            _userSessions.Remove(email);
        }

        public int GetTotalUsers() => _users.Count;
    }
}
