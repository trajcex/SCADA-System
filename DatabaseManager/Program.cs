using DatabaseManager.TagServiceReference;
using System;
using DatabaseManager.CoreServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class Program
    {
        static AuthenticationClient service = new AuthenticationClient();
        static bool loggedIn = false;
        static string loginToken = "";
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                if (loggedIn)
                {
                    HandleMenu(loginToken);
                }
                else
                {
                    Printer.DisplayLoginRegister(); 
                    running = HandleLoginRegister(); 
                }
            }

            Console.WriteLine("Izlaz iz aplikacije. Doviđenja!");
        }

        public static bool HandleLoginRegister()
        {
            Console.Write("Unesite opciju: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                {
                    (string username, string password) = GetCredentials();
                    string token = Login(username, password);
                    if (service.IsUserAuthenticated(token))
                    {
                        Console.WriteLine("Ulogovao si se.");
                        loggedIn = true;
                        loginToken = token;
                    }
                    else
                    {
                        Console.WriteLine("Neuspesan login.");
                    }
                    break;
                    }
                case "x":
                {
                    return false;
                }
                default:
                {
                    Console.WriteLine("Nevažeća opcija. Molimo pokušajte ponovo.");
                    break;
                }
            }
            return true;
        }

        public static (string, string) GetCredentials()
        {
            string username = "";
            string password = "";

            while (true)
            {
                Console.Write("Unesite korisničko ime: ");
                username = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Korisničko ime ne može biti prazno. Molimo pokušajte ponovo.");
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Unesite lozinku: ");
                password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Lozinka ne može biti prazna. Molimo pokušajte ponovo.");
                }
                else
                {
                    break;
                }
            }

            return (username, password);
        }

        public static string Login(string username, string password)
        {
            string token = service.Login(username, password);
            return token;
        }

        public static bool Register(string username, string password)
        {
            bool success = service.Registration(username, password);
            return success;
        }

        public static void HandleMenu(string token)
        {
            bool menuRunning = true;

            while (menuRunning)
            {
                Printer.DisplayMenu();
                Console.Write("Unesite opciju: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    {
                        (string username, string password) = GetCredentials();

                        bool success = Register(username, password);
                        if (success)
                        {
                            Console.WriteLine("Uspesna registracija.");
                        }
                        else
                        {
                            Console.WriteLine("Neuspesna registracija, vec postoji korisnik sa unetim korisnickim imenom.");
                        }
                        break;
                    }
                    case "2":
                        TagManager.addTag();
                        break;
                    case "3":
                        TagManager.deleteTag();
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "0":
                    {
                        Console.WriteLine("Odjavljivanje...");
                        service.Logout(loginToken);
                        loginToken = "";
                        loggedIn = false;
                        menuRunning = false;
                        break;
                    }
                    default:
                        Console.WriteLine("Nevažeća opcija. Molimo pokušajte ponovo.");
                        break;
                }
            }
        }

    }
}
