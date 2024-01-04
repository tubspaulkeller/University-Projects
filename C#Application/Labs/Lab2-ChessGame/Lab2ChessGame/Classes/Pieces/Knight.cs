using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2ChessGame.Classes.Pieces
{
    // Springer (Pferd)
    //[Serializable]
    //[XmlInclude(typeof(Knight))]

    public class Knight : Piece
    {
        private ChessColor chessColor;
        public Knight(ChessColor chessColor, int playValue)
        {
            this.chessColor = chessColor;
            PlayValue = playValue;
        }
        public Knight()
        {

        }
        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {

            List<Coordinate> coordinateList = new List<Coordinate>();
            Coordinate[] coordinateArr = { };
            List<Coordinate> moves = new List<Coordinate>();
            //vorne vorne links
            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 2, "+"))
            {
                moves.Add(new Coordinate(currentPos.X + 1, currentPos.Y + 2));
            }
            //vorne vorne rechts
            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 2, "+"))
            {
                moves.Add(new Coordinate(currentPos.X - 1, currentPos.Y + 2));
            }
            //vorne rechts rechts
            if (currentPos.IsValid(currentPos.X, 2, "+") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                moves.Add(new Coordinate(currentPos.X + 2, currentPos.Y + 1));
            }
            //hinten rechts rechts
            if (currentPos.IsValid(currentPos.X, 2, "+") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                moves.Add(new Coordinate(currentPos.X + 2, currentPos.Y - 1));
            }

            if (currentPos.IsValid(currentPos.X, 2, "-") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                moves.Add(new Coordinate(currentPos.X - 2, currentPos.Y + 1));
            }

            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 2, "-"))
            {
                moves.Add(new Coordinate(currentPos.X + 1, currentPos.Y - 2));
            }

            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 2, "-"))
            {
                moves.Add(new Coordinate(currentPos.X - 1, currentPos.Y - 2));
            }

            if (currentPos.IsValid(currentPos.X, 2, "-") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                moves.Add(new Coordinate(currentPos.X - 2, currentPos.Y - 1));
            }


            foreach (var move in moves)
            {
                anyPieceHit(board, move, coordinateList);
            }
            coordinateArr = coordinateList.ToArray();
            return coordinateArr;

        }
        
    }
}


