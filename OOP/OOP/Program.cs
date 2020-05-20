using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tier tier = new Tier();

            Katze k = new Katze();
            Tier tier2 = new Katze();
            Hund h = new Hund();

            EinTierSollLautMachen(h);
            EinTierSollLautMachen(k);

            EinTierSollKommen(h);
            EinTierSollKommen(k);

            var becker = new Schwimmbecken() { Schwimmmer = k };
            Schimmbecken(k);
            Schimmbecken(h);

            Console.ReadKey();
            Console.WriteLine("Ende");
        }
        static void Schimmbecken(IKannSchwimmen schwimmer)
        {
            schwimmer.Schwimmen();
        }

        static void EinTierSollLautMachen(Tier tier)
        {
            tier.MachLaut();
        }
        static void EinTierSollKommen(Tier tier)
        {
            tier.Rufen();
            //tier.geheim = "lala";
        }

    
    }

    public class Schwimmbecken
    {
        public IKannSchwimmen Schwimmmer { get; set; }

        
    }

    public abstract class Tier
    {
        internal string Name { get; set; }
        public abstract void MachLaut();

        private protected string geheim;

        public virtual void Rufen()
        {
            Console.WriteLine("Das Tier könnte vielleicht kommen");
        }
    }

    public interface IKannSchwimmen
    {
        void Schwimmen();
        int MinutenLuftAnhalten { get; set; }
    }

    public interface IKannFliegen
    {
        void Fliegen();


    }

    public class Katze : Tier, IKannSchwimmen, IKannFliegen
    {

        public int AnzahlPfoten { get; set; }
        public int MinutenLuftAnhalten { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Fliegen()
        {

        }

        public override void MachLaut()
        {
            Console.WriteLine("Miau");
            geheim = "Muia!!";
        }

        public override void Rufen()
        {
            //base.Rufen();
            Console.WriteLine("aber die Katze kommt auf keinen Fall!");
        }

        public void Schwimmen()
        {
            Console.WriteLine("Katze schwimmt, mag es aber nicht");
        }
    }

    class Hund : Tier, IKannSchwimmen
    {
        public int MinutenLuftAnhalten { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void MachLaut()
        {
            Console.WriteLine("Wuff");
        }

        public void Schwimmen()
        {
            Console.WriteLine("Der Hund schwimmt und hat spass");
        }
    }
}
