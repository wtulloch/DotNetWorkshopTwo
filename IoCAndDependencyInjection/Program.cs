using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IoCAndDependencyInjection
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteInputLabel("Enter a user's name: ");
            var name = Console.ReadLine();
            using (var userManagement = new UserManagement())
            {
                var notification = await userManagement.AddNewUserAsync(name);
                WriteResponse(notification.Message);
            }
          
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
