namespace AsyncWait;

public class AsynEx
{
    public AsynEx()
    {
        Console.WriteLine("Woke up");
        //Coffee();
        //Toast();
        Task ta = Breakfast();
        Console.WriteLine("Read newspaper");

        while (!ta.IsCompleted)
        {
            Console.WriteLine("Wait for Breakfast..");
            Thread.Sleep(1000);
        }
        Console.ReadKey(); 
    }

    private async Task Breakfast()
    {
        await Coffee();
        await Toast();


    } 

    private async Task Coffee()
    {
        Console.WriteLine("Making coffee");
        await Task.Delay(2000);
        Console.WriteLine("Coffee is ready");
        
    }

    public async Task Toast()
    {
        Console.WriteLine("Start making toast");
        await Task.Delay(1000); 
        Console.WriteLine("Finished making toast");
    }
    // Parallel
    // Woke Up
    // Making Coffee 
    // Start making Toast
    // Read newspaper 
    // Finished making Toast 
    // Coffee is Ready 
    
    // Sequentiell 
    // Woke up 
    // Start making Coffee
    // Read newspaper 
    // Wait for Breakfast 
    // Coffee is Ready
    // Start making Toast
    // Waiting for Breakfast
    // Finished making toast




}