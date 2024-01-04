using System.Collections;

namespace Collections;

public class Enumerator: IEnumerator<Car>
{
    public int index;
    public Car[] cars;
    public Enumerator(Car[] cars)
    {
        this.cars = cars;
        index = -1; 

    }
    
    public bool MoveNext()
    {
        if(index < cars.Length-1)
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

    public object Current { get; }

    Car IEnumerator<Car>.Current {
        get
        {
            if (index == cars.Length || index < 0)
                throw new Exception("Out of bounds");
            return cars[index];
        }
    }
    
    public void Dispose()
    {}
}