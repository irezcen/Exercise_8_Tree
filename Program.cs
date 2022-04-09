using System;

namespace zadanie_10_drzewa
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree zwierzaki = new Tree();
            
            string[] doDodania = { "slon", "zolw", "lampart", "tygrys", "kondor", "jastrzab", "lemur", "sum", "lis" };
            foreach (string el in doDodania)
            {
                zwierzaki.AddAnimal(el);
            }
            foreach(string el in doDodania)
            {
                Console.WriteLine(el + ": " + zwierzaki.CheckAnimal(el));
            }
            
            zwierzaki.RemoveAnimal("sum");
            Console.WriteLine("sum: " + zwierzaki.CheckAnimal("sum"));
            
            zwierzaki.AddAnimal("kot");
            Console.WriteLine("kot: " + zwierzaki.CheckAnimal("kot"));
            
            Random rnd = new Random();
            string zwierzak = "";
            for(int i = 0; i < 10000; i++)
            {
                for(int j = 0; j < rnd.Next(1, 15); j++)
                {
                    zwierzak = zwierzak + "c";
                }
                zwierzaki.AddAnimal(zwierzak);
                zwierzak = "";
            }

            for (int i = 0; i <100; i++)
            {
                for (int j = 0; j < rnd.Next(1, 15); j++)
                {
                    zwierzak = zwierzak + "c";
                }
                zwierzaki.RemoveAnimal(zwierzak);
                zwierzak = "";
            }
            
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < rnd.Next(1, 15); j++)
                {
                    zwierzak = zwierzak + "c";
                }
                zwierzaki.AddAnimal(zwierzak);
                zwierzak = "";
            }
            
            zwierzaki.SaveAnimals();
        }
    }
}
