using System.Collections.Generic;
namespace Lab2ChessGame.Classes.Pieces
{
    // König
    public class King : Piece
    {
        private ChessColor chessColor;
        public King(ChessColor chessColor, int playValue)
        {
            this.chessColor = chessColor;
            PlayValue = playValue;
        }
        public King()
        {

        }
        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            List<Coordinate> coordinateList = new List<Coordinate>();
            List<Coordinate> moves = new List<Coordinate>();
            Coordinate[] coordinateArr = { };

            if(currentPos.IsValid(currentPos.X,1,"+") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                moves.Add(new Coordinate(currentPos.X + 1, currentPos.Y + 1));
            }
            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                moves.Add(new Coordinate(currentPos.X + 1, currentPos.Y - 1));
            }
            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                moves.Add(new Coordinate(currentPos.X - 1, currentPos.Y + 1));
            }
            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                moves.Add(new Coordinate(currentPos.X - 1, currentPos.Y - 1));
            }
            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 0, "+"))
            {
                moves.Add(new Coordinate(currentPos.X + 1, currentPos.Y + 0));
            }
            if (currentPos.IsValid(currentPos.X, 0, "+") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                moves.Add(new Coordinate(currentPos.X + 0, currentPos.Y - 1));
            }
            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 0, "+"))
            {
                moves.Add(new Coordinate(currentPos.X - 0, currentPos.Y + 0));
            }
            if (currentPos.IsValid(currentPos.X, 0, "+") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                moves.Add(new Coordinate(currentPos.X + 0, currentPos.Y + 1));
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
