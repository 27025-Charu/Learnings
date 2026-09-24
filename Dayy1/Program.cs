using System;

namespace AbstractInterfaceDemo
{
    public abstract class Animal
    {

        protected string Name;

        public abstract string Sound { get; }

        public abstract void MakeSound();

        public virtual void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping.");
        }

        public void Eat()
        {
            Console.WriteLine($"{Name} is eating.");
        }

        protected Animal(string name)
        {
            Name = name;
            Console.WriteLine($"Animal constructor ran for {name}");
        }

        public void Describe()
        {
            Console.WriteLine($"I am an animal named {Name}");
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name)
        {
            Console.WriteLine($"Dog constructor ran for {name}");
        }

        public override string Sound => "Woof";

        public override void MakeSound() => Console.WriteLine(Sound);

        public override void Sleep()
        {
            Console.WriteLine($"{Name} curls up in a bed.");
        }

        public void Fetch()
        {
            Console.WriteLine($"{Name} fetches the ball.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Rex");
            Console.WriteLine();

            Animal a = dog;
            a.MakeSound();
            a.Sleep();
            a.Eat();
            Console.WriteLine() ;
            Dog d = new Dog("Rex");
            d.MakeSound();
            d.Sleep();
            d.Eat();


        }
    }
}