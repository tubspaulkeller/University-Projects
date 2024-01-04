namespace AsyncWait;

public class Encryption
{
    public static async Task<string> genSalt()
    {
        Console.WriteLine("Starting generating Salt");
        await Task.Delay(2000);
        string salt =  BCrypt.Net.BCrypt.GenerateSalt();
        Console.WriteLine("Salt generating is done.");
        return salt;
    }

    public static async Task<string> hashpsw(string userpsw, string salt)
    {
        Console.WriteLine("Starting hashing");
        await Task.Delay(1000);
        string hashpsw = BCrypt.Net.BCrypt.HashPassword(userpsw,salt);
        Console.WriteLine("Hashing is done.");
        return hashpsw;
            
    }
}