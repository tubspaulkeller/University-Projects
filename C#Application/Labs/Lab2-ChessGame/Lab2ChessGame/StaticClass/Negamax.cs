using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ChessGame.StaticClass
{
    public static class Negamax
    {
        public static int calls = 0;
        public static Board saved;
        public static double NegaMax(Board current, int depth, int maxdepth)
        {
            calls++;
            if (depth == 0) return current.getSum();
            double max = double.NegativeInfinity;
            double currentValue;
            foreach (Board brd in current.PossibleBoards())
            {
                currentValue = -NegaMax(brd, depth - 1, maxdepth);
                if (currentValue > max)
                {
                    max = currentValue;
                    if (depth == maxdepth)
                    {
                        saved = brd;
                    }
                }
            }
            return max;
        }
    }
}
