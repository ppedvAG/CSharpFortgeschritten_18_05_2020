using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpFeatures
{
    class Program
    {


        static void Main(string[] args)
        {
            ZeigeName(new Katze() { Name = "Bella" });
            ZeigeName(new Hund() { Name = "Myra" });

            var eineKlasse = new { Anzahl = 12, Text = "lala" };
            Tuple<int, string, Katze> t1 = new Tuple<int, string, Katze>(21, "Mize", new Katze());

            var meinZeug = Zeug;
            meinZeug.name = "lala";

            int zahl = 17;
            Console.WriteLine(zahl);
            AlteVersion_ValueByRef(ref zahl);
            Console.WriteLine(zahl);


            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        static void AlteVersion_ValueByRef(ref int zahl) => zahl += 7;

        static ref int GetRefZahl(ref int zahl)
        {
            zahl *= 2;
            return ref zahl;
        }


        private int zahl;
        public int Zahl
        {
            get => zahl;
            set => zahl = value;
        }


        static (int zahl, string name) Zeug => (57, "lala");

        static void ZeigeName(Object obj)
        {
            //casting: machen wir heute so nicht mehr (.NET 1.1 style)
            if (obj is Katze) //typprüfung
            {
                Katze castKatze = (Katze)obj; //casting
            }

            //boxing (ab .NET 2.0) 
            Katze zielKatze = obj as Katze;
            if (zielKatze != null)
            {

            }

            //patter matching (ab vs2019)
            if (obj is Katze zielKatze2)
            {

            }
        }
    }

    class Hund
    {
        public string Name { get; set; }
    }
    class Katze : Object
    {
        public string Name { get; set; }
    }

}
