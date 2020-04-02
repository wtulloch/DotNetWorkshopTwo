using System;
using System.Threading.Tasks;

namespace UserServices
{
    public class UserManagement: IDisposable
    {
        private EmailNotificationService _notificationService = new EmailNotificationService();
        private UserRepository _userRepository = new UserRepository();

        public async Task<Notification>  AddNewUserAsync(string name)
        {
            var userInfo = new UserInfo {Name = name};
            if(_userRepository.TryAddNewUser(userInfo))
            {
                return await _notificationService.Send(userInfo);
            }

            return await Task.FromResult(new Notification {Message = $"{name} already exists"});
        }

        public void Dispose()
        {
            _notificationService?.Dispose();
        }
    }
}