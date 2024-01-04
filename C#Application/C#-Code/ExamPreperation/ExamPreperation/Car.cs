using System;
namespace Inheritance_Polymorphism_Static_Fields_Function_Class
{
    public class Car
    {
        
        protected string name;
        protected string color;
        private string privatefield;
        public static uint CarGenerated = 0;



        public Car(string name, string color, string privatefield)
        {
            this.name = name;
            this.color = color;
            this.privatefield = privatefield;
            CarGenerated++;

        }

        // I need this constructor for inharintence bcs in the above ctor is a private field
        public Car(string n, string c)
        {
            this.name = n;
            this.color = c;
            
        }

        public Car()
        {
            CarGenerated--;
        }
        public virtual string carConfig()
        {

            return name + " " + color + " " + privatefield;

        }
        
        public virtual string MyName()
        {
            return name;
        }
        public override string ToString()
        {
            return carConfig();
           // return name + " " + color + " " + privatefield;
        }

        public override bool Equals(object? obj)
        {
            var car = obj as Car;
            return car != null && name == car.name && color == car.color;
        }



    }
}

