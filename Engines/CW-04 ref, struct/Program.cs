using System;

namespace CW_04
{
    class Program : IDisposable
    {
        struct Test
        {
            public int x;
            public int y;
        }
        public static void Main()
        {
            var a = 10;
            var b = 10;
            var rtest = new Test() { x = a, y = b };
            var test = rtest;
            var outVar = 5;
            TryRefTest(a, ref b, ref rtest, test, out outVar);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(rtest);
            Console.WriteLine(test);
            Console.WriteLine(outVar);
        }

        public void Dispose()
        {

        }

        static Boolean TryRefTest(Int32 a, ref Int32 b, ref Test rtest, Test test, out Int32 outVar)
        {
            a = 5;
            b = 5;
            rtest = new Test() { x = a, y = b };
            test = rtest;
            outVar = 10;
            return true;
        }
    }
}