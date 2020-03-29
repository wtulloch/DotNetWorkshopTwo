using System;

namespace SimpleIocContainerTestFixtures.TestInterfaces
{
    public interface ISimpleLogger
    {
        void LogMessage(string message);
    }

    class SimpleLogger : ISimpleLogger
    {
        public void LogMessage(string message)
        {
           Console.WriteLine(message);
        }
    }
}