/*
using Lab2ChessGame.Classes.Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2ChessGame
{
    //Spielfigur
    
    [XmlInclude(typeof(Castle))]
    [XmlInclude(typeof(King))]
    [XmlInclude(typeof(Bishop))]
    [XmlInclude(typeof(Knight))]
    [XmlInclude(typeof(Pawn))]
    [XmlInclude(typeof(Queen))]
    
   
  
    
    //[XmlInclude(typeof(Board))]
    [Serializable]
    public abstract class Piece 
    {
        public ChessColor Color;
        public bool HasBeenMoved = false;
        protected int playValue;
        public int PlayValue { get => playValue; set => playValue = value; }
        public abstract Coordinate[] PossibleMoves(Board board, Coordinate currentPos);

        public Piece()
        {

        }

        //ToDo implement bcs this method is not abstract

        protected bool anyPieceHit(Board board, Coordinate checkpos, List<Coordinate> moves)
        {
            var pawn = board.fields[checkpos.X, checkpos.Y];
            var inValid = false;

            if (pawn != null)
            {
                inValid = true;

                if (pawn.Color != Color)
                {
                    moves.Add(checkpos);
                }

            }
            else
            {
                moves.Add(checkpos);
                inValid = false;

            }
            return inValid;
        }
    }
}
*/
using Lab2ChessGame.Classes.Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2ChessGame
{
    //Spielfigur
   public abstract class Piece
    {
        public ChessColor Color;
        public bool HasBeenMoved = false;
        protected int playValue;
        public int PlayValue { get => playValue; set => playValue = value; }
        public abstract Coordinate[] PossibleMoves(Board board, Coordinate currentPos);

        public Piece(){}
        //ToDo implement bcs this method is not abstract
        protected bool anyPieceHit(Board board, Coordinate checkpos, List<Coordinate> moves)
        {
            var pawn = board.fields[checkpos.X, checkpos.Y];
            var inValid = false;
            if (pawn != null)
            {
                inValid = true;

                if (pawn.Color != Color)
                {
                    moves.Add(checkpos);
                }

            }
            else
            {
                moves.Add(checkpos);
                inValid = false;

            }
            return inValid;
        }

        protected bool IsValidPos(int newPosition)
        {
            return true ? (newPosition >= 0 && newPosition < 8) : false;
        }
        
    }
}
