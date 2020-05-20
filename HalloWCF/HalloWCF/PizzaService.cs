using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HalloWCF
{
    public class PizzaService : IPizzaService
    {
        public bool Bestelle(Pizza pizza)
        {
            return !pizza.Name.ToLower().Contains("hawai");
        }

        public List<Pizza> GetListe()
        {
            var p1 = new Pizza { Nummer = 1, Name = "Salami", Preis = 8.9m };
            var p2 = new Pizza { Nummer = 2, Name = "Schnecken", Preis = 12.9m };
            var p3 = new Pizza { Nummer = 2, Name = "Hawai", Preis = 4.9m };
            return new[] { p1, p2, p3 }.ToList();
        }
    }
}
