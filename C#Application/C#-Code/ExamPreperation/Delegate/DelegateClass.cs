namespace Delegate;

public class DelegateClass
{
    // delegate type
    public delegate void  DisplayDelegate(String display);

    public delegate void MathDelegate(int a, int b);
    
    
    
    // static methods
    public static void displayString(string name)
    {
        Console.WriteLine("My Name is: {0}", name);
    }
    public static void addAndDisplay(int x, int y) {
        Console.WriteLine("The sum of {0} and {1} is {2}", x, y, x + y);
       // return x + y;
    }
    // non static method
    public void multAndDisplay(int h, int i)
    {
        Console.WriteLine("The product of {0} and {1} is {2}", h, i, h*i);
        //return h * i;
    }

    public void CallDelegates(MathDelegate del)
    {
        del(3, 5);
    }
    
}