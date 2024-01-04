using System;
namespace Abstract_Classes_Interfaces_Properties_events
{
    public abstract class Person {

       protected string name;
       protected int age; // set to public for LINQ
       protected double height;
       private double salary;
       

       public int Age
        {
            get => age;
            set
            {
                if(value > 16)
                {
                    age = value;
                }
            }
        }

        public Person(string n, int a, double h, double s)
        {
            name = n;
            age = a;
            height = h;
            salary = s;

        }
        public Person(string n, int a, double h)
        {
            name = n;
            age = a;
            height = h;

        }
        
        public Person()
        {

        }

        public abstract string idCard();

        public void sayHello()
        {
            Console.WriteLine("Hello everyone!");
        }

    }
}

