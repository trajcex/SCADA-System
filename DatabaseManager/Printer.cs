using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public  class Printer
    {
        public static void DisplayLoginRegister()
        {
            Console.WriteLine("\n----- Dobrodošli -----");
            Console.WriteLine("1. Prijava korisnika");
            Console.WriteLine("x. Izlaz");
        }
        public static void DisplayMenu()
        {
            Console.WriteLine("\n----- Meni -----");
            Console.WriteLine("1. Registracija korisnika");
            Console.WriteLine("2. Dodavanje taga");
            Console.WriteLine("3. Uklanjanje taga");
            Console.WriteLine("4. Uključivanje/isključivanje skeniranja taga");
            Console.WriteLine("5. Upis vrednosti taga");
            Console.WriteLine("6. Prikaz trenutne vrednosti taga");
            Console.WriteLine("0. Odjava");
        }
    }
}
