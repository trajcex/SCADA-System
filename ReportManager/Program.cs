using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
        private void DisplayMenu()
        {
            Console.WriteLine("\n----- Meni -----");
            Console.WriteLine("1. Registracija korisnika");
            Console.WriteLine("2. Dodavanje taga");
            Console.WriteLine("3. Uklanjanje taga");
            Console.WriteLine("4. Uključivanje/isključivanje skeniranja taga");
            Console.WriteLine("5. Promena vrednosti taga");
            Console.WriteLine("6. Prikaz trenutnih vrednosti izlaznih tagova");
            Console.WriteLine("0. Odjava");
        }
    }
}
