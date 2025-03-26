using OrderProcessingAppv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingAppv2.Services
{
    public class OrderService
    {
        private List<Order> _orders = new List<Order>();

        public void UtworzPrzykladoweZamowienie()
        {
            var order = new Order
            {
                Produkt = "Laptop Dell XPS",
                CenaJednostkowa = 3000,
                Ilosc = 1,
                TypKlienta = ClientType.Firma,
                NazwaKlienta = "Firma XYZ",
                AdresUlica = "ul. Przykładowa 12",
                AdresKod = "00-123",
                AdresMiejscowosc = "Warszawa",
                SposobPlatnosci = PaymentMethod.GotówkaPrzyOdbiorze
            };

            _orders.Add(order);
            Console.WriteLine("[OK] Przykładowe zamówienie utworzone!");
        }

        public void UtworzZamowienieZInputu()
        {
            try
            {
                Console.Write("Podaj nazwę produktu: ");
                string produkt = Console.ReadLine();

                Console.Write("Podaj cenę jednostkową: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal cena) || cena <= 0)
                {
                    Console.WriteLine("[BŁĄD] Nieprawidłowa cena.");
                    return;
                }

                Console.Write("Podaj ilość: ");
                if (!int.TryParse(Console.ReadLine(), out int ilosc) || ilosc <= 0)
                {
                    Console.WriteLine("[BŁĄD] Nieprawidłowa ilość.");
                    return;
                }

                Console.Write("Typ klienta (1 - Firma, 2 - Osoba fizyczna): ");
                string typInput = Console.ReadLine();
                ClientType typKlienta = typInput == "1" ? ClientType.Firma :
                                        typInput == "2" ? ClientType.OsobaFizyczna :
                                        throw new Exception("Nieprawidłowy typ klienta.");

                Console.Write("Nazwa klienta: ");
                string nazwaKlienta = Console.ReadLine();

                Console.Write("Ulica i nr: ");
                string ulica = Console.ReadLine();
                Console.Write("Kod pocztowy: ");
                string kod = Console.ReadLine();
                Console.Write("Miejscowość: ");
                string miasto = Console.ReadLine();

                Console.Write("Sposób płatności (1 - Karta, 2 - Gotówka przy odbiorze): ");
                string platnoscInput = Console.ReadLine();
                PaymentMethod sposobPlatnosci = platnoscInput == "1" ? PaymentMethod.Karta :
                                                 platnoscInput == "2" ? PaymentMethod.GotówkaPrzyOdbiorze :
                                                 throw new Exception("Nieprawidłowy sposób płatności.");

                var order = new Order
                {
                    Produkt = produkt,
                    CenaJednostkowa = cena,
                    Ilosc = ilosc,
                    TypKlienta = typKlienta,
                    NazwaKlienta = nazwaKlienta,
                    AdresUlica = ulica,
                    AdresKod = kod,
                    AdresMiejscowosc = miasto,
                    SposobPlatnosci = sposobPlatnosci
                };

                _orders.Add(order);
                Console.WriteLine("[OK] Zamówienie zostało utworzone!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD] Błąd: {ex.Message}");
            }
        }

        public void PrzekazDoMagazynu()
        {
            var order = WybierzZamowienieDoPrzetworzenia();
            if (order == null) return;

            if (order.Kwota >= 2500 && order.SposobPlatnosci == PaymentMethod.GotówkaPrzyOdbiorze)
            {
                order.Status = OrderStatus.ZwróconoDoKlienta;
                Console.WriteLine("[BŁĄD] Zamówienie zwrócone do klienta (gotówka + kwota >= 2500).");
                return;
            }

            order.Status = OrderStatus.WMagazynie;
            Console.WriteLine("[MAGAZYN] Zamówienie przekazane do magazynu.");
        }

        public async void PrzekazDoWysylki()
        {
            var order = WybierzZamowienieDoPrzetworzenia();
            if (order == null) return;

            if (order.Status == OrderStatus.Zamknięte)
            {
                Console.WriteLine("[BŁĄD] Zamówienie zostało już zamknięte – nie można ponownie wysłać.");
                return;
            }

            if (string.IsNullOrWhiteSpace(order.AdresUlica) ||
                string.IsNullOrWhiteSpace(order.AdresKod) ||
                string.IsNullOrWhiteSpace(order.AdresMiejscowosc))
            {
                order.Status = OrderStatus.Błąd;
                Console.WriteLine("[BŁĄD] Brak adresu dostawy – status zmieniony na Błąd.");
                return;
            }

            order.Status = OrderStatus.WWysylce;
            Console.WriteLine("[WYSYŁKA] Zamówienie przekazane do wysyłki...");

            await Task.Delay(5000);
            order.Status = OrderStatus.Zamknięte;
            Console.WriteLine("[OK] Zamówienie zostało wysłane i zamknięte.");
        }

        public void PrzegladZamowien()
        {
            if (!_orders.Any())
            {
                Console.WriteLine("[PRZEGLĄD] Brak zamówień.");
                return;
            }

            foreach (var order in _orders)
            {
                Console.WriteLine(order);
                Console.WriteLine("--------------------------------");
            }
        }

        private Order WybierzZamowienieDoPrzetworzenia()
        {
            if (!_orders.Any())
            {
                Console.WriteLine("[UWAGA] Brak dostępnych zamówień.");
                return null;
            }

            Console.WriteLine("Podaj ID zamówienia:");
            string input = Console.ReadLine();

            if (Guid.TryParse(input, out Guid id))
            {
                var order = _orders.FirstOrDefault(o => o.Id == id);
                if (order == null)
                {
                    Console.WriteLine("[BŁĄD] Nie znaleziono zamówienia o podanym ID.");
                }
                return order;
            }

            Console.WriteLine("[BŁĄD] Nieprawidłowy format ID.");
            return null;
        }
    }
}