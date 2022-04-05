using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCheck
{
    internal class Modules
    {
        public static void CreateWarn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[ПРЕДУПРЕЖДЕНИЕ] : {message}");
            Console.WriteLine();
        }
        public static void CreateError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ГРЕШКА] : "+ message);
            Console.WriteLine();
        }
        public static void CreateInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Информация] : " + message);
            Console.WriteLine();
        }
        public static void CreateAwait(int seconds)
        {
            // await
            System.Threading.Thread.Sleep(seconds * 1000);
        }
        public static void ClearLogs()
        {
            Console.Clear();
        }
    }
}
