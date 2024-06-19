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
            Console.WriteLine("1. Registracija korisnika");
            Console.WriteLine("2. Prijava korisnika");
        }
        public static void DisplayMenu()
        {
            Console.WriteLine("1. Dodavanje taga");
            Console.WriteLine("2. Uklanjanje taga");
            Console.WriteLine("3. Uključivanje/isključivanje skeniranja taga");
            Console.WriteLine("4. Upis vrednosti taga");
            Console.WriteLine("5. Prikaz trenutne vrednosti taga");
            Console.WriteLine("6. Odjava");
        }
        public static void TagTypes()
        {
            Console.WriteLine("1. Digitalni Input");
            Console.WriteLine("2. Digitalni Output");
            Console.WriteLine("3. Analogni Input");
            Console.WriteLine("4. Analogni Output");
        }
    }
}
