using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SimpleIocContainer
{
    /// <summary>
    /// borrowed from https://nearsoft.com/blog/writing-a-minimal-ioc-container-in-c/
    /// </summary>
    public class ExampleContainer
    {
        private readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

        public ExampleContainer Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _types[typeof(TInterface)] = typeof(TImplementation);
            return this;
        }

       

        public T Create<T>()
        {
            return (T) CreateInstance(typeof(T));
        }

        

        private object CreateInstance(Type type)
        {
            if (!_types.ContainsKey(type))
            {
                throw new SimpleContainerException($"{type.Name} is not registered");
            }

            var concreteType = _types[type];
            var defaultConstructor = concreteType.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();

            var parameters = defaultParams.Select(param => CreateInstance(param.ParameterType))
                .ToArray();

            return defaultConstructor.Invoke(parameters);
        }
    }
}
