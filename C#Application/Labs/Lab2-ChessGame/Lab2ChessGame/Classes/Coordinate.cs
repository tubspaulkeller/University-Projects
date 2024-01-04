using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ChessGame
{
    //represents a position on the board.
    public class Coordinate
    {

        private int x;
        private int y;
        
        public int X
        {
            get { return x; }
            set {
                if (IsValid(value, 0, "+"))
                {
                    x = value;
                    //ASCII Encoding
                    C = Convert.ToChar(65 + x);
                }
               

            }
                
        }

        public int Y
        {
            get { return y; }
            set
            {
                if (IsValid(value, 0, "+"))
                {
                    y = value;

                    Z = 8 - y;

                }
               
            }

        }

        public char C { get; set; }
        public int Z { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;

        }

        // operator overload for + and - to calculate relative postions
        public bool IsValid(int position, int? nextStep, string? operation)
        {
            bool isValid = false;

            if (operation.Equals("+"))
            {
                isValid = true ? (position + nextStep >= 0 && position + nextStep < 8) : isValid = false;

            }
            else if (operation.Equals("-"))
            {
                isValid = true ? (position - nextStep >= 0 && position - nextStep < 8) : isValid = false;

            }
            return isValid;
        }


        public bool IsValidAdd(int position, int? nextStep)
        {
            bool isValid = false;
            isValid = true ? (position + nextStep >= 0 && position + nextStep < 8) : isValid = false;
            return isValid;
        }

        public bool IsValidSub(int position, int? nextStep)
        {
            bool isValid = false;
            isValid = true ? (position + nextStep >= 0 && position + nextStep < 8) : isValid = false;
            return isValid;
        }




        public override string ToString()
        {
            return "Coordinates: (" + C + ", " + Z + ")";

        }

        public override bool Equals(object obj)
        {
            var coordinate = obj as Coordinate;
            return coordinate != null &&
                   X == coordinate.x &&
                   Y == coordinate.y;
                

        }


    }
}
