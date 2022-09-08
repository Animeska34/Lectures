using System;

namespace AnimalHospital
{
    public enum VisitType
    {
        Diagnostics,
        Operation,
        MedicalTreatment,
        Consultation,
        Other
    }
    public enum Diagnosis
    {
        Undefined,
        Anthrax,
        BlackQuarter,
        FootAndMouthDisease,
        Rabies,
        BlueTongue,
        Pox,
        BrucellosisOfSheep,
        Tetanus,
        Other
    }
    public enum BodyPart
    {
        Head,
        LeftEye,
        RightEye,
        LeftMimi,
        RightMimi,
        Mouth,
        Nose,
        Neck,
        Chest,
        Stomach,
        LeftBackLeg,
        RightBackLeg,
        LeftFrontLeg,
        RightFrontLeg,
        LeftArm,
        RightArm,
        Tail,
        LeftHorn,
        RightHorn
    }
    public class Animal
    {

        public VisitType visitType = VisitType.Diagnostics;
        public Diagnosis diagnosis = Diagnosis.Undefined;
        public List<BodyPart> affectedParts = new List<BodyPart>();

        private String name = String.Empty;
        public String Name
        {
            get { return name; }
            set
            {
                if (name == String.Empty)
                    name = value;
            }
        }
        public Int32 TimeRemains { get; private set; } = 0;
        private void Update()
        {
            TimeRemains--;
        }

        public virtual BodyPart[] BodyParts()
        {
            return Enum.GetValues<BodyPart>();
        }
        public String[] BodyPartsAsString()
        {
            List<BodyPart> parts = new List<BodyPart>(BodyParts());
            List<String> partsString = new List<String>();
            foreach (var item in parts)
            {
                partsString.Add(item.ToString());
            }
            return partsString.ToArray();
        }

        public void Register(Program p)
        {
            TimeRemains = p.animals.Count;
            p.Update += Update;
        }
        public virtual void Roar()
        {
            Console.WriteLine("Zzzz");
        }
        public override string ToString()
        {
            String tmp;
            if (diagnosis == Diagnosis.Undefined)
                tmp = $"Animal {Name} {visitType}\nAffected body parts:\n";
            else
                tmp = $"Animal {Name} with {diagnosis} {visitType}\nAffected body parts:\n";
            foreach (var item in affectedParts)
                tmp += item.ToString() + "\n";
            return tmp;
        }
    }
    public class Boozer : Animal
    {
        public override void Roar()
        {
            Console.WriteLine("Blew-Blew");
        }
        public override BodyPart[] BodyParts()
        {
            return new BodyPart[] {
                BodyPart.Head,
                BodyPart.LeftEye,
                BodyPart.RightEye,
                BodyPart.LeftMimi,
                BodyPart.RightMimi,
                BodyPart.Mouth,
                BodyPart.Nose,
                BodyPart.Neck,
                BodyPart.Chest,
                BodyPart.Stomach,
                BodyPart.LeftBackLeg,
                BodyPart.RightBackLeg,
                BodyPart.LeftArm,
                BodyPart.RightArm
            };
        }
        public override string ToString()
        {
            return base.ToString().Replace("Animal", "Boozer");
        }
    }
    public class Ookami : Animal
    {
        public override void Roar()
        {
            Console.WriteLine("U-u-u-u");
        }

        public override BodyPart[] BodyParts()
        {
            return new BodyPart[] {
                BodyPart.Head,
                BodyPart.LeftEye,
                BodyPart.RightEye,
                BodyPart.LeftMimi,
                BodyPart.RightMimi,
                BodyPart.Mouth,
                BodyPart.Nose,
                BodyPart.Neck,
                BodyPart.Chest,
                BodyPart.Stomach,
                BodyPart.LeftBackLeg,
                BodyPart.RightBackLeg,
                BodyPart.LeftFrontLeg,
                BodyPart.RightFrontLeg,
                BodyPart.Tail
            };
        }

        public override string ToString()
        {
            return base.ToString().Replace("Animal", "Ookami");
        }
    }
    public class Tanuki : Ookami
    {
        public override void Roar()
        {
            Console.WriteLine("Pon-Pon");
        }
        public override string ToString()
        {
            return base.ToString().Replace("Ookami", "Tanuki");
        }
    }
    public class Dog : Ookami
    {
        public override void Roar()
        {
            Console.WriteLine("Gav-Gav");
        }
        public override string ToString()
        {
            return base.ToString().Replace("Ookami", "Dog");
        }
    }
    public class Fox : Ookami
    {
        public override void Roar()
        {
            Console.WriteLine("Kon-Kon");
        }
        public override string ToString()
        {
            if (diagnosis == Diagnosis.Undefined)
                return $"Fox {Name} {visitType}";
            else
                return $"Fox {Name} with {diagnosis} {visitType}";
        }
    }
    public class Neko : Animal
    {
        public override void Roar()
        {
            Console.WriteLine("Meow-Meow");
        }
        public override BodyPart[] BodyParts()
        {
            return new BodyPart[] {
                BodyPart.Head,
                BodyPart.LeftEye,
                BodyPart.RightEye,
                BodyPart.LeftMimi,
                BodyPart.RightMimi,
                BodyPart.Mouth,
                BodyPart.Nose,
                BodyPart.Neck,
                BodyPart.Chest,
                BodyPart.Stomach,
                BodyPart.LeftBackLeg,
                BodyPart.RightBackLeg,
                BodyPart.LeftFrontLeg,
                BodyPart.RightFrontLeg,
                BodyPart.Tail
            };
        }
        public override string ToString()
        {
            return base.ToString().Replace("Animal", "Neko");
        }
    }
    public class Tiger : Neko
    {
        public override void Roar()
        {
            Console.WriteLine("R-r-r-r");
        }
        public override string ToString()
        {
            return base.ToString().Replace("Neko", "Tiger");
        }
    }
    public class Usagi : Animal
    {
        public override void Roar()
        {
            Console.WriteLine("????????????");
        }
        public override BodyPart[] BodyParts()
        {
            return new BodyPart[] {
                BodyPart.Head,
                BodyPart.LeftEye,
                BodyPart.RightEye,
                BodyPart.LeftMimi,
                BodyPart.RightMimi,
                BodyPart.Mouth,
                BodyPart.Nose,
                BodyPart.Neck,
                BodyPart.Chest,
                BodyPart.Stomach,
                BodyPart.LeftBackLeg,
                BodyPart.RightBackLeg,
                BodyPart.LeftFrontLeg,
                BodyPart.RightFrontLeg,
                BodyPart.Tail
            };
        }
        public override string ToString()
        {
            return base.ToString().Replace("Animal", "Usagi");
        }
    }
}