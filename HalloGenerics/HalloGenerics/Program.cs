using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace HalloGenerics
{
    class Program
    {

        static void GibtMitEinenType(Type t)
        {
            Console.WriteLine($"Danke für den {t.Name}");
        }

        static void GibtMitEinenTypeUndDreiBeispiele(Type t, object b1, object b2, object b3)
        {
            //...
        }

        static void GibMitEinenType<T>()
        {
            Console.WriteLine($"Danke für den {typeof(T).Name}");
        }

        static void GibMitEinenTypeMit3Beispielen<T>(T b1, T b2, T b3) where T : MyClass
        { }

        static void GibMitEineMyClass<T>(T b1) where T : MyClass
        { }

        static void GibMitEineMyClass<T>(in T b1) where T : MyClass
        { }

        static void GibMitEineMyClass2<T>(T b1) where T : MyClass
        { }

        static void GibMirEinenTypeMit2Beispielen<T, K, X>(T b1, T b2, K b3, K b4, X b5, X b6)
        { }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GibtMitEinenType(typeof(DateTime));
            GibMitEinenType<DateTime>();
            //GibMitEinenTypeMit3Beispielen<DateTime>(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-3));
            GibMitEinenTypeMit3Beispielen<MyClass>(null, null, null);
            GibMitEinenTypeMit3Beispielen<MyClass2>(null, null, null);

            GibMitEineMyClass<MyClass>(new MyClass2());
            //GibMitEineMyClass<MyClass2>(new MyClass());

            var myGen = new MyClassGen<int>();
            var myGen2 = new MyClassGen2<MyClass2>();
            myGen2.GetAll();
         //  Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }

    class MyClassGen<T>
    {
        public void ZeigeType()
        {
            //T dings;
            Console.WriteLine(typeof(T).Name);
        }
    }

    class MyClassGen2<T> : MyClassGen<T>
    {
       public  IEnumerable<T> GetAll()
        {
            return null;
        }
    }

    class MyClass2 : MyClass { }
    class MyClass
    {
        public MyClass()
        {

        }
        public MyClass(int z)
        {

        }

    }
}
