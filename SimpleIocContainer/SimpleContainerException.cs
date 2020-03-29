using System;

namespace SimpleIocContainer
{
    public class SimpleContainerException : Exception
    {
        public SimpleContainerException()
        {
        }
        public SimpleContainerException(string message)
        :base(message)
        {
        }

        public SimpleContainerException(string message, Exception innerException)
        :base(message, innerException)
        {
            
        }
    }
}