using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingAppv2.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Produkt { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public int Ilosc { get; set; }
        public decimal Kwota => CenaJednostkowa * Ilosc;
        public ClientType TypKlienta { get; set; }
        public string NazwaKlienta { get; set; }
        public string AdresUlica { get; set; }
        public string AdresKod { get; set; }
        public string AdresMiejscowosc { get; set; }
        public PaymentMethod SposobPlatnosci { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Nowe;

        public string PelnyAdres => $"{AdresUlica}, {AdresKod} {AdresMiejscowosc}";

        public override string ToString()
        {
            return $"ID: {Id}\nProdukt: {Produkt}\nCena: {CenaJednostkowa} PLN\nIlość: {Ilosc}\nŁączna Kwota: {Kwota} PLN\n" +
                   $"Klient: {NazwaKlienta} ({TypKlienta})\nAdres: {PelnyAdres}\nPłatność: {SposobPlatnosci}\nStatus: {Status}\n";
        }
    }
}
