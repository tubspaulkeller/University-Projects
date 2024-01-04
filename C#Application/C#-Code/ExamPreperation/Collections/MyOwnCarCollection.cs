using System.Collections;

namespace Collections;

public class MyOwnCarCollection: IEnumerable<Car>
{
    private Car[] cars = new Car[] { new VW("Golf5", "White", 75), 
        new VW("Golf4", "Black", 100),
        new VW("GolfVI","Silver", 130)  
    };
    public IEnumerator<Car> GetEnumerator()
    {
        return new MyCarEnumerator(cars);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

