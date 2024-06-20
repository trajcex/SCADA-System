using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportManager.ServiceReference1;

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
                    Console.WriteLine("Izabrali ste opciju: Registracija korisnika");
                    break;
                case "2":
                    Console.WriteLine("Izabrali ste opciju: Dodavanje taga");
                    break;
                case "3":
                    GetByTag();
                    break;
                case "4":
                    Console.WriteLine(_reportManagerClient.GetLastValueAi());
                    break;
                case "5":
                    Console.WriteLine(_reportManagerClient.GetLastValueDi());
                    break;
                case "6":
                    Console.WriteLine("Izabrali ste opciju: Prikaz trenutnih vrednosti izlaznih tagova");
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
    }
}
