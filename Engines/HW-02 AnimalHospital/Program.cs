using System;
using CUI;

namespace AnimalHospital
{
    public class Program
    {
        //Animal queue
        public List<Animal> animals = new List<Animal>();
        //Calling update for all animals
        public delegate void UpdateTime();
        public event UpdateTime? Update;

        public Animal? Next()
        {
            if (animals.Count > 0)
            {
                var patient = animals[0];
                animals.RemoveAt(0);
                return patient;
            }
            else return null;
        }

        public void Register()
        {
            //Selecting animal
            Console.Clear();
            Console.WriteLine("Please select animal");
            Selector selector = new(new String[] { "Boozer", "Ookami", "Tanuki", "Fox", "Dog", "Neko", "Tiger", "Usagi", "Other" });
            selector.ElementWidth = 25;
            selector.ESCReturn = false;
            var selection = selector.Show();

            //Setting name
            Console.Clear();
            Console.WriteLine($"Please enter name of {selection}");
            var name = Console.ReadLine();
            Console.Clear();
            Animal animal = selection switch
            {
                "Boozer" => new Boozer(),
                "Ookami" => new Ookami(),
                "Tanuki" => new Tanuki(),
                "Dog" => new Dog(),
                "Fox" => new Fox(),
                "Neko" => new Neko(),
                "Tiger" => new Tiger(),
                "Usagi" => new Usagi(),
                _ => new Animal(),
            };
            animal.Name = name ?? String.Empty;

            //Setting visit type
            selector.Clear();
            selector.Add(Enum.GetNames<VisitType>());
            Console.Clear();
            Console.WriteLine("Please select visit type");
            Enum.TryParse(selector.Show(), out animal.visitType);

            if (animal.visitType != VisitType.Diagnostics)
            {
                //Setting diagnosis
                selector.Clear();
                selector.Add(Enum.GetNames<Diagnosis>());
                Console.Clear();
                Console.WriteLine("Please select diagnosis");
                Enum.TryParse(selector.Show(), out animal.diagnosis);
            }

            //Setting affected body parts
            selector.Clear();
            var parts = animal.BodyPartsAsString();
            var count = parts.Length - 1;
            selector.Add(parts);
            selector.ESCReturn = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select single or multi affected body parts.\nPress ESC to continue.\n");
                selection = selector.Show();
                if (selection == "" || count <= 0)
                    break;
                count--;
                animal.affectedParts.Add(Enum.Parse<BodyPart>(selection));
                selector.Remove(selection);
            }
            Console.WriteLine("Registered");
            //Finishing registration
            animal.Register(this);
            animals.Add(animal);
            Console.Clear();
            Console.WriteLine($"{animal}\nWas registered as {animals.Count} patient");
            Console.ReadKey();
            Console.Clear();
        }

        static void Main()
        {
            Program program = new();
            Selector selector = new(new String[] { "Next patient", "Register new patient" });
            selector.ElementWidth = 25;
            selector.ESCReturn = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Pacients remainig: {program.animals.Count}");
                var selection = selector.Show();
                switch (selection)
                {
                    case "Next patient":
                        var patient = program.Next();
                        Console.Clear();
                        Console.WriteLine(patient != null ? $"Next pacient: {patient}" : "No patients");
                        Console.ReadKey();
                        program.Update?.Invoke();
                        break;
                    case "Register new patient":
                        program.Register();
                        break;
                    case "":
                        return;
                }
            }

        }
    }
}