using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shogi_v_1_0
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Field[,] Field = new Field[11, 11];
        Piece[] whitePieces = new Piece[20];
        Piece[] blackPieces = new Piece[20];
        Piece activePiece = null;
        Field activeField = null;

        public MainWindow()
        {
            InitializeComponent();

            DrawBoard();
            SetPieces();
        }

        private void DrawBoard()
        {
            int row;
            int column;

            for (row = 1; row <= 9; row++)
            {
                for (column = 1; column <= 9; column++)
                {
                    Field[row, column] = new Field(row, column, 2);
                    Field[row, column].MouseDown += Move;
                    TheGrid.Children.Add(Field[row, column]);

                    if (column == 9)
                    {
                        Field[row, column].BorderThickness = new Thickness(1, 1, 1, 0);
                    }
                    if (row == 9)
                    {
                        Field[row, column].BorderThickness = new Thickness(1, 1, 0, 1);
                    }
                    if (row == 9 && column == 9)
                    {
                        Field[row, column].BorderThickness = new Thickness(1, 1, 1, 1);
                    }
                }
            }

            for (column = 1; column <= 9; column++)
            {
                Field[0, column] = new Field(0, column);
                Field[10, column] = new Field(10, column);
            }
        }

        private void SetPieces()
        {
            //Piece test = new Tower(5, 5, 0);
            //test.MouseDown += Highlight;
            //TheGrid.Children.Add(test);
            //Field[5, 5].SetColor(0);

            //Piece test2 = new Knight(3, 7, 1);
            //test2.MouseDown += Highlight;
            //TheGrid.Children.Add(test2);
            //Field[3, 7].SetColor(1);
            
            //Create Tower and Bishop Pieces for black and white
            whitePieces[0] = new Tower(2, 3, 1);
            blackPieces[0] = new Tower(8, 7, 0);
            whitePieces[19] = new Bishop(2, 7, 1);
            blackPieces[19] = new Bishop(8, 3, 0);
            //tell Fields what Pieces are standing on them
            Field[2, 3].SetPiece(whitePieces[0]);
            Field[8, 7].SetPiece(blackPieces[0]);
            Field[2, 7].SetPiece(whitePieces[19]);
            Field[8, 3].SetPiece(blackPieces[19]);

            int column;
            //create remaining Pieces for black and white and tell respective Fields what's standing on them 
            for (column = 1; column <= 9; column++)
            {
                whitePieces[column] = new Pawn(3, column, 1);
                blackPieces[column] = new Pawn(7, column, 0);
                Field[3, column].SetPiece(whitePieces[column]);
                Field[7, column].SetPiece(blackPieces[column]);

                if (column == 1 || column == 9)
                {
                    whitePieces[column + 9] = new Lance(1, column, 1);
                    blackPieces[column + 9] = new Lance(9, column, 0);

                    Field[1, column].SetPiece(whitePieces[column + 9]);
                    Field[9, column].SetPiece(blackPieces[column + 9]);
                }
                if (column == 2 || column == 8)
                {
                    whitePieces[column + 9] = new Knight(1, column, 1);
                    blackPieces[column + 9] = new Knight(9, column, 0);

                    Field[1, column].SetPiece(whitePieces[column + 9]);
                    Field[9, column].SetPiece(blackPieces[column + 9]);
                }
                if (column == 3 || column == 7)
                {
                    whitePieces[column + 9] = new SilverGeneral(1, column, 1);
                    blackPieces[column + 9] = new SilverGeneral(9, column, 0);

                    Field[1, column].SetPiece(whitePieces[column + 9]);
                    Field[9, column].SetPiece(blackPieces[column + 9]);
                }
                if (column == 4 || column == 6)
                {
                    whitePieces[column + 9] = new GoldGeneral(1, column, 1);
                    blackPieces[column + 9] = new GoldGeneral(9, column, 0);

                    Field[1, column].SetPiece(whitePieces[column + 9]);
                    Field[9, column].SetPiece(blackPieces[column + 9]);
                }
                if (column == 5)
                {
                    whitePieces[column + 9] = new King(1, column, 1);
                    blackPieces[column + 9] = new King(9, column, 0);

                    Field[1, column].SetPiece(whitePieces[column + 9]);
                    Field[9, column].SetPiece(blackPieces[column + 9]);
                }

                //tell the fields which color they belong to
                Field[1, column].SetColor(1);
                Field[3, column].SetColor(1);
                Field[2, 3].SetColor(1);
                Field[2, 7].SetColor(1);
                Field[7, column].SetColor(0);
                Field[9, column].SetColor(0);
                Field[8, 3].SetColor(0);
                Field[8, 7].SetColor(0);
            }

            //Draw all Pieces onto the screen and add necessary methods
            for (column = 0; column < 20; column++)
            {
                whitePieces[column].MouseDown += Highlight;
                TheGrid.Children.Add(whitePieces[column]);

                blackPieces[column].MouseDown += Highlight;
                TheGrid.Children.Add(blackPieces[column]);
            }
        }

        public void Highlight(object sender, RoutedEventArgs e)
        {
            Piece selectedPiece = sender as Piece;
            int selectedPieceColor = selectedPiece.GetColor();
            
            if (activePiece == null) //check if there is a Piece active
            {
                activePiece = selectedPiece;
                activePiece.Background = Brushes.Red;
                if (activePiece.GetIsCaptured()) HintsCapturedPieceOn(); //if it's a previously captured Piece 
                else HintsOn(); //Show where the Piece may move and set respective Permissions
            }
            else
            {
                int activePieceColor = activePiece.GetColor();

                if(activePieceColor == selectedPieceColor)
                {
                    if (activePieceColor == 1) activePiece.Background = Brushes.White;
                    else activePiece.Background = Brushes.Black;

                    HintsOff(); //Remove all active hints and delete all active Permissions
                    activePiece = selectedPiece;
                    activePiece.Background = Brushes.Red;
                    if (activePiece.GetIsCaptured()) HintsCapturedPieceOn(); //if it's a previously captured Piece 
                    else HintsOn(); //Show where the Piece may move and set respective Permissions
                }
                else
                {
                    int selectedPieceRow = Grid.GetRow(selectedPiece);
                    int selectedPieceColumn = Grid.GetColumn(selectedPiece);
                    int activePieceRow = Grid.GetRow(activePiece);
                    int activePieceColumn = Grid.GetColumn(activePiece);

                    if (Field[selectedPieceRow, selectedPieceColumn].GetPermission())
                    {
                        int column;

                        for (column = 1; column < 10; column++)
                        {
                            if(activePieceColor == 1 && Field[0, column].GetPermission())
                            {
                                Field[0, column].SetPermission(false);
                                Grid.SetRow(selectedPiece, 0);
                                Grid.SetColumn(selectedPiece, column);
                                selectedPiece.SetColor(1);
                                selectedPiece.SetIsCaptured(true);
                                activePiece.Background = Brushes.White;
                                break;
                            }
                            if (activePieceColor == 0 && Field[10, column].GetPermission())
                            {
                                Field[10, column].SetPermission(false);
                                Grid.SetRow(selectedPiece, 10);
                                Grid.SetColumn(selectedPiece, column);
                                selectedPiece.SetColor(0);
                                selectedPiece.SetIsCaptured(true);
                                activePiece.Background = Brushes.Black;
                                break;
                            }
                        }
                        HintsOff();
                        Field[activePieceRow, activePieceColumn].SetColor(2);
                        Grid.SetRow(activePiece, selectedPieceRow);
                        Grid.SetColumn(activePiece, selectedPieceColumn);
                        Field[selectedPieceRow, selectedPieceColumn].SetColor(activePieceColor);
                        
                        activePiece = null;
                    }
                }
            }
        }

        public void Move(object sender, RoutedEventArgs e)
        {
            if (activePiece != null) //Do nothing if no Piece is active
            {
                activeField = sender as Field;

                int pieceRow = Grid.GetRow(activePiece);
                int pieceColumn = Grid.GetColumn(activePiece);
                int fieldRow = Grid.GetRow(activeField);
                int fieldColumn = Grid.GetColumn(activeField);
                int activePieceColor = activePiece.GetColor();

                if (activeField.GetPermission()) //check if Movement is allowed if yes move
                {
                    if(activePiece.GetIsCaptured()) 
                    {
                        activePiece.SetIsCaptured(false);
                        Field[pieceRow, pieceColumn].SetPermission(true);
                    }
                    else Field[pieceRow, pieceColumn].SetColor(2);
                    
                    if (activePieceColor == 1)
                    {
                        activePiece.Background = Brushes.White;
                        activeField.SetColor(1);
                    }
                    else
                    {
                        activePiece.Background = Brushes.Black;
                        activeField.SetColor(0);
                    }

                    HintsOff(); //delete all hints and permissions
                    Grid.SetRow(activePiece, fieldRow);
                    Grid.SetColumn(activePiece, fieldColumn);
                    activePiece = null;
                }
                else //if move not allowed
                {
                    HintsOff(); //delete all hints and permissions
                    if (activePieceColor == 1)
                    {
                        activePiece.Background = Brushes.White;
                    }
                    else
                    {
                        activePiece.Background = Brushes.Black;
                    }
                    activePiece = null;
                }
            }
        }

        public void HintsOn()
        {
            //Check all fields in a direction if movement of active Piece is generally allowed and which color the field has
            //if movement is not allowed or field has same color as active piece, check next direction
            //if the Field is empty(2), highlight it and give permission on field then check next field in same direction
            //if field has different color than active piece, set hint and give permission, then check next direction            

            int activePieceRow = Grid.GetRow(activePiece);
            int activePieceColumn = Grid.GetColumn(activePiece);
            int fieldRow = activePieceRow;
            int fieldColumn = activePieceColumn;

            for (fieldRow = activePieceRow + 1; fieldRow < 10; fieldRow++)
            {
                if (activePiece.IsMoveAllowed(fieldRow, activePieceColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, activePieceColumn].SetPermission(true);
                        Field[fieldRow, activePieceColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            for (fieldRow = activePieceRow - 1; fieldRow > 0; fieldRow--)
            {
                if (activePiece.IsMoveAllowed(fieldRow, activePieceColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, activePieceColumn].SetPermission(true);
                        Field[fieldRow, activePieceColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            fieldRow = activePieceRow;
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (activePiece.IsMoveAllowed(activePieceRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[activePieceRow, fieldColumn].SetPermission(true);
                        Field[activePieceRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (activePiece.IsMoveAllowed(activePieceRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[activePieceRow, fieldColumn].SetPermission(true);
                        Field[activePieceRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }
            
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (fieldRow < 9) fieldRow++;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            fieldRow = activePieceRow;
            for (fieldColumn = activePieceColumn + 1; fieldColumn < 10; fieldColumn++)
            {
                if (fieldRow > 1) fieldRow--;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            fieldRow = activePieceRow;
            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (fieldRow < 9) fieldRow++;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }

            fieldRow = activePieceRow;
            for (fieldColumn = activePieceColumn - 1; fieldColumn > 0; fieldColumn--)
            {
                if (fieldRow > 1) fieldRow--;
                else break;

                if (activePiece.IsMoveAllowed(fieldRow, fieldColumn))
                {
                    if (activePiece.GetColor() != Field[fieldRow, fieldColumn].GetColor())
                    {
                        Field[fieldRow, fieldColumn].SetPermission(true);
                        Field[fieldRow, fieldColumn].Background = Brushes.Tomato;
                        if (Field[fieldRow, fieldColumn].GetColor() != 2) break;
                    }
                    else break;
                }
                else break;
            }
        }

        public void HintsCapturedPieceOn()
        {
            int Row;
            int Column;

            for (Row = 1; Row < 10; Row++)
            {
                for (Column = 1; Column < 10; Column++)
                {
                    if (Field[Row, Column].GetColor() == 2)
                    {
                        Field[Row, Column].Background = Brushes.Tomato;
                        Field[Row, Column].SetPermission(true);
                    }
                }
            }
        }

        public void HintsOff()
        {
            int Row;
            int Column;

            for (Row = 1; Row < 10; Row++)
            {
                for (Column = 1; Column < 10; Column++)
                {
                    if (Field[Row, Column].GetPermission())
                    {
                        Field[Row, Column].Background = Brushes.LightGray;
                        Field[Row, Column].SetPermission(false);
                    }
                }
            }
        }
    }
}
