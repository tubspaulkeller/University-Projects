// See https://aka.ms/new-console-template for more information
using BCrypt;
using BCrypt.Net;
namespace AsyncWait
{

    public class Program
    {
        private int num = 0;
        static void Main(string[] args)
        {
        /*
            Console.WriteLine("Let´s go!");

            Task<string> ta = RunningTaskSequentielly();
            CreateAlphabet();

            while (!ta.IsCompleted)
            {
                Console.WriteLine(".");
                Thread.Sleep(100);
            }
            string hashedPassword = ta.Result;
            Console.WriteLine("Hashed Password: {0}", hashedPassword);
            Console.WriteLine("All Operations are done.");
            */
        TaskExample taskExample = new TaskExample();

        //StringTask stringTask = new StringTask();
        //AsynEx asyncex = new AsynEx();
        

       //FurtherTaskExample ftk = new FurtherTaskExample();
                
            Console.ReadKey();
        }
        private static async Task<string> RunningTaskSequentielly()
        {
            Program p = new Program();
            // here all Task running seq. 
            string userpsw = "Test1234";
            string salt = await Encryption.genSalt();
            p.num = 2;
            return await Encryption.hashpsw(userpsw, salt); 
        }
        
        
        private static void CreateAlphabet()
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
}