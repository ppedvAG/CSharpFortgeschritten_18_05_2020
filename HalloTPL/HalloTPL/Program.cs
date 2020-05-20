using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo TPL ***");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, () => Thread.Sleep(5000));
            //Parallel.For(0, 100000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));
            //Parallel.For(0, 1000, i =>new Zähler());

            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(2800);
                Console.WriteLine("T1 fertig");
                try
                {
                    throw new ExecutionEngineException();
                }
                catch (Exception)
                {
                    throw new FieldAccessException();
                }
            });

            t1.ContinueWith(t => Console.WriteLine("T1C Ohne Optionen wird immer ausgeführt"), TaskContinuationOptions.None);

            t1.ContinueWith(t => Console.WriteLine("T1 Alles OK"), TaskContinuationOptions.OnlyOnRanToCompletion);

            t1.ContinueWith(t => Console.WriteLine($"T1 ERROR: {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);



            Task<long> t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(1700);
                Console.WriteLine("T2 fertig");
                return 345678934567;
            });

            var t2c = t2.ContinueWith(t => //t == t2
                {
                    Console.WriteLine($"Continue {t.Result}");
                });

            t1.Start();

            //t1.ContinueWith(t=>t2.Start()); //t2 erst startet wenn t1 fertig
            t2.Start();

            Console.WriteLine($"Result of T2: {t2.Result}");//wartet automatisch
            //t2.Wait();
            //t1.Wait();

            //Task.WaitAll(t1, t2);

            //var s = new ThreadStart(MachWas);

            Timer timer = new Timer(WennFertig);
            timer.Change(100, 100);

            long result = IchWillEineMethode(MachWas, 17);

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        public delegate byte EineDooferDelegate();
        //public static long IchWillEineMethodeMODERN(Func<byte> dele, int anzahl) { return IchWillEineMethode(dele, anzahl); }
        public static long IchWillEineMethode(EineDooferDelegate dele, int anzahl)
        {
            long result = 0;

            for (int i = 0; i < anzahl; i++)
            {
                result += dele.Invoke();
            }
            return result;
        }

        private static void WennFertig(object state)
        {
            Console.WriteLine("fertig !!1");
            ((Timer)state).Change(500, 2000);
        }

        private static byte MachWas()
        {
            Console.WriteLine("Grüße vom alten Thread");
            return 12;
        }

        static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }

    class Zähler
    {
        public Zähler()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
