using System;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionExamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var workingExceptions = new WorkingExceptions();
            var examples = new AysncExamples();
            try
            {
                await examples.FailAsync(0);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.Message);
                var flatNestExceptions = ae.Flatten().Message;
                Console.WriteLine(flatNestExceptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //workingExceptions.GetTextFromFile(@"c:\temp\test.txt");
           

            //examples.ThisIsAStupidExample().Wait();

        }
    }
}
