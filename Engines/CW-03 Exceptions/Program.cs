using System;

namespace CW_03
{
    class Program
    {

        static List<UInt64> results = new();
        public static void Main()
        {
            Thread thread = new(Calc);
            thread.Start();
            while (thread.IsAlive || results.Count > 0)
            {
                if (results.Count > 0)
                {
                    Console.WriteLine(results[0]);
                    results.RemoveAt(0);
                }
            }
        }

        static void Calc()
        {
            UInt64 lir = 1;
            for (UInt64 i = 1; i < 50; i++)
            {
                lir *= i;
                results.Add(lir);
            }
        }
    }
}