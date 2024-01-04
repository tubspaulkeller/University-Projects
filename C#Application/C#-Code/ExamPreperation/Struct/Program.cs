using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct

{
    class Program
    {
        static void Main(string[] args)
        {
            /* Vector3D vec = new Vector3D(10, 10, 10);

             Console.WriteLine(vec);
             vec.increase();
             Console.WriteLine(vec);
             Console.WriteLine(vec.norm());*/
            /*

             Vector3D[] someVectors = new Vector3D[3];
             someVectors[0] = new Vector3D(1, 1, 1);
             someVectors[1] = new Vector3D(1, 1, 0);
             someVectors[2] = new Vector3D(1, 3, 5);

             foreach (Vector3D item in someVectors)
             {
                 Console.WriteLine(item);
             }

             Console.WriteLine();

             Array.Sort(someVectors);

             foreach (Vector3D item in someVectors)
             {
                 Console.WriteLine(item);
             }

             */
            Vector3D a = new Vector3D(1, 1, 1);
            Vector3D b = new Vector3D(2, 0, 1);
            Vector3D c = a + b;
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.ReadKey();

            
            

        }
    }
}