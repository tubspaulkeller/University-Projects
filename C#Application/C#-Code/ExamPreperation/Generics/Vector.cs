using System;
namespace Generics
{
    public class Vector<T>
    {
        private T x;
        private T y;
        private T z;


        public T X {get; set;}
        public T Y { get; set; }
        public T Z { get; set; }
        
        
        public Vector()
        {
        }

        public Vector(T x, T y, T z){
            this.x = x;
            this.y = y;
            this.z = z;


         }

        public override string ToString()
        {
            return "X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString();
        }



    }
}

