using System;
using System.Threading.Tasks;
using UserServices;

namespace IoCAndDependencyInjection
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            using (var userManagement = new UserManagement())
            {
                var name = GetName();
                do
                {
                    var notification = await userManagement.AddNewUserAsync(name);
                    WriteResponse($"Notification received:{notification.Message} - {notification.Method}");
                    
                    Console.WriteLine();
                    name = GetName();

                } while (!string.IsNullOrWhiteSpace(name));
            }
          
        }

        static string GetName()
        {
            WriteInputLabel("Enter a user's name: ");
            return Console.ReadLine();
        }
        static void WriteInputLabel(string labelText)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(labelText);
            Console.ResetColor();
        }

        static void WriteResponse(string response, bool successful = true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }
    }
}
