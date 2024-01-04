namespace Bitshift;

public class Program
{
    static void Main(string[] args)
    {
        int i = 4;
        //Console.WriteLine("0x{0:x}", i << 1);

        byte a = 0x01;
        byte state = 5;

        if ((state & 0x01) != 0)
        {
            
        }
        else
        {
            a = 40;
        }
            
            

       // a |= 0x04; 
        
        
        
        Console.WriteLine(a.ToString());
        
    }
}