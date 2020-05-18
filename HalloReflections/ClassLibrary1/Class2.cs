namespace ClassLibrary1
{
    public class Class2 : ISagMirDeinWetter
    {
        public Class2()
        {
            System.Console.WriteLine("CTOR von Class2");
        }

        public string GetWetter()
        {
            return "Sonnig bei 22°C";
        }
    }
}
