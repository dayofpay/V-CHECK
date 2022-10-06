using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace V_CHECK
{
    internal class checkLicense
    {
        public static string noVignette = "public.ui.ok.noVignette";
        public static string standartList = @"licensenums.txt";
        public static string invalidList = @"invalid.txt";
        public static string validList = @"valid.txt";
        public static string uniqueList = @"uniquenums.txt";
        public static int nullVignette = 0;
        public static int validVignette = 0;
        public static int total = 0;
        public static void Check(string checkType)
        {
            if (checkType == "standart")
            {
                using (StreamWriter writer = new StreamWriter(validList))
                {
                    using (StreamWriter writer2 = new StreamWriter(invalidList))
                    {
                        foreach (string x in File.ReadAllLines(standartList))
                        {
                            Console.Title = "V-CHECK | Проверка на стандартни номера ... | " + validVignette + " Валидни винетки ";
                            total++;
                            WebRequest webRequest = WebRequest.Create("https://check.bgtoll.bg/check/vignette/plate/BG/" + x);
                            WebResponse webResponse = webRequest.GetResponse();
                            Stream data = webResponse.GetResponseStream();
                            StreamReader reader = new StreamReader(data);
                            string response = reader.ReadToEnd();
                            JObject jsReader = JObject.Parse(response);
                            if (response.Contains(noVignette))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[НЯМА ВИНЕТКА] " + x);
                                writer2.WriteLine(x);
                                nullVignette++;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("[ИМА ВИНЕТКА]" + x);
                                writer.WriteLine(x);
                                validVignette++;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.Title = "V-CHECK | Проверка на стандартни номера ... | " + validVignette + " Валидни винетки ";
                using (StreamWriter writer = new StreamWriter(invalidList))
                {
                    using (StreamWriter writer2 = new StreamWriter(validList))
                    {
                        foreach (string x in File.ReadAllLines(uniqueList))
                        {
                            total++;
                            WebRequest webRequest = WebRequest.Create("https://check.bgtoll.bg/check/vignette/plate/BG/" + x);
                            WebResponse webResponse = webRequest.GetResponse();
                            Stream data = webResponse.GetResponseStream();
                            StreamReader reader = new StreamReader(data);
                            string response = reader.ReadToEnd();
                            JObject jsReader = JObject.Parse(response);
                            if (response.Contains(noVignette))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[НЯМА ВИНЕТКА] " + x);
                                writer.WriteLine(x);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("[ИМА ВИНЕТКА] " + x);
                                writer2.WriteLine(x);
                            }
                        }
                    }
                }
            }
            Modules.CreateInfo("             Проверката приключи успешно !");
            Modules.CreateInfo($"            {total} Общо проверени МПС!");
            Modules.CreateInfo($"            {validVignette} Валидни винетки");
            Modules.CreateWarn($"            {nullVignette} Невалидни винетки");
            Modules.CreateAwait(10);
            Program.Main();
        }
    }
}