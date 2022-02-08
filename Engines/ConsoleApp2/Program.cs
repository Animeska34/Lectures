using System;

namespace ConsoleApp2
{
    public class Program
    {
        public delegate void Test();
        public event Test? DoTest;
        public Action<Int32, Int32, Int32>? action;
        public Func<Int32, Int32, String>? func;
        public static void Main()
        {
            A a1 = new();
            A a2 = new();
            B b = new();
            Program p = new Program();

            a1.Register(p);
            a2.Register(p);
            b.Register(p);
            p.DoTest?.Invoke();

            p.func += p.Add;
            Console.WriteLine(p.func?.Invoke(4, 3));
        }

        String Add(Int32 a, Int32 b)
        {
            return (a+b).ToString();
        }
    }

    public class A
    {
        public void Register(Program p)
        {
            p.DoTest += Act;
        }

        public void Act()
        {
            Console.WriteLine("A Exec");
        }
    }

    public class B
    {
        public void Register(Program p)
        {
            p.DoTest += Act;
        }

        public void Act()
        {
            Console.WriteLine("B Exec");
        }
    }
}
