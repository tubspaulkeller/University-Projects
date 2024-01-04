namespace AsyncWait;

public class TaskExample
{
    public TaskExample()
    {
        Console.WriteLine("Let´s go!");

        Task<string> ta = RunningTaskSequentielly();
        for (int i = 0; i < 100; i++)
        {
            CreateAlphabet();
        }
        while (!ta.IsCompleted)
        { 
            Console.WriteLine(".");
            Thread.Sleep(100);
        }
        string hashedPassword = ta.Result;
        Console.WriteLine("Hashed Password: {0}", hashedPassword);
        Console.WriteLine("All Operations are done.");
            
    }
    
    private async Task<string> RunningTaskSequentielly()
    {
        // here all Task running seq. 
        string userpsw = "Test1234";
        string salt = await Encryption.genSalt();
        return await Encryption.hashpsw(userpsw, salt); 
    }
        
    public async Task<string> genSalt()
    {
        Console.WriteLine("Starting generating Salt");
        await Task.Delay(2000);
        string salt =  BCrypt.Net.BCrypt.GenerateSalt();
        Console.WriteLine("Salt generating is done.");
        return salt;
    }

    public async Task<string> hashpsw(string userpsw, string salt)
    {
        Console.WriteLine("Starting hashing");
        await Task.Delay(1000);
        string hashpsw = BCrypt.Net.BCrypt.HashPassword(userpsw,salt);
        Console.WriteLine("Hashing is done.");
        return hashpsw;
            
    }
        
    private void CreateAlphabet()
    {
        string alphabet = "";
        for (int i = 0; i < 1000; i++)
        {
            for (char charArr = (char)97; charArr < (char)123; charArr++)
            {
                alphabet += charArr;
            }
        }
        Console.WriteLine("created alphabet");
    }
}