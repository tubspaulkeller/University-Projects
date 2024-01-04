using System;
using System.Collections;

namespace Collections
{
    public class PrimeNumbers : IEnumerable
    {
        public PrimeNumbers()
        {
        }

        public IEnumerator GetEnumerator()
        {
            yield return 2;
            yield return "Hello";
            yield return 5;
            yield return 7;
        }
    }
}

