using System;
using System.ComponentModel;

namespace IoCAndDependencyInjection
{
    public class UserDataStore
    {
        public void AddUser(UserInfo userInfo)
        {
            Console.WriteLine($"A new user has been {userInfo.Name}");
        }
    }
}