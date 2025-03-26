using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingAppv2.Models
{
    public enum OrderStatus
    {
        Nowe,
        WMagazynie,
        WWysylce,
        ZwróconoDoKlienta,
        Błąd,
        Zamknięte
    }

    public enum ClientType
    {
        Firma,
        OsobaFizyczna
    }

    public enum PaymentMethod
    {
        Karta,
        GotówkaPrzyOdbiorze
    }
}
