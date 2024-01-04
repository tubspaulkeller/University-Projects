namespace Delegate
{



    class Program
    {
        public delegate void sayname(string str);
        
        
        static void Main(string[] args)
        {
            // instance of the delegate types
            DelegateClass.DisplayDelegate displayDelegate; 
            DelegateClass.MathDelegate mathDelegate;
            
            sayname SayName;

            SayName = (string surname) => Console.WriteLine("My Surname is {0}", surname);

            SayName += (string lastname) => Console.WriteLine("My lastname is: {0}", lastname);


            SayName("Paul");
            SayName("Keller"); 
            
            
            // instance of the class
            DelegateClass delegateClassObject = new DelegateClass();
            
            // Void Delegate
            // call static string method
            displayDelegate = DelegateClass.displayString; 
            
            // Anonymous Function
            displayDelegate += delegate(string name)
            {
                Console.WriteLine("My name is: {0}", name);
            };
            // Statement Lambda
            displayDelegate += (String name) =>
            {
                Console.WriteLine("My name is: {0}", name);
            };
            // Expression Lambda
            displayDelegate -= (String name) => Console.WriteLine("My name is: {0}", name);
            
            // call it!
            displayDelegate("Paul");
            
            // int Delegate 
            // call static method
            mathDelegate = DelegateClass.addAndDisplay; 
            
            // call non static method 
            mathDelegate += delegateClassObject.multAndDisplay;
            
            // Anonymous Function 
            mathDelegate += delegate(int a, int b)
            {
                Console.WriteLine("The subtraction of {0} and {1} is {2}", a, b, a + b);
              //  return a - b;
            };
            
            // Statment Lambda
            mathDelegate -= (int a, int b) =>
            {
                Console.WriteLine("The sum of {0} and {1} is {2}", a, b, a + b);
             //   return a + b;
            };
            
            // Expression Lambda 
           mathDelegate += (int a, int b) => Console.WriteLine("The sum of {0} and {1} is {2}", a, b, a + b);;
            
            // Call it
            if(mathDelegate!= null) mathDelegate(1, 2);
            
            // Call my delegates
            if(mathDelegate!= null) delegateClassObject.CallDelegates(mathDelegate);

            


        }
    }
}