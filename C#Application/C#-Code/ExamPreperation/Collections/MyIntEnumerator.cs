using System.Collections;

namespace Collections;

public class MyIntEnumerator: IEnumerator<int>
{
    private int[] arr;
    private int index;

    public MyIntEnumerator(int[] array)
    {
        arr = array;
        index = -1; 

    }
    public bool MoveNext()
    {
        if (index < arr.Length - 1)
        {
            index++;
            return true;
        }
        return false;
    }

    public void Reset()
    {
        index = -1;
    }

    public int Current {      
        get {
        if (index == arr.Length || index < 0)
        {
            throw new Exception("Out of bounds!");
        }
        return arr[index];
        } 
    }

    object IEnumerator.Current { get; }

    public void Dispose()
    { }
}