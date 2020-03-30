using System.Collections.Generic;

namespace IoCAndDependencyInjection
{
    public class UserRepository
    {
        private UserDataStore _datastore = new UserDataStore();
        private List<UserInfo> _UserCache = new List<UserInfo>();

        public bool TryAddNewUser(UserInfo newUser)
        {
            if (_UserCache.Contains(newUser))
            {
                return false;
            }

            _datastore.AddUser(newUser);
            _UserCache.Add(newUser);
            
            return true;
        }
    }
}