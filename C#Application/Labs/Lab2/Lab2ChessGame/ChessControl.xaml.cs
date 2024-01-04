using Lab2ChessGame.Classes.Pieces;
using Lab2ChessGame.StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lab2ChessGame
{
   
    /// <summary>
    /// Interaktionslogik für ChessControl.xaml
    /// </summary>
    public partial class ChessControl : UserControl
    {
        private Button[,] buttons;
        private int boardLength = 8;
        private bool firstValidMove = true;
        Coordinate selectedField;     
        private Coordinate source;
        //private Dictionary<List<int>, Brush> originalColorDict = new Dictionary<List<int>, Brush>();

        Board board;
       
        public Board Board
        {
            set
            {
                board = value;
                board.statechanged += Board_statechanged;
                board.turnFlip += Board_turnflip;
                drawBackgorund();
                drawPieces();
            }
        }


        private Board drawPieces()
        {
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (board.fields[i, j] != null)
                    {
                        bool color = board.fields[i, j].Color == ChessColor.White;
                        switch (board.fields[i, j])
                        {
                            case Pawn p:
                                buttons[i, j].Content = color ? '♙' : '♟';
                                break;
                            case King k:
                                buttons[i, j].Content = color ? '♔' : '♚';
                                break;
                            case Castle c:
                                buttons[i, j].Content = color ? '♖' : '♜';
                                break;
                            case Bishop b:
                                buttons[i, j].Content = color ? '♗' : '♝';
                                break;
                            case Knight kn:
                                buttons[i, j].Content = color ? '♘' : '♞';
                                break;
                            case Queen q:
                                buttons[i, j].Content = color ? '♕' : '♛';
                                break;
                          
                            default:
                                buttons[i, j].Content = "";

                                break;
                        }
                    }
                    else
                    {
                        buttons[i, j].Content = "";
                    }
                }
            }

            

            return board;
        }
        
        public ChessControl()
        {
            InitializeComponent();
            // Get the fields of the grid as children
            UIElementCollection chlds = theGrid.Children;
            // intialize the buttons arr
            buttons = new Button[8, 8];
      
            // itereate through the field of the grids, which are buttons,
            // and put the reference to an two-dim-array
            // the Labels are ToggleButton otherwise it would be a array out of bounds
            try
            {
                foreach (UIElement item in chlds)
                {
                    if (item is Button btn)
                        buttons[Grid.GetColumn(btn) - 1, Grid.GetRow(btn) - 1] = btn;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

          //  Board = Board.testBoard();

            Board = Board.standardBoard();
            var sum = board.getSum();
          //  showSum.Content = "Sum: " + sum;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                
                if (firstValidMove )
                {
                    int x = Grid.GetColumn(btn as UIElement) - 1;
                    int y = Grid.GetRow(btn as UIElement) - 1;

                    source = new Coordinate(x, y);
                    System.Diagnostics.Debug.WriteLine(source);
                    System.Diagnostics.Debug.WriteLine(String.Format("x: {0} y: {1}", x, y));
                
                    //show possible moves
                    var piece = board.fields[x, y];

                    if (piece != null)
                    {
                        markFields(piece.PossibleMoves(board, source));
                        firstValidMove = false;
                    }
                }
                else
                {

                    int x = Grid.GetColumn(btn as UIElement) - 1;
                    int y = Grid.GetRow(btn as UIElement) - 1;
                    Coordinate destination = new Coordinate(x, y);
                    selectedField = destination;
              
                    if (board.PerformMove(source, destination))
                    {
                        
                        firstValidMove = true;
                      
                    }
                    else
                    {
                        drawBackgorund();
                        firstValidMove = true;
                    }
                }
            }

            //var sum = board.getSum();
            //showSum.Content = "Sum: " + sum;
        }
        private void Board_turnFlip(Board board)
        {
            if (board.onTurn == ChessColor.Black)
            {
                Negamax.calls = 0;
                
                Negamax.NegaMax(board, 3, 3);
                board = Negamax.saved;
                board.turnFlip += Board_turnFlip;
                ChessControl chessControl = new ChessControl();
                chessControl.Board = board;
            }
           System.Diagnostics.Debug.WriteLine(String.Format("NegaMaxCalls: {0} ", Negamax.calls));
        }

        private void Board_turnflip(Board board)
        {
             
            onTurnLabel.Content = board.player;
            //Board_turnFlip(board);


        }

        private void Board_statechanged(Board board)
        {
            Board = board;   
        }

        private void markFields(Coordinate[] marked)
        {
            for (int i = 0; i < marked.Length; i++)
            {
                if (marked[i] != null)
                {
                    var x = marked[i].X;
                    var y = marked[i].Y;
                    buttons[x, y].Background = new SolidColorBrush(Colors.Aqua);
                    buttons[x, y].UpdateLayout();
                    
                }
            }
        }

        private void drawBackgorund()
        {
            
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if((i+j) % 2 == 0)
                    {
                        buttons[i, j].Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        buttons[i, j].Background = new SolidColorBrush(Colors.ForestGreen);
                    }
                }
            }
           
        }
        
        private void SaveGameState(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton btn)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Board));
                XmlWriter writer = XmlWriter.Create("test.xml");
                serializer.Serialize(writer, board);
                writer.Close();

                  
                
            }

        }

        private void LoadGameState(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton btn)
            {
                
                XmlSerializer serializer = new XmlSerializer(typeof(Board));
                StreamReader reader = new StreamReader("test.xml");
                Board cloneBoard = (Board)serializer.Deserialize(reader);

                reader.Close();
                Board = cloneBoard;
            }
        }

       
    }
}
