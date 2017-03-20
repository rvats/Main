using System;

namespace DemoApp
{
    public static class Logger
    {
        public static void PrintException(Exception ex, string methodName)
        {
            Console.WriteLine("{0}: \n '{1}' \n '{2}'", methodName, ex.Message, ex.StackTrace);
        }
    }
}
