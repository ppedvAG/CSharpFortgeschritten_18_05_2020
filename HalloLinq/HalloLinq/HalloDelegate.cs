using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloLinq
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string txt);
    delegate long CalcDele(int aaaaaaaa, int bbbbb);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode; //meinDele.Invoke();
            Action meinDeleAction = EinfacheMethode;
            Action meinDeleActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action meinDeleActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> deleMitParaActio = MethodeMitPara;
            DelegateMitPara deleMitParaAno = (string msg) => { Console.WriteLine(msg); };
            DelegateMitPara deleMitParaAno2 = (msg) => Console.WriteLine(msg);
            DelegateMitPara deleMitParaAno3 = x => Console.WriteLine(x);

            CalcDele calcDele = Minus;  //long res = calcDele.Invoke(4, 6);
            Func<int, int, long> calcFunc = Sum;
            CalcDele calcDeleAno = (int x, int y) => { return x * y; };
            CalcDele calcDeleAno2 = (x, y) => { return x * y; };
            CalcDele calcDeleAno3 = (x, y) => x * y;

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("M"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("M"))
                return true;
            else

                return false;
        }

        private long Minus(int a, int b)
        {
            return a - b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string txt)
        {
            Console.WriteLine(txt);
        }

        public void EinfacheMethode()
        {
            Console.WriteLine("Hallo Welt");
        }
    }
}
