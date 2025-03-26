using System;
using OrderProcessingAppv2.Services;

namespace OrderProcessingAppv2
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new OrderService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Utwórz przykładowe zamówienie");
                Console.WriteLine("2. Utwórz zamówienie ręcznie");
                Console.WriteLine("3. Przekaż do magazynu");
                Console.WriteLine("4. Przekaż do wysyłki");
                Console.WriteLine("5. Przegląd zamówień");
                Console.WriteLine("6. Wyjście");

                Console.Write("Wybierz opcję: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        service.UtworzPrzykladoweZamowienie();
                        break;
                    case "2":
                        service.UtworzZamowienieZInputu();
                        break;
                    case "3":
                        service.PrzekazDoMagazynu();
                        break;
                    case "4":
                        service.PrzekazDoWysylki();
                        break;
                    case "5":
                        service.PrzegladZamowien();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("[UWAGA] Nieprawidłowa opcja.");
                        break;
                }
            }

            Console.WriteLine("Do zobaczenia!");
        }
    }
}