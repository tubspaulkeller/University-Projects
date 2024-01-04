using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector<int> vectorInt = new Vector<int>(1, 1, 1);

            Vector<double> vectorDouble = new Vector<double>(1.5, 2.5, 3.4);

            Vector<Vector<int>> myVectorVectorInt = new Vector<Vector<int>>(
                new Vector<int>(1, 2, 1),
                new Vector<int>(1,1,1),
                new Vector<int>(3,2,1));


            Console.WriteLine(myVectorVectorInt.ToString());



            Squares mySquares = new Squares();

            // iterate through
            IEnumerator sqrs = mySquares.GetEnumerator();
            while (sqrs.MoveNext())
            {
                Console.WriteLine(sqrs.Current);
            }

           

            // same as above
            foreach (var item in mySquares)
            {
                Console.WriteLine(item);
            }

        


            Console.ReadKey();
        }
    }
}