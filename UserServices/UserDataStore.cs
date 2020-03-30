using System;

namespace UserServices
{
    public class UserDataStore
    {
        public void AddUser(UserInfo userInfo)
        {
            Console.WriteLine($"A new user has been {userInfo.Name}");
        }
    }
}