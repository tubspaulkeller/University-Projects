using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Generics
{
    class Squares : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            return new GenericEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
