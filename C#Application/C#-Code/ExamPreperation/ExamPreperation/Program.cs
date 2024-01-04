
namespace Inheritance_Polymorphism_Static_Fields_Function_Class
{

    // Example Inheritance, Polymorphism, static fields, static function, static class 
    class Program
    {
        static void Main(string[] args)
        {
            Car privateCar = new Car("PrivateCar", "purple", "secret");
            Car mercedes = new VW("Mercedes", "white", 90); // you can use the base class
            VW vw = new VW("Golf5", "White", 90);
            Car vw2 = new VW("Golf5", "White", 90);
            Object vw3 = new VW("Golf5", "White", 90);

            Car[] cars = new Car[2];
            cars[0] = vw;
            cars[1] = vw2;
            
            foreach (var cari in cars)
            {
                Console.WriteLine("Foreach " +  cari.ToString());
            }

            ;
           
            //Console.WriteLine(vw.ToString());
            
            Console.WriteLine(privateCar.carConfig()); // privateCar purple secret
            Console.WriteLine(vw.carConfig()); // Golf5 White 90
            Console.WriteLine(vw.ToString());// Golf5 White 90
            Console.WriteLine(vw.Equals(mercedes)); // False
            Console.WriteLine(vw.Equals(vw2)); // True

            Car car = new Car();
            Console.WriteLine("Number of objects {0}", Car.CarGenerated); // 0

            VW standardVW = VW.generateVW();
            Console.WriteLine(standardVW.ToString());

            Console.WriteLine("Number of objects {0}", VW.CarGenerated); // 0

            double x = Math.Round(3.4);
            Console.WriteLine(x.ToString()); // 3
            
        }
    }
}

