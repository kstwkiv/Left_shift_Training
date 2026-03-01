class Program{
    interface IAnimal
    {
        void bark();
    }
    interface Ibird
    {
        void swim();
    }
    class Dog : IAnimal
    {
        public void bark()
        {
            Console.WriteLine("I am barking.");
        }
    }
    class Duck : IAnimal,Ibird
    {
        public void bark()
        {
            Console.WriteLine("I am Barking");
        }
        public void swim()
        {
            Console.WriteLine("I am swimming");
        }
    }

    static void Main(string[] args)
    {
        Dog dog= new Dog();
        dog.bark();
        Duck duck=new Duck();
        duck.swim();
        duck.bark();
    }
}