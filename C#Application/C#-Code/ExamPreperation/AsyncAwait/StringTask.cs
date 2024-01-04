namespace AsyncWait;

public class StringTask
{
    public StringTask()
    {
        Console.WriteLine("Let´s GOO!");

        Task<string> task = GetString(); 
        Console.WriteLine("Waiting for the task to finish...");
        while(!task.IsCompleted)
        {
            Console.WriteLine("Still waiting...");
            Thread.Sleep(1000);
        }
      
        String result = task.Result;
        
        Console.WriteLine("Result: " + result); 
        Console.WriteLine("Done!");
        



    }
    
    private async Task<string> GetString()
    {
        Console.WriteLine("Get your String");

        string randomString = await CreateRandomString();
        
        Console.WriteLine("Got your string ");
        return randomString;

    }
    
    private async Task<string> CreateRandomString()
    {
        Console.WriteLine("Creating your string");
        await Task.Delay(1000);
        Console.WriteLine("String created");
        return "Hello World";
    }
}