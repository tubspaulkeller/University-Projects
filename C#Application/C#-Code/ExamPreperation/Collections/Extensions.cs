using System;
namespace Collections
{
    public static class Extensions
    {
        public static bool isColorBlack(this Car c)
        {
            return c.Color == "Black" ? true : false;
        }

    }
}

