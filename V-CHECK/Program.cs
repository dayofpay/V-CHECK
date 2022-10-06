using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_CHECK
{
    internal class Program {
    public static void Main()
    {
    Restart:
        Console.Title = "V-CHECK | НАЧАЛНО МЕНЮ | ДОВРЕ ДОШЪЛ," + Environment.UserName;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{DateTime.Now.ToString()}]    Добре дошъл, {Environment.UserName} !");
        Console.WriteLine("[1] Проверка на списъка ви със частни номера");
        Console.WriteLine("[2] Проверка на списъка ви със стандартни номера");
        Console.WriteLine("[3] Генериране на регистрационни номера");
        try
            {
                var izbor = int.Parse(Console.ReadLine());
                if (izbor == 1)
                {
                    checkLicense.Check("custom");
                }
            if (izbor == 2)
            {
                checkLicense.Check("standart");
            }
            if (izbor == 3)
            {
                Console.Write("Местоположение за генериране: ");
                var placeType = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Брой рег. номера за генериране: ");
                var totalAmount = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Да съдържа ли списъка поръчкови номера ? (да/не): ");
                var uniqueList = Console.ReadLine();
                if (uniqueList.ToLower() == "да")
                {
                    CreateVehicleList.CreateVehicles(placeType.ToUpper(), totalAmount, true);
                }
                else
                {
                    CreateVehicleList.CreateVehicles(placeType.ToUpper(), totalAmount, false);
                }
            }
        }
        catch (Exception ex)
        {
            Modules.ClearLogs();
            Modules.CreateError($"Възникна грешка ... информация {ex.Message}");
            Modules.CreateAwait(2);
            goto Restart;
        }
    }
}
}