
namespace Abstract_Classes_Interfaces_Properties_events
{

    // Abstract Classes
    class Program
    {
      
        // Example abstract class, interfaces, properties, events
        static void Main(string[] args)
        {
            
            //  Person p = new Person(); not allowed !!

            Person std1 = new Student("Paul", 27, 1.91, 12345);
            // call implemented method from abstract class
            std1.sayHello();
            
            
            //Console.WriteLine(std1.idCard());

            // Abstract Class + Inheritance
            // Arrraz
            Person[] peoples = new Person[2];
            peoples[0] = new Student("Paul", 27, 1.91, 12345);
            peoples[1] = new Student("Lotta", 22, 1.68, 1212);

            
            Console.WriteLine();
            foreach (var people in peoples)
            {
                Console.WriteLine(people.idCard());
            }

            Console.WriteLine();

            // List + IEnumerable
            List<Person> peopleList = new List<Person>();
            peopleList.Add(peoples[0]);
            peopleList.Add(peoples[1]);

            peopleList.ForEach(x => Console.WriteLine(x.idCard()));

            Console.WriteLine();
            
            // List + LINQ

            peopleList = peopleList.OrderBy(x => x.Age).ToList();
            peopleList.ForEach(x => Console.WriteLine(x.idCard()));

            Console.WriteLine();
            // LINQ + Properties
            foreach (String id in peoples.Select(x => x.idCard()))
            {
                Console.WriteLine(id);
            };
            Console.WriteLine();
            double avg = peoples.Select(x => x.Age).Average();
            Console.WriteLine("Avg: {0}", avg.ToString());

            // Interface method
            Student std2 = new Student("Max", 26, 1.91, 101); // here Student has to be on the left side ohterwise you can not call getMatNo()
            uint maxMatNo = std2.getMatNo();
            Console.WriteLine(maxMatNo.ToString());

            
            
            
            // events
            Student std3 = new Student("Ferdi", 26, 1.91, 101); // here Student has to be on the left side ohterwise you can not call getMatNo()

            Student[] students = new Student[] { std2, std3 };

            foreach (var std in students)
            {
                if (std is IMatNo mem)
                {
                    std.OnChange += (uint mat) => Console.WriteLine("Matno changed");
                    std.MatNo = 0;
                }

            }

            //std2.MatNo = 0;
            
            
            
            /*
            std2.OnChange += (uint matno) => Console.WriteLine("Matno changed!"); // subscribe
            std2.MatNo = 0; //change then back to Console.Writline('MatNo changed!')
            Console.WriteLine(std2.getMatNo().ToString());


            double d;
            string str = "1,23";
            if (double.TryParse(str, out d))
                Console.WriteLine(d);
            //d= Convert.ToDouble(str);
            */
        }
    }
}