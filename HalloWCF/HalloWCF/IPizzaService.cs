using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HalloWCF
{
    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract]
        List<Pizza> GetListe();

        [OperationContract]
        bool Bestelle(Pizza pizza);
    }

    public class Pizza
    {
        public int Nummer { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
    }

}
