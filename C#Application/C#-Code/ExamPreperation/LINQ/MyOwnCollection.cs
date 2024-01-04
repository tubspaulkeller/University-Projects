using System.Collections;

namespace Funfair;

public class MyOwnCollection: IEnumerable<Kiwo>
{
    IEnumerator<Kiwo> IEnumerable<Kiwo>.GetEnumerator()
    {
        yield return new Kiwo("Bayernzelt", 15000, 40000, 10000);
        yield return new Kiwo("Kölsch", 5000, 20000, 7000);
        yield return new Kiwo("Bob", 10000, 30000, 9000); 

    }
/*
    public IEnumerator<MyRides> GetEnumerator()
    {
        yield return new MyRides("Rollercoaster", 40000, 500, 800, 7);
        yield return new MyRides("Ghosttrain", 10000, 300, 1200, 5);
        yield return new MyRides("Water-ride", 30000, 800, 300, 6.40);
        yield return new MyRides("Freefall-Tower", 20000, 100, 200, 10);
        yield return new MyRides("Children Carousel", 5000, 100, 300, 2.50);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return null;
    }
    */

public IEnumerator GetEnumerator()
{
    throw new NotImplementedException();
}
}