using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ExceptionExamples
{
    public class WorkingExceptions
    {
        public string GetTextFromFile(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}