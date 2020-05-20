using Calculator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Calc calc = new Calc();
            Console.WriteLine("Gib Zahl !");
            int input = Convert.ToInt32(Console.ReadLine());
            int res = calc.Sum(input, 5);
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}

