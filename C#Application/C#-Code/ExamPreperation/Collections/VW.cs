namespace Collections;

using System;

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
        
     
        

        public static VW generateVW()
        {
            return new VW("standardVW", "silver", 75);
        }

        public override string ToString()
        {
            return Name + " " + Color + " " + ps;
        }
}

