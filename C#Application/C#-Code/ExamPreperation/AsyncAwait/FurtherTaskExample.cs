namespace AsyncWait;

public class FurtherTaskExample
{
    public FurtherTaskExample()
    {
        Console.WriteLine("Hey");

        Task ta = Calculator();

        while (!ta.IsCompleted)
        {
            return;
        }
        Console.WriteLine("finish!");
        
    }
    
    public async Task Calculator()
    {
        // Seq Run 

        int a = await SquareNum(3);

        int result = await GetResult(a);
        
        Console.WriteLine("result = {0}", result);

    }

    private async Task<int> SquareNum(int a)
    {
        await Task.Delay(1000);
        return a * a;
    }

    private async Task<int> GetResult(int a)
    {
        await Task.Delay(2000);
        return a + 2; 
    }
}