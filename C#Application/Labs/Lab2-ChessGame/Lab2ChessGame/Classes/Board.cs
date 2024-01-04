using Lab2ChessGame.Classes.Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lab2ChessGame
{
    // Chess board
    public delegate void ChessBoardDelegate(Board board);
   
    [Serializable]
    public class Board : IEnumerable<Piece>,IXmlSerializable
    {
        [XmlArray]
        public Piece[,] fields = new Piece[8, 8];//2dim Array for Chess Pieces

        public string player = "";
        public ChessColor onTurn = ChessColor.White;// White starts
        public event ChessBoardDelegate statechanged;
        public event ChessBoardDelegate turnFlip;
        private Board board;

        public Board() { }

        
        
        public Board(Board other)
        {
            this.onTurn = other.onTurn;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (other.fields[i, j] != null)
                    {
                        fields[i, j] = newPiece(other.fields[i, j]);
                        fields[i, j].HasBeenMoved = other.fields[i, j].HasBeenMoved;
                    }
                }
            }
        }
        public static Piece newPiece(Piece other)
        {
            if (other is Bishop) return new Bishop(other.Color, 3);
            if (other is Castle) return new Castle(other.Color, 5);
            if (other is King) return new King(other.Color, 0);
            if (other is Knight) return new Knight(other.Color, 3);
            if (other is Pawn) return new Pawn(other.Color, 1);
            if (other is Queen) return new Queen(other.Color, 9);
            return null;
        }

        public static Board standardBoard()
        {
            // you need to create an instance of the class bcs of the static method
            Board board = new Board();

            // Create an instances of the pieces
            Piece[] blackPieces = {new Castle(ChessColor.Black, 5),new Knight(ChessColor.Black, 3),new Bishop(ChessColor.Black, 3),
                                new Queen(ChessColor.Black, 9), new King(ChessColor.Black, 0), new Bishop(ChessColor.Black, 3),
                                new Knight(ChessColor.Black, 3), new Castle(ChessColor.Black, 5)
            };

            Piece[] whitePieces = {new Castle(ChessColor.White, 5),new Knight(ChessColor.White, 3),new Bishop(ChessColor.White, 3),
                                new Queen(ChessColor.White, 9), new King(ChessColor.White, 0), new Bishop(ChessColor.White, 3),
                                new Knight(ChessColor.White, 3), new Castle(ChessColor.White, 5)
            };

            for (int i = 0; i < 8; i++)
            {
                board.fields[i, 0] = blackPieces[i];
                board.fields[i, 7] = whitePieces[i];
                board.fields[i, 7].Color = board.onTurn;


          
            }
            // Add Pawns
            for (int i = 0; i <= 7; i++)
            {
                board.fields[i, 1] = new Pawn(ChessColor.Black, 1);
                board.fields[i, 6] = new Pawn(ChessColor.White, 1);
                board.fields[i, 6].Color = board.onTurn;
            }

            return board;
        }

        public static Board testBoard()
        {
            // you need to create an instance of the class bcs of the static method
            Board board = new Board();

            // Create an instances of the pieces
            Piece[] blackPieces = {new Castle(ChessColor.Black, 5),new Knight(ChessColor.Black, 3),new Bishop(ChessColor.Black, 3),
                               null, new King(ChessColor.Black, 0), new Bishop(ChessColor.Black, 3),
                                new Knight(ChessColor.Black, 3), new Castle(ChessColor.Black, 5)
            };

            Piece[] whitePieces = {new Castle(ChessColor.White, 5),null,new Bishop(ChessColor.White, 3),
                                null, new King(ChessColor.White, 0), null,
                                new Knight(ChessColor.White, 3), new Castle(ChessColor.White, 5)
            };

            for (int i = 0; i < 8; i++)
            {
                board.fields[i, 0] = blackPieces[i];
                board.fields[i, 7] = whitePieces[i];
                if (board.fields[i, 7] != null)
                {
                    board.fields[i, 7].Color = board.onTurn;
                }



            }
            board.fields[0, 5] = new King(ChessColor.Black, 0);
            board.fields[5, 4] = new Knight(ChessColor.Black, 3);
            board.fields[2, 3] = new Queen(ChessColor.Black, 9);
            board.fields[5, 2] = new Pawn(ChessColor.White, 1);
            board.fields[5, 2].Color = board.onTurn;
            board.fields[2, 5] = new Pawn(ChessColor.Black, 1);
            board.fields[6, 3] = new Pawn(ChessColor.Black, 1);
            board.fields[3, 2] = new Pawn(ChessColor.White, 1);
            board.fields[3, 2].Color = board.onTurn;
            board.fields[3, 4] = new Bishop(ChessColor.White, 3);
            board.fields[3, 4].Color = board.onTurn;

            // Add Pawns

            board.fields[1, 3] = new Pawn(ChessColor.Black, 1);
                board.fields[2, 1] = new Pawn(ChessColor.Black, 1);
                board.fields[5, 1] = new Pawn(ChessColor.Black, 1);
                board.fields[6, 1] = new Pawn(ChessColor.Black, 1);
                board.fields[1, 6] = new Pawn(ChessColor.White, 1);
                board.fields[1, 6].Color = board.onTurn;
                board.fields[3, 6] = new Pawn(ChessColor.White, 1);
                board.fields[3, 6].Color = board.onTurn;
                board.fields[5, 6] = new Pawn(ChessColor.White, 1);
                board.fields[5, 6].Color = board.onTurn;
            return board;
        }


      
        public bool Move(Coordinate source, Coordinate destination)
        {
            bool legal = false;

            legal = fields[source.X, source.Y].PossibleMoves(this, new Coordinate(source.X, source.Y)).Contains(destination);
            return legal;
        }

        public bool PerformMove(Coordinate from, Coordinate to)
        {
            bool success = false;   
            if (Move(from, to))
            {
                var pawn = this.fields[from.X, from.Y];
                this.fields[from.X, from.Y] = null;
                this.fields[to.X, to.Y] = pawn;

                if (statechanged != null) statechanged(this);
                success  = true;    
            }
            
            if (onTurn == ChessColor.White)
            {
                onTurn = ChessColor.Black;
                player = "OnTurn: Black Pieces";
                if (turnFlip != null) turnFlip(this);
            }
            else
            {
                onTurn = ChessColor.White;
                player = "OnTurn: White Pieces";
                if (turnFlip != null) turnFlip(this);
            }
            
            return success;
        }

           public void Add(Piece value)
        {
            throw new NotSupportedException("Add got called in BoardClass.");
        }

        public IEnumerator<Piece> GetEnumerator()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (fields[i, j] != null)
                    {
                        var piece = fields[i, j];   
                        yield return piece;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
          
            throw new NotImplementedException();
        }

        public int getPlayValueSum()
        {
            int sum = 0; 

            if(onTurn == ChessColor.White)
            {
              sum = this.Where(p => p.Color == ChessColor.White).Sum(p => p.PlayValue);
            }
            else
            {
               sum = this.Where(p => p.Color == ChessColor.Black).Sum(p => p.PlayValue);
            }
           
            return sum;
        }

        private int getAgilitySum()
        {
            int agilitySum= 0;
            var color = (onTurn == ChessColor.White) ? ChessColor.White : ChessColor.Black;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var piece = fields[i, j];
                    if (piece != null && piece.Color == color)
                    {
                        //var pos = getPosition(piece);
                        var pos = new Coordinate(i, j); 
                       // agilitySum += piece.PossibleMoves(this, pos).Length;
                    }
                }
            }
                return agilitySum;
        }

       public double getSum()
        {
            var x = getAgilitySum() * .1 + getPlayValueSum();
            return x;
        }

        public Board[] PossibleBoards()
        {
            
            var turns = (onTurn == ChessColor.White) ? ChessColor.White : ChessColor.Black;
            Board[] possibleBoards;
            List<Board> boardList = new List<Board>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board board = new Board(this);
                    
                    var piece = board.fields[i, j];
                    if(piece != null && piece.Color == turns)
                    {
                        var pos = new Coordinate(i, j);
                       var arr = piece.PossibleMoves(board, pos);
                       foreach (var move in arr)
                        {

                            board.fields[move.X, move.Y] = piece;
                        }

                        boardList.Add(board);

                    }
                }

        }

        possibleBoards = boardList.ToArray();
            //return array of possible board with possbile moves
            return possibleBoards;   

        }

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string onTurnColor = reader.GetAttribute("color");
            onTurn = onTurnColor == "White" ? ChessColor.White : ChessColor.Black;
            reader.Read();//um zum nächsten zu kommen
            while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
            {

                int x = int.Parse(reader.GetAttribute("x"));
                int y = int.Parse(reader.GetAttribute("y"));
                string color = reader.GetAttribute("color");
                ChessColor enumColor = color == "White" ? ChessColor.White : ChessColor.Black;
                var piece = reader.GetAttribute("piece");

                switch (piece)
                {
                    case "Castle":
                        fields[x, y] = new Castle();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 5;
                        break;
                    case "Pawn":
                        fields[x, y] = new Pawn();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 1;
                        break;
                    case "Knight":
                        fields[x, y] = new Knight();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 3;
                        break;
                    case "Bishop":
                        fields[x, y] = new Bishop();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 3;
                        break;
                    case "Queen":
                        fields[x, y] = new Queen();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 9;
                        break;
                    case "King":
                        fields[x, y] = new King();
                        fields[x, y].Color = enumColor;
                        fields[x, y].PlayValue = 0;
                        break;
                     default:
                        fields[x, y] = null;
                        break;
                }
                
            }


        }
       
        public void WriteXml(XmlWriter writer)
        {

            writer.WriteAttributeString("onTurn", onTurn.ToString());
            writer.WriteStartElement("allFields");
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    var item = this.fields[i, j];
                    Coordinate cor = new Coordinate(i, j);

                    if (item != null)
                    {
                        writer.WriteStartElement(item.ToString().Substring(29));
                        writer.WriteAttributeString("piece",item.ToString().Substring(29));
                        writer.WriteAttributeString("color", item.Color.ToString());
                        writer.WriteAttributeString("z", cor.Z.ToString());
                        writer.WriteAttributeString("c", cor.C.ToString());
                        writer.WriteAttributeString("x", cor.X.ToString());
                        writer.WriteAttributeString("y", cor.Y.ToString());
                        writer.WriteEndElement();

                    }
                }
            }

            writer.WriteEndElement();
        }
        

    }

}

