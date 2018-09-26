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
using System.Security;
using System.Security.Permissions;


namespace Shogi_v_1_0
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //set initial Arrays and default values
        Field[,] Field = new Field[11, 11];
        Piece[] whitePieces = new Piece[20];
        Piece[] blackPieces = new Piece[20];
        Piece activePiece = null;
        Field activeField = null;
        Menu mainMenu = new Menu();
        int activePlayer = 1;

        public MainWindow()
        {
            InitializeComponent();
            FileIOPermission f = new FileIOPermission(PermissionState.None);
            f.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                f.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }
            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, "C:\\Users\\saves");
            f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, "C:\\Users\\saves");

            DrawBoard();
            GeneratePieces();
            DrawPieces();
            mainMenu.SetPiecePositions(whitePieces, blackPieces);
            mainMenu.InitFieldColors(Field);
        }

        //draws board visuals with neutral fieldcolors
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

        public void GeneratePieces()
        {
            int index;

            for(index=0; index < 9; index++)
            {
                whitePieces[index] = new Pawn(0, 0, 1);
                blackPieces[index] = new Pawn(10, 10, 0);
            }
            whitePieces[9] = new Tower(0, 0, 1);
            blackPieces[9] = new Tower(10, 10, 0);
            whitePieces[10] = new Bishop(0, 0, 1);
            blackPieces[10] = new Bishop(10, 10, 0);
            whitePieces[11] = new Lance(0, 0, 1);
            blackPieces[11] = new Lance(10, 10, 0);
            whitePieces[12] = new Lance(0, 0, 1);
            blackPieces[12] = new Lance(10, 10, 0);
            whitePieces[13] = new Knight(0, 0, 1);
            blackPieces[13] = new Knight(10, 10, 0);
            whitePieces[14] = new Knight(0, 0, 1);
            blackPieces[14] = new Knight(10, 10, 0);
            whitePieces[15] = new SilverGeneral(0, 0, 1);
            blackPieces[15] = new SilverGeneral(10, 10, 0);
            whitePieces[16] = new SilverGeneral(0, 0, 1);
            blackPieces[16] = new SilverGeneral(10, 10, 0);
            whitePieces[17] = new GoldGeneral(0, 0, 1);
            blackPieces[17] = new GoldGeneral(10, 10, 0);
            whitePieces[18] = new GoldGeneral(0, 0, 1);
            blackPieces[18] = new GoldGeneral(10, 10, 0);
            whitePieces[19] = new King(0, 0, 1);
            blackPieces[19] = new King(10, 10, 0);
            whitePieces[19].SetIsKing(true);
            blackPieces[19].SetIsKing(true);
        }

        //sets initial pieces and colors
        public void DrawPieces()
        {
            int column;
            //Draw all Pieces onto the screen and add necessary methods
            for (column = 0; column < 20; column++)
            {
                whitePieces[column].MouseDown += Highlight;
                TheGrid.Children.Add(whitePieces[column]);

                blackPieces[column].MouseDown += Highlight;
                TheGrid.Children.Add(blackPieces[column]);
            }
        }

        //shows possible movements and moves pieces
        public void Highlight(object sender, RoutedEventArgs e)
        {
            Piece selectedPiece = sender as Piece;
            int selectedPieceColor = selectedPiece.GetColor();
            
            if (activePiece == null  ) //first selection
            {
                if (selectedPieceColor == activePlayer)
                {
                    activePiece = selectedPiece;
                    activePiece.Background = Brushes.Red;
                    if (activePiece.GetIsCaptured()) HintsCapturedPieceOn(); //if it's a previously captured Piece 
                    else HintsOn(); //Show where the Piece may move and set respective Permissions
                }
            }
            else //with active piece
            {
                int activePieceColor = activePiece.GetColor();

                if(activePieceColor == selectedPieceColor) //switch pieces if seletion is of same color
                {
                    if (activePieceColor == 1) activePiece.Background = Brushes.White;
                    else activePiece.Background = Brushes.Black;

                    HintsOff(); //Remove all active hints and delete all active Permissions
                    activePiece = selectedPiece;
                    activePiece.Background = Brushes.Red;
                    if (activePiece.GetIsCaptured()) HintsCapturedPieceOn(); //if it's a previously captured Piece 
                    else HintsOn(); //Show where the Piece may move and set respective Permissions
                }
                else //move and/or capture pieces
                {
                    int selectedPieceRow = Grid.GetRow(selectedPiece);
                    int selectedPieceColumn = Grid.GetColumn(selectedPiece);
                    int activePieceRow = Grid.GetRow(activePiece);
                    int activePieceColumn = Grid.GetColumn(activePiece);

                    if (Field[selectedPieceRow, selectedPieceColumn].GetPermission())
                    {
                        int column;
                        int gameOver = 0;

                        for (column = 1; column < 10; column++)
                        {
                            if(activePieceColor == 1 && Field[0, column].GetPermission()) //find free spot outside of board for white pieces
                           {
                                Field[0, column].SetPermission(false);
                                Grid.SetRow(selectedPiece, 0);
                                Grid.SetColumn(selectedPiece, column);
                                selectedPiece.SetColor(1);
                                selectedPiece.SetIsCaptured(true);
                                if (mainMenu.WinCondition(selectedPiece.GetIsKing(), 1) == 1)
                                {
                                    mainMenu.SetPiecePositions(whitePieces, blackPieces);
                                    mainMenu.ResetPieceStates(whitePieces, blackPieces);
                                    mainMenu.ResetFieldColors(Field);
                                    mainMenu.InitFieldColors(Field);
                                    gameOver = 1;
                                }
                                activePiece.Background = Brushes.White;
                                break;
                            }
                            if (activePieceColor == 0 && Field[10, column].GetPermission()) // find free spot outside of board for black pieces
                            {
                                Field[10, column].SetPermission(false);
                                Grid.SetRow(selectedPiece, 10);
                                Grid.SetColumn(selectedPiece, column);
                                selectedPiece.SetColor(0);
                                selectedPiece.SetIsCaptured(true);
                                if (mainMenu.WinCondition(selectedPiece.GetIsKing(), 0) == 1)
                                {
                                    mainMenu.SetPiecePositions(whitePieces, blackPieces);
                                    mainMenu.ResetPieceStates(whitePieces, blackPieces);
                                    mainMenu.ResetFieldColors(Field);
                                    mainMenu.InitFieldColors(Field);
                                    gameOver = 1;
                                }
                                activePiece.Background = Brushes.Black;
                                break;
                            }
                        }
                        HintsOff();
                        if(gameOver == 0) //only if King is still in Play
                        {
                        Field[activePieceRow, activePieceColumn].SetColor(2);
                        Grid.SetRow(activePiece, selectedPieceRow);
                        Grid.SetColumn(activePiece, selectedPieceColumn);
                        Field[selectedPieceRow, selectedPieceColumn].SetColor(activePieceColor);
                        }

                        activePiece = null;
                        activePlayer = activePlayer == 1 ? 0 : 1;
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
                    if(activePiece.GetIsCaptured()) //for pieces outside the board
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
                    activePlayer = activePlayer == 1 ? 0 : 1;
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
        
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.SetPiecePositions(whitePieces, blackPieces);
            mainMenu.ResetPieceStates(whitePieces, blackPieces);
            mainMenu.ResetFieldColors(Field);
            mainMenu.InitFieldColors(Field);
            activePlayer = 1;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            string dir = "C:\\Users\\saves\\save.txt";
            int row;
            int column;
            int index;
            string save = "[FieldStatus]" + Environment.NewLine + Environment.NewLine;

            for(row=0; row<11; row++)
            {
                for(column=1; column<10; column++)
                {
                    int color = Field[row, column].GetColor();
                    bool permission = Field[row, column].GetPermission();
                    string FieldName = "Field[" + row.ToString() +","+ column.ToString() + "]" + Environment.NewLine;
                    save += FieldName + "[Color]=" + color.ToString() + " [Permission]=" + permission.ToString() + Environment.NewLine;
                }
            }
            System.IO.File.WriteAllText(dir, save);

            save = Environment.NewLine + "[whitePieces]" + Environment.NewLine;

            for(index=0; index<20; index++)
            {
                row = Grid.GetRow(whitePieces[index]);
                column = Grid.GetColumn(whitePieces[index]);
                string PieceName = "whitePiece[" + index.ToString() + "]" + Environment.NewLine;
                save += PieceName + "[Row]=" + row.ToString() + " [Column]=" + column.ToString() + Environment.NewLine;
            }
  
            System.IO.File.AppendAllText(dir, save);
            save = Environment.NewLine + "[blackPieces]" + Environment.NewLine;

            for (index = 0; index < 20; index++)
            {
                row = Grid.GetRow(blackPieces[index]);
                column = Grid.GetColumn(blackPieces[index]);
                string PieceName = "blackPiece[" + index.ToString() + "]" + Environment.NewLine;
                save += PieceName + "[Row]=" + row.ToString() + " [Column]=" + column.ToString() + Environment.NewLine;
            }
            System.IO.File.AppendAllText(dir, save);
        }
    }
}
