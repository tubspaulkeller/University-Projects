using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2ChessGame.Classes.Pieces
{
    // Bauer

    //[Serializable]
    //[XmlInclude(typeof(Pawn))]
    public class Pawn : Piece
    {
        private ChessColor chessColor;
        public Pawn(ChessColor chessColor, int playValue)
        {
            this.chessColor = chessColor;
            PlayValue = playValue;
        }

        public Pawn()
        {

        }
        public override Coordinate[] PossibleMoves(Board board, Coordinate currentPos)
        {
            //switchcase
            List<Coordinate> coordinateList = new List<Coordinate>();
            Coordinate[] coordinateArr = { };
            if (board.fields[currentPos.X, currentPos.Y].Color == ChessColor.White)
            {
                if (currentPos.Z == 2)
                {
                    //first move
                    for (int i = 1; i < 3; i++)
                    {
                        Coordinate newCordiante = new Coordinate(currentPos.X - 0, currentPos.Y - i);
                        if (anyHit(board, newCordiante)) { break; }
                        else { coordinateList.Add(newCordiante); }
                    }
                }
                else
                {
                 
                    if (currentPos.IsValid(currentPos.X, 0, "+") && currentPos.IsValid(currentPos.Y, 1, "-"))
                    {
                        Coordinate newCordiante = new Coordinate(currentPos.X + 0, currentPos.Y - 1);
                        if (anyHit(board, newCordiante)) { }
                        else { coordinateList.Add(newCordiante); }
                    }

                }
                // use Method for attack!
                // check if valid 
                whitehitEnemyPiece(board, currentPos, coordinateList);
            }

            if (board.fields[currentPos.X, currentPos.Y].Color == ChessColor.Black)
            {
                if (currentPos.Z == 7)
                {
                    //first move
                    for (int i = 1; i < 3; i++)
                    {
                        Coordinate newCordiante = new Coordinate(currentPos.X + 0, currentPos.Y + i);
                        if (anyHit(board, newCordiante)) { break; }
                        else { coordinateList.Add(newCordiante); }
                    }

                }
                else
                {
                    if (currentPos.IsValid(currentPos.X, 0, "+") && currentPos.IsValid(currentPos.Y, 1, "+"))
                    {
                        Coordinate newCordiante = new Coordinate(currentPos.X + 0, currentPos.Y + 1);
                        if (anyHit(board, newCordiante)) { }
                        else { coordinateList.Add(newCordiante); }
                    }

                }

                blackhitEnemyPiece(board, currentPos, coordinateList);
            }

            
            coordinateArr = coordinateList.ToArray();
            return coordinateArr;
        }

        private void blackhitEnemyPiece(Board board, Coordinate currentPos, List<Coordinate> coordinateList)
        {

            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                Coordinate newCordinate = new Coordinate(currentPos.X - 1, currentPos.Y + 1);
                if (checkEnemyPiece(board, newCordinate)) coordinateList.Add(newCordinate);
            }
            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 1, "+"))
            {
                Coordinate newCordinate = new Coordinate(currentPos.X + 1, currentPos.Y + 1);
                if (checkEnemyPiece(board, newCordinate)) coordinateList.Add(new Coordinate(currentPos.X + 1, currentPos.Y + 1));
            }
        }

        private void whitehitEnemyPiece(Board board, Coordinate currentPos, List<Coordinate> coordinateList)
        {
            if (currentPos.IsValid(currentPos.X, 1, "-") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                Coordinate newCordinate = new Coordinate(currentPos.X - 1, currentPos.Y - 1);
                if (checkEnemyPiece(board, newCordinate))coordinateList.Add(newCordinate);
            }
            if (currentPos.IsValid(currentPos.X, 1, "+") && currentPos.IsValid(currentPos.Y, 1, "-"))
            {
                Coordinate newCordinate = new Coordinate(currentPos.X + 1, currentPos.Y - 1);
                if (checkEnemyPiece(board, newCordinate)) coordinateList.Add(newCordinate); 
            }
        }

        private bool checkEnemyPiece(Board board, Coordinate checkpos)
        {
            bool valid = false;
           
            var pawn = board.fields[checkpos.X, checkpos.Y];
          
            // check if it is an enemy piece
            if (pawn != null && pawn.Color != Color)
            {
                valid = true;
            }             
            return valid;

        }

        private bool anyHit(Board board, Coordinate checkpos)
        {
            var pawn = board.fields[checkpos.X, checkpos.Y];
            var inValid = false;

            if (pawn != null)
            {
                inValid = true;

            }
            return inValid;
        }
    }

}
