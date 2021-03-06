﻿using ClassLibrary1;
using System;
using System.Linq;
using System.Reflection;

namespace HalloReflections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Reflections!");

            var file = @"C:\Users\rulan\source\repos\ppedvAG\CSharpFortgeschritten_18_05_2020\HalloReflections\ClassLibrary1\bin\Debug\netstandard2.0\ClassLibrary1.dll";

            var ass = Assembly.LoadFrom(file);

            foreach (var item in ass.GetTypes())
            {
                Console.WriteLine($"{item.Name} {string.Join(",", item.GetInterfaces().Select(x => x.Name))}");
                foreach (var m in item.GetMembers())
                {
                    Console.WriteLine($"\t {m.Name}");
                }
            }

            //über das Interface
            Type classMitWetter = ass.GetTypes().Where(x => x.GetInterfaces().Any(y => y == (typeof(ISagMirDeinWetter)))).FirstOrDefault();
            ISagMirDeinWetter wetterDings = (ISagMirDeinWetter)Activator.CreateInstance(classMitWetter);
            Console.WriteLine("Wetter von dings:" + wetterDings.GetWetter());

            //nur Strings
            Type classMitWetterByName = ass.GetType("ClassLibrary1.Class2");
            object obj = Activator.CreateInstance(classMitWetterByName);
            MethodInfo mi = classMitWetterByName.GetMethod("GetWetter");
            object res = mi.Invoke(obj, null);
            Console.WriteLine($"per string geladen: {res}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
