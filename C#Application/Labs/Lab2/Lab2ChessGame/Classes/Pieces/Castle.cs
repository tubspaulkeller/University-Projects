using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2ChessGame.Classes.Pieces
{
    // Turm
    public class Castle : Piece
    {
        private ChessColor chessColor;

        public Castle(ChessColor chessColor, int playValue)
        {
            this.chessColor = chessColor;
            PlayValue = playValue;
        }

        public Castle(){}

        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {


            List<Coordinate> coordinateList = new List<Coordinate>();
            Coordinate[] coordinateArr = { };
            for (int i = 1; i < 8; i++)
            {
                //movement to right
                if (IsValidPos(currentPos.X + i) && IsValidPos(currentPos.Y - 0))
                {
                    Coordinate move = new Coordinate(currentPos.X + i, currentPos.Y - 0);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };
            }
            // movements down
            for (int i = 1; i < 8; i++)
            {
                if (IsValidPos(currentPos.X + 0) && IsValidPos(currentPos.Y + i))
                {
                    Coordinate move = new Coordinate(currentPos.X + 0, currentPos.Y + i);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };
            }

            // movements to left
            for (int i = 1; i < 8; i++)
            {
                if (IsValidPos(currentPos.X - i) && IsValidPos(currentPos.Y - 0))
                {
                    Coordinate move = new Coordinate(currentPos.X - i, currentPos.Y - 0);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };

            }
            // movement down
            for (int i = 1; i < 8; i++)
            {
                if (IsValidPos(currentPos.X + 0) && IsValidPos(currentPos.Y - i))
                {
                    Coordinate move = new Coordinate(currentPos.X + 0, currentPos.Y - i);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };

            }
            coordinateArr = coordinateList.ToArray();
            return coordinateArr;

        }
    }
}
