using System.Collections;

namespace Collections;
public class MyCarEnumerator: IEnumerator<Car>
{
    private Car[] cars;
    private int index; 
    public MyCarEnumerator(Car [] cars)
    {
        this.cars = cars;
        this.index = -1;
    }
    public bool MoveNext()
    {
        if (index < cars.Length - 1)
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

    public Object Current
    { get; }

    Car IEnumerator<Car>.Current
    {
        get
        {
            if (index < 0 || index == cars.Length)
            {
                throw new Exception("Out of bounds");
            }

            return cars[index];
        }
    }

    public void Dispose()
    { }
}