using CodeSmellsDemo.LargeClassSmell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LargeClassClean
{
    public interface IUserService
    {
        bool RegisterUser(string name, string email, string password);
        User LoginUser(string email, string password);
        void LogoutUser(string email);
        int GetTotalUsers();
    }
}
