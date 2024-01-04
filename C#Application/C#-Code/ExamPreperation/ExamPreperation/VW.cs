using System;
namespace Inheritance_Polymorphism_Static_Fields_Function_Class
{
    public class VW : Car
    {
        private int ps;
        public const string Brand = "VW";

        public string BrandII = "VWW";
        // in base class has to be a paramless ctor otherwise you can not have on in this class
        public VW()
        {

        }
        
      

        public VW(String name, String color, int ps) :base(name, color) //properties of base class have to be protected!
        {
            this.ps = ps;
        }
        
        public override string carConfig()
        {
            return name + " " + " " + color + " " + ps + " " + Brand;
        }

        public override string ToString()
        {
            return name + " " + " " + color + " " + ps + " " + Brand;
        }

        public override bool Equals(object? obj)
        {
            var vw = obj as VW;
            return name == vw.name && color == vw.color && ps == vw.ps;
        }
        

        public static VW generateVW()
        {
            return new VW("standardVW", "silver", 75);
        }
    }
}

