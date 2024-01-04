using System.Collections.Generic;

namespace Lab2ChessGame.Classes.Pieces
{
    public class Queen : Piece
    {
        private ChessColor chessColor;
        public Queen(ChessColor chessColor, int playVaue)
        {
            chessColor = chessColor;
            PlayValue = playVaue;
        }
        public Queen()
        {

        }
        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            
            List<Coordinate> coordinateList = new List<Coordinate>();
            Coordinate[] coordinateArr = { };
            // rechts oben diagonal
            for (int i = 1; i < 8; i++)
            {            
                    if(IsValidPos(currentPos.X + i) && IsValidPos(currentPos.Y - i))
                    {
                        Coordinate move = new Coordinate(currentPos.X + i, currentPos.Y - i);
                        if (anyPieceHit(board, move, coordinateList))
                        {
                            break;
                        }
                    };
            }
            // links unten diagonal
            for (int i = 1; i < 8; i++)
            {

                if (IsValidPos(currentPos.X - i) && IsValidPos(currentPos.Y + i))
                {
                    Coordinate move = new Coordinate(currentPos.X - i, currentPos.Y + i);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };
            }
            // recht unten diagonal
            for (int i = 1; i < 8; i++)
            {

                if (IsValidPos(currentPos.X + i) && IsValidPos(currentPos.Y + i))
                {
                    Coordinate move = new Coordinate(currentPos.X + i, currentPos.Y + i);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };
            }
            //  oben links diagonal
            for (int i = 1; i < 8; i++)
            {

                if (IsValidPos(currentPos.X - i) && IsValidPos(currentPos.Y - i))
                {
                    Coordinate move = new Coordinate(currentPos.X - i, currentPos.Y - i);
                    if (anyPieceHit(board, move, coordinateList))
                    {
                        break;
                    }
                };
            }

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
