using System;

namespace SimpleCalc
{
    public class Program
    {
        public delegate void Action(Single value1, Single value2);
        public static void Exe(Single value1, Single value2, Action act)
        {
            act.Invoke(value1, value2);
        }

        public static void Ran(Action act, Int16 num = 1, Int32 min = 0, Int32 max = 100)
        {
            Random ran = new();
            for (Int16 i = 0; i < num; i++)
            {
                Exe(ran.Next(max - min) + min, ran.Next(max - min) + min, act);
                if (i < num - 1)
                    Console.WriteLine("--------");
            }
        }

        public static void Add(Single value1, Single value2)
        {
            Console.WriteLine($"{value1} + {value2} = {value1 + value2}");
        }

        public static void Sub(Single value1, Single value2)
        {
            Console.WriteLine($"{value1} - {value2} = {value1 - value2}");
        }

        public static void Mul(Single value1, Single value2)
        {
            Console.WriteLine($"{value1} * {value2} = {value1 * value2}");
        }

        public static void Div(Single value1, Single value2)
        {
            Console.WriteLine($"{value1} / {value2} = {(value2 > 0 ? (value1 / value2).ToString() : "NaN")}");
        }

        public static void Main(String[] args)
        {
            Action act = Add;
            act += Sub;
            act += Mul;
            act += Div;

            if (args.Length == 2 && Single.TryParse(args[0], out var value1) && Single.TryParse(args[1], out var value2))
                Exe(value1, value2, act);
            else if (args.Length == 1 && Int16.TryParse(args[0], out var value))
                Ran(act, value);
            else if (args.Length == 3 && Int32.TryParse(args[1], out var v1) && Int32.TryParse(args[2], out var v2))
            {
                if (Int16.TryParse(args[0], out var num))
                    Ran(act, num, v1, v2);
                else switch (args[0].ToLower())
                    {
                        case "add":
                            Exe(v1, v2, Add);
                            break;
                        case "sustract":
                        case "sub":
                            Exe(v1, v2, Sub);
                            break;
                        case "multiply":
                        case "mul":
                            Exe(v1, v2, Mul);
                            break;
                        case "divide":
                        case "div":
                            Exe(v1, v2, Div);
                            break;
                        default:
                            Console.WriteLine($"Unknown argument \"{args[0]}\"");
                            break;
                    }
            }
            else
                Ran(act);
        }
    }
}