using System;
using System.Collections;

namespace Generics
{
    public class GenericEnumerator : IEnumerator<int>
    {
        int index = -1;
        public object Current => index * index;

        int IEnumerator<int>.Current => index * index;

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            index++;

            return (index < 10);
        }

        public void Reset()
        {
            index = -1;
        }
    }
}

