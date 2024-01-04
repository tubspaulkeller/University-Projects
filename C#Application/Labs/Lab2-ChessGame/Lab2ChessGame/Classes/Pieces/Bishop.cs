using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ChessGame.Classes.Pieces
{
    // Läufer
    [Serializable]
    public class Bishop : Piece 
    {
        private ChessColor chessColor;
        public Bishop(ChessColor chessColor, int playValue)
        {
            this.chessColor = chessColor;
            PlayValue = playValue;
        }

        public Bishop(){}
        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> bishopCoordinateList = new List<Coordinate>();

            // rechts oben diagonal
            for (int i = 1; i < 8; i++)
            {
                if (IsValidPos(currentPos.X + i) && IsValidPos(currentPos.Y - i))
                {
                    Coordinate move = new Coordinate(currentPos.X + i, currentPos.Y - i);
                    if (this.anyPieceHit(board, move, bishopCoordinateList))
                    {
                        break;
                    }
                };
            }
            // links unten diagonal
            for (int i = 1; i < 8; i++)
            {

                if (this.IsValidPos(currentPos.X - i) && this.IsValidPos(currentPos.Y + i))
                {
                    Coordinate move = new Coordinate(currentPos.X - i, currentPos.Y + i);
                    if (this.anyPieceHit(board, move, bishopCoordinateList))
                    {
                        break;
                    }
                };
            }
            // recht unten diagonal
            for (int i = 1; i < 8; i++)
            {

                if (this.IsValidPos(currentPos.X + i) && this.IsValidPos(currentPos.Y + i))
                {
                    Coordinate move = new Coordinate(currentPos.X + i, currentPos.Y + i);
                    if (this.anyPieceHit(board, move, bishopCoordinateList))
                    {
                        break;
                    }
                };
            }
            //  oben links diagonal
            for (int i = 1; i < 8; i++)
            {

                if (this.IsValidPos(currentPos.X - i) && this.IsValidPos(currentPos.Y - i))
                {
                    Coordinate move = new Coordinate(currentPos.X - i, currentPos.Y - i);
                    if (this.anyPieceHit(board, move, bishopCoordinateList))
                    {
                        break;
                    }
                };
            }
          
            Coordinate[] coordinateArr = bishopCoordinateList.ToArray();
           return coordinateArr;
         
        }
    }
}

