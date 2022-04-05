using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace VCheck
{
    internal class checkLicense
    {
        public static string noVignette = "public.ui.ok.noVignette";
        public static string standartList = @"licensenums.txt";
        public static string invalidList = @"invalid.txt";
        public static string validList = @"valid.txt";
        public static string uniqueList = @"uniquenums.txt";
        public static void Check(string checkType)
        {
            if (checkType == "standart")
            {
                Console.Title = "V-CHECK | Проверка на стандартни номера ...";
                using (StreamWriter writer = new StreamWriter(validList))
                {
                    using (StreamWriter writer2 = new StreamWriter(invalidList))
                    {
                        foreach (string x in File.ReadAllLines(standartList))
                        {
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
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("[ИМА ВИНЕТКА]" + x);
                                writer.WriteLine(x);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.Title = "V-CHECK | Проверка на частни номера ...";
                using (StreamWriter writer = new StreamWriter(invalidList))
                {
                    using (StreamWriter writer2 = new StreamWriter(validList))
                    {
                        foreach (string x in File.ReadAllLines(uniqueList))
                        {
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
                }

            }
        }