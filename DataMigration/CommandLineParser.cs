using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataMigration
{
    class CommandLineParser<T> where T: class, new()
    {
        public T Parse(IEnumerable<string> args)
        {
            var options = new T();
            if (args == null || !args.Any())
            {
                return options;
            }

            var argsStack = new Stack<string>(args);


            while (argsStack.Any())
            {
                var arg = argsStack.Pop();

                var propertyAndValue = GetPropertyNameAndValue(arg);
                if (!propertyAndValue.isMatch) continue;

                var prop = typeof(T).GetProperty(propertyAndValue.Name);

                if (prop == null) continue;

                prop.SetValue(options, propertyAndValue.Value);
            }

            return options;
        }

        private (bool isMatch, string Name, string Value) GetPropertyNameAndValue(string arg)
        {
            var regEx = new Regex(@"^/(?<propertyName>.*?):(?<propertyValue>.*)$");

            if (regEx.IsMatch(arg))
            {
                var match = regEx.Match(arg);
                return (true, match.Groups["propertyName"].Value, match.Groups["propertyValue"].Value);
            }
            return (false, null, null);
        }

    }
}
