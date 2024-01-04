namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //////////////////////////////////////////////
            //CustomCollection cc = new CustomCollection();

            MyOwnIntCollection moic = new MyOwnIntCollection();
            foreach (int number in moic)
            {
                Console.WriteLine(number);

            }
            
            
            
            
            MyOwnCarCollection mocc = new MyOwnCarCollection();
            foreach (var item in mocc)
            {
                Console.WriteLine(item.ToString());
            }

            var ite = mocc.GetEnumerator().Current; 
            
            
            
           
            
            
            
            ////////////////////////////////////////////
            
            Car vw = new Car("VW", "Black");
            Car mercedes = new Car("Mercedes", "White");


            CarCollection carCollection = new CarCollection();
            carCollection.CarInserted += (car) => Console.WriteLine("Car: {0} inserted.", car.Name);
            carCollection.Inserted += () => Console.WriteLine("Car is added.");
            carCollection.Add(vw);
            
            
            
            Console.WriteLine("YO");
            carCollection.Add(mercedes);


           // Remove Item
           carCollection.Removed += () => Console.WriteLine("Item removed.");
           carCollection.Remove(vw);
         
           foreach (var car in carCollection)
           {
               Console.WriteLine(car.ToString());
           }

           // Clear all Items 
           carCollection.AllCleared += () => Console.WriteLine("Items cleared.");
           carCollection.Clear();
         
           foreach (var car in carCollection)
           {
               if (car is Car c)
               {
                   Console.WriteLine(car.ToString());
               }
              
           }

            if (vw.isColorBlack())
            {
                Console.WriteLine("vw is Black");
            }



            // own Collection
            PrimeNumbers prim = new PrimeNumbers();

            foreach (var item in prim)
            {
                if (item is int i)
                    Console.WriteLine(i);
            }
            Console.ReadKey();

        
        }

        private static void Cars_CarInserted(Car c)
        {
            Console.WriteLine(c.Name + " is here!");
        }

    }


}

