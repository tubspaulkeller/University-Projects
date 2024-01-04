using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct

{
    struct Vector3D : IComparable
    {
        public double x;
        public double y;
        public double z;

        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        

        public override string ToString()
        {
            return "x: " + x.ToString() + " y: " + y.ToString() + " z: " + z.ToString();
        }

        public void increase()
        {
            x++;
            y++;
            z++;
        }
        public double norm()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public int CompareTo(object obj)
        {
            if (obj is Vector3D v)
            {
                if (this.norm() < v.norm())
                    return -1;
                else
                    return +1;
            }
            else
                return 0;

        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.x + right.x, left.y + right.y, left.z + right.z);
        }
    }
}
