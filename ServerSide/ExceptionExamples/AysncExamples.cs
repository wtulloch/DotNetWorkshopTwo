using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExceptionExamples
{
    public class AysncExamples
    {
        public Task FailAsync(int value)
        {
            var result = Task.Run(()=> 100 / value).Result;
            return Task.CompletedTask;
        }

        public Task ThisIsAStupidExample()
        {
            var tasks = new List<Task>
            {
                new Task(() => throw new ArgumentException("Task1 failed")),
                new Task(() => throw new ArgumentException("Task2 failed")),
                new Task(() => throw new FileLoadException("Task3 failed"))
            };

            foreach (var task in tasks)
            {
                task.Start();
            }

            return Task.WhenAll(tasks);
        }
    }
}