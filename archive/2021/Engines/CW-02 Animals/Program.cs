using System;

namespace Animals
{
    public class Program
    {
        class Animal
        {
            virtual public void Roar()
            {
                Console.WriteLine("Zzzz");
            }
        }

        class Dog : Animal
        {
            override public void Roar()
            {
                Console.WriteLine("Gav-Gav");
            }
        }
        class Cat : Animal
        {
            override public void Roar()
            {
                Console.WriteLine("Meow-Meow");
            }
        }

        class Fox : Animal
        {
            override public void Roar()
            {
                Console.WriteLine("Kon-Kon");
            }
        }
        class Tanuki : Animal
        {
            override public void Roar()
            {
                Console.WriteLine("Pon-Pon");
            }
        }
        class Boozer : Animal
        {
            override public void Roar()
            {
                Console.WriteLine("Blew");
            }
        }

        public static void Main()
        {
            Animal[] a =
            {
                new(),
                new Dog(),
                new Cat(),
                new Fox(),
                new Tanuki(),
                new Boozer()
            };

            foreach (var item in a)
                item.Roar();
        }
    }
}
