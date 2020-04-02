using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UserServices
{
    public class EmailNotificationService : IDisposable
    {
        private Uri _uri = new Uri("https://tg-workshop.azurewebsites.net/api/SendEmailNotification");
        private HttpClient _client = new HttpClient();

        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<Notification> Send(UserInfo userInfo)
        {
            var json = JsonSerializer.Serialize(userInfo, _jsonOptions);
               var response = await _client.PostAsync(_uri, new StringContent(json, Encoding.UTF8, "application/json"));

               if (response.IsSuccessStatusCode)
               {
                   var data = await response.Content.ReadAsStringAsync();
                   var notification = JsonSerializer.Deserialize<Notification>(data,_jsonOptions);
                   return notification;
               }
               else
               {
                   return default(Notification);
               }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}