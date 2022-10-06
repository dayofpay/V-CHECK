using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_CHECK
{
    internal class CreateVehicleList
    {
        public static string selectedEnding
        {
            get;
            set;
        }
        public static int selectedUnique
        {
            get;
            set;
        }
        public static string fileName = @"licensenums.txt";
        public static string fileName2 = @"uniquenums.txt";
        public static void CreateVehicles(string cityType, int totalList, bool uniqueTypes) // Метод, съдържащ 3 параметъра (cityType - Отговарящ за областа, която ще генерираме ) (totalList - Броят рег. номера, които ще генерираме) ( uniqueTypes - С този параметър уточняваме, дали ще използваме поръчкови рег. номера като СВ1111СВ//
                                                                                            // cityType = Името на града
                                                                                            // totalList = Общ брой номера за генериране
                                                                                            // uniqueTypes = Обознаваме дали ще използваме поръчкови номера
        {
            if (VehicleCodes.cityCodes.Contains(cityType) && totalList >= 1)
            {
                if (uniqueTypes == false) // Ако списъка със рег. номера не съдържа номера като ТХ7777АР , ТХ1111СВ
                {

                        Random randomNums = new Random(); // Създаваме 4 цифрено число на случаен принцип
                        Random randomEnding = new Random(); // Създаваме края на регистрационния номер според съответната генерирана област от cityCodes array-a
                    for (int i = 0; i <= totalList; i++) // Създаваме цикъл със начална позиция 0 и крайната е параметъра totalList, който е описан при извикването на метода
                    {
                        Console.Title = $"V-CHECK | ГЕНЕРИРАНЕ НА РЕГ.НОМЕР | ТИП : ОБИКНОВЕН | {i}/{totalList}";
                        selectedEnding = VehicleCodes.cityCodes[randomEnding.Next(1, VehicleCodes.cityCodes.Length)];
                        if (selectedEnding.Length <= 1) // В случай, че областа в края на номера съдържа само една буква
                        {
                            i--; // Премахваме стойноста със 1 назад
                                 //selectedEnding = VehicleCodes.cityCodes[randomEnding.Next(1, VehicleCodes.cityCodes.Length)];
                        }
                        else
                        {
                            string genInfo = cityType + randomNums.Next(1000, 9999) + selectedEnding; // Пример ТХ1234СВ
                            Modules.CreateInfo("Генериран регистрационен номер: " + genInfo);
                            VehicleList.vehicleList.Add(cityType + randomNums.Next(1000, 9999) + selectedEnding); // Финален резултат, който се добавя към списъка със генерирани регистрационни номера :)
                        }
                    }
                    Modules.CreateAwait(2);
                    Modules.CreateInfo("Прехвърляне към началното меню след 2 секунди ...");
                    Modules.CreateFile();
                    Program.Main();
                }
                else
                {
                        for (int i = 0; i <= totalList; i++)
                        {
                            Console.Title = $"V-CHECK | ГЕНЕРИРАНЕ НА РЕГ.НОМЕР | ТИП : ПОРЪЧКОВ | {i}/{totalList}";
                            Random randomNums = new Random();
                            Random randomEnding = new Random();
                            Random randomUnique = new Random();
                            selectedEnding = VehicleCodes.cityCodes[randomEnding.Next(1, VehicleCodes.cityCodes.Length)];
                            selectedUnique = VehicleCodes.uniqueCodes[randomUnique.Next(0, VehicleCodes.uniqueCodes.Length)];
                            if (selectedEnding.Length <= 1)
                            {
                                i--;
                                //selectedEnding = VehicleCodes.cityCodes[randomEnding.Next(1, VehicleCodes.cityCodes.Length)];
                            }
                            else if(VehicleList.uniqueList.Contains(cityType + selectedUnique + selectedEnding))
                        {
                            i--;
                            //Modules.CreateInfo($"Игнорираме {cityType + selectedUnique + selectedEnding} , тъй като вече съществува в нашият списък !");
                        }
                            else
                            {
                                string genInfo = cityType + selectedUnique + selectedEnding;
                                VehicleList.uniqueList.Add(cityType + selectedUnique + selectedEnding);
                                Modules.CreateInfo("Генериран регистрационен номер: " + genInfo);
                            }
                    }
                    Modules.CreateInfo("Прехвърляне към началното меню след 2 секунди ...");
                    Modules.CreateAwait(2);
                    Modules.CreateUniqueFile();
                    Program.Main();
                }
            }
            else
            {
                Modules.CreateError("Не съдържа град с наименувание : " + cityType);
                Modules.CreateAwait(5);
            }
        }
    }
}