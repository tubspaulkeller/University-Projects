using System.Collections;
namespace Collections;
public class CustomCollection:IEnumerable<Car>
{
    private int[] arr = new int[] { 1, 3, 6 };
    private Car[] cars = new Car[] { new VW("Golf5", "White", 75), 
                                    new VW("Golf4", "Black", 100),
                                    new VW("GolfVI","Silver", 130)  
                                    };

    IEnumerator<Car> IEnumerable<Car>.GetEnumerator()
    {
        return new Enumerator(cars);
    }

    
    
    // ICollection
    /*
    public IEnumerator GetEnumerator()
     {
         foreach (int i in arr)
         {
             yield return i;
         }
        return new Enumerator(arr); 
    }
  
    private int counter;
    public CustomCollection()
    {
        counter = 3; 
    }
    public void CopyTo(Array array, int index)
    {
        foreach (int i in arr)
        {
            array.SetValue(i, index);
            index += 1; 
        }
    }
    public int Count
    {
        get => arr[counter];
    }
    public bool IsSynchronized { get => false; }
    public object SyncRoot { get => this; }
    */
    public IEnumerator GetEnumerator()
    {
        return new Enumerator(cars);
    }
}