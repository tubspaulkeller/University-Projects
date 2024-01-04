using System.Collections;

namespace Collections;

public class MyOwnIntCollection: IEnumerable<int>
{
    private int[] arr = new int[] { 1, 3, 6 };
    public IEnumerator<int> GetEnumerator()
    {
        return new MyIntEnumerator(arr);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

