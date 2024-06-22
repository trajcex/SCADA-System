using ReportManager.ReportServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    internal class Program
    {
        private static ReportManagerClient _reportManagerClient = new ReportManagerClient();
        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
                Console.Write("Unesite opciju: ");
                string userInput = Console.ReadLine();
                if (HandleMenuOption(userInput))
                {
                    break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n----- Izvestaji -----");
            Console.WriteLine("1. Svi alarmi koji su se desili u određenom vremenskom periodu");
            Console.WriteLine("2. Svi alarmi određenog prioriteta");
            Console.WriteLine("3. Sve vrednosti tagova koje su dospele na servis u određenom vremenskom periodu");
            Console.WriteLine("4. Poslednja vrednost svih AI tagova");
            Console.WriteLine("5. Poslednja vrednost svih DI tagova");
            Console.WriteLine("6. Sve vrednosti taga sa određenim identifikatorom");
            Console.WriteLine("x. Izlaz");
        }
        private static bool HandleMenuOption(string option)
        {
            switch (option)
            {
                case "1":
                    GetAlarmsByInterval();
                    break;
                case "2":
                    GetByPriority();
                    break;
                case "3":
                    GetByInterval();
                    break;
                case "4":
                    Console.WriteLine(_reportManagerClient.GetLastValueAi());
                    break;
                case "5":
                    Console.WriteLine(_reportManagerClient.GetLastValueDi());
                    break;
                case "6":
                    GetByTag();
                    break;
                case "x":
                    return true;
                default:
                    Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                    break;
            }
            return false;
        }

        private static void GetByTag()
        {
            var tagNames = _reportManagerClient.GetAllTagNames();
            if (tagNames.Length == 0)
            {
                Console.WriteLine("\nNema dostupnih tagova.");
                return;
            }
            Console.WriteLine("\nDostpuni tagovi:");
            foreach (var tag in tagNames)
            {
                Console.WriteLine(tag);
            }
            while (true)
            {
                Console.Write("Unesite TagName: ");
                string tagName = Console.ReadLine();

                if (tagNames.Contains(tagName))
                {
                    Console.WriteLine(_reportManagerClient.GetSortedValuesByTagName(tagName));
                    break;
                }
                else
                {
                    Console.WriteLine($"Tag sa imenom '{tagName}' ne postoji. Pokušajte ponovo.");
                }
            }
        }

        private static void GetByInterval()
        {
            DateTime firstDate = GetValidDate("Unesite prvi datum (dd.MM.yyyy.): ");
            DateTime secondDate = GetValidDate("Unesite drugi datum (dd.MM.yyyy.): ", firstDate);

            if (firstDate.Date == secondDate.Date)
            {
                firstDate = firstDate.Date;
            }

            Console.WriteLine("\nUneli ste validne datume:");
            Console.WriteLine("Prvi datum: " + firstDate.ToString("dd.MM.yyyy."));
            Console.WriteLine("Drugi datum: " + secondDate.ToString("dd.MM.yyyy."));
            Console.WriteLine(_reportManagerClient.GetTagValuesInPeriod(firstDate, secondDate));
        }

        private static void GetAlarmsByInterval()
        {
            DateTime firstDate = GetValidDate("Unesite prvi datum (dd.MM.yyyy.): ");
            DateTime secondDate = GetValidDate("Unesite drugi datum (dd.MM.yyyy.): ", firstDate);

            if (firstDate.Date == secondDate.Date)
            {
                firstDate = firstDate.Date;
            }

            Console.WriteLine("\nUneli ste validne datume:");
            Console.WriteLine("Prvi datum: " + firstDate.ToString("dd.MM.yyyy."));
            Console.WriteLine("Drugi datum: " + secondDate.ToString("dd.MM.yyyy."));
            Console.WriteLine(_reportManagerClient.GetAlarmsInPeriod(firstDate, secondDate));
        }

        static DateTime GetValidDate(string prompt, DateTime? earlierDate = null)
        {
            DateTime date = DateTime.MinValue;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                isValid = DateTime.TryParseExact(input, "dd.MM.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (isValid)
                {
                    if (date > DateTime.Now)
                    {
                        Console.WriteLine("Datum ne sme biti u budućnosti. Pokušajte ponovo.");
                        isValid = false;
                    }
                    else if (earlierDate.HasValue && date.Date < earlierDate.Value.Date)
                    {
                        Console.WriteLine("Drugi datum ne sme biti pre prvog datuma. Pokušajte ponovo.");
                        isValid = false;
                    }
                }
                else
                {
                    Console.WriteLine("Neispravan format datuma. Pokušajte ponovo.");
                }
            }

            date = date.Date.AddHours(23).AddMinutes(59);
            return date;
        }

        public static void GetByPriority()
        {
            var priorities = _reportManagerClient.GetAllPriorities();

            if (priorities.Length == 0)
            {
                Console.WriteLine("\nNema dostupnih prioriteta.");
                return;
            }

            Console.WriteLine("\nDostupni prioriteti:");
            foreach (var priority in priorities)
            {
                Console.WriteLine($"- {priority}");
            }

            string chosenPriority;
            bool isValidPriority = false;

            do
            {
                Console.Write("\nUnesite prioritet: ");
                chosenPriority = Console.ReadLine();

                if (priorities.Contains(chosenPriority))
                {
                    isValidPriority = true;
                }
                else
                {
                    Console.WriteLine("Uneli ste nevalidan prioritet. Molimo unesite jedan od dostupnih prioriteta.");
                }

            } while (!isValidPriority);

            Console.WriteLine($"Izabrali ste prioritet: {chosenPriority}");
            Console.WriteLine(_reportManagerClient.GetAlarmsByPriority(int.Parse(chosenPriority)));
        }

    }
}
