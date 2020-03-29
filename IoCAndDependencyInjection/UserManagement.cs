using System;
using System.Threading.Tasks;

namespace IoCAndDependencyInjection
{
    public class UserManagement: IDisposable
    {
        private EmailNotificationService _notificationService = new EmailNotificationService();


        public async Task<Notification>  AddNewUserAsync(string name)
        {
            //we would do something here

           return await _notificationService.Send(new UserInfo {Name = name});
        }

        public void Dispose()
        {
            if(_notificationService != null)
            {
                _notificationService.Dispose();
            }
        }
    }
}