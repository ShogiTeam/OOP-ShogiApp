using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


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
        PieceCounter[] blackPieceCounter = new PieceCounter[7];
        PieceCounter[] whitePieceCounter = new PieceCounter[7];
        Piece activePiece = null;
        Field activeField = null;
        Menu mainMenu = new Menu();
        TextBlock TurnLine = new TextBlock();

        int activePlayer = 1;

        public MainWindow()
        {
            InitializeComponent();
            DrawBoard();
            DrawCounter();
            GeneratePieces();
            DrawPieces();
            Menu.SetPiecePositions(whitePieces, blackPieces);
            Menu.InitFieldColors(Field);
            Menu.Player(TurnLine, TheGrid);
        }

        //draw counter for captured Pieces per Type
        public void DrawCounter()
        {
            int index;

            for (index = 0; index < 7; index++)
            {
                blackPieceCounter[index] = new PieceCounter(10, index + 1);
                TheGrid.Children.Add(blackPieceCounter[index]);
                whitePieceCounter[index] = new PieceCounter(0, index + 1);
                TheGrid.Children.Add(whitePieceCounter[index]);
            }
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

            for (index = 0; index < 9; index++)
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

            if (activePiece == null) //first selection
            {
                if (selectedPieceColor == activePlayer)
                {
                    activePiece = selectedPiece;
                    activePiece.Background = Brushes.Red;
                    if (activePiece.GetIsCaptured()) Hints.HintsCapturedPieceOn(Field); //if it's a previously captured Piece 
                    else Hints.HintsOn(activePiece, Field); //Show where the Piece may move and set respective Permissions
                }
            }
            else //with active piece
            {
                Console.WriteLine(activePiece);
                int activePieceColor = activePiece.GetColor();

                if (activePieceColor == selectedPieceColor) //switch pieces if seletion is of same color
                {
                    if (activePieceColor == 1) activePiece.SetColor(1);
                    else activePiece.SetColor(0);

                    Hints.HintsOff(Field); //Remove all active hints and delete all active Permissions
                    activePiece = selectedPiece;
                    activePiece.Background = Brushes.Red;
                    if (activePiece.GetIsCaptured()) Hints.HintsCapturedPieceOn(Field); //if it's a previously captured Piece 
                    else Hints.HintsOn(activePiece, Field); //Show where the Piece may move and set respective Permissions
                }
                else //move and/or capture pieces
                {
                    int selectedPieceRow = Grid.GetRow(selectedPiece);
                    int selectedPieceColumn = Grid.GetColumn(selectedPiece);
                    int activePieceRow = Grid.GetRow(activePiece);
                    int activePieceColumn = Grid.GetColumn(activePiece);

                    if (Field[selectedPieceRow, selectedPieceColumn].GetPermission())//if movement is allowed
                    {
                        int gameOver = 0;

                        if (activePieceColor == 1) //if captured piece is black
                        {
                            if (selectedPiece is Pawn)
                            {
                                Grid.SetColumn(selectedPiece, 1);
                                whitePieceCounter[0].IncreaseCounter();
                            }
                            if (selectedPiece is Lance)
                            {
                                Grid.SetColumn(selectedPiece, 2);
                                whitePieceCounter[1].IncreaseCounter();
                            }
                            if (selectedPiece is Knight)
                            {
                                Grid.SetColumn(selectedPiece, 3);
                                whitePieceCounter[2].IncreaseCounter();
                            }
                            if (selectedPiece is SilverGeneral)
                            {
                                Grid.SetColumn(selectedPiece, 4);
                                whitePieceCounter[3].IncreaseCounter();
                            }
                            if (selectedPiece is GoldGeneral)
                            {
                                Grid.SetColumn(selectedPiece, 5);
                                whitePieceCounter[4].IncreaseCounter();
                            }
                            if (selectedPiece is Tower)
                            {
                                Grid.SetColumn(selectedPiece, 6);
                                whitePieceCounter[5].IncreaseCounter();
                            }
                            if (selectedPiece is Bishop)
                            {
                                Grid.SetColumn(selectedPiece, 7);
                                whitePieceCounter[6].IncreaseCounter();
                            }
                            Grid.SetRow(selectedPiece, 0);//move captured piece to designated spot outside of board
                            selectedPiece.SetColor(1);//change captured piece color to white
                            selectedPiece.SetIsCaptured(true);//flag captured piece as captured
                            selectedPiece.SetIsPromoted(false);//strip capturedPiece of Promotion
                            activePiece.SetColor(1);//reset activePiece color
                            if (Menu.WinCondition(selectedPiece is King, 1) == 1)//check if captured piece is King and possibly reset board
                            {
                                Hints.HintsOff(Field);
                                Menu.SetPiecePositions(whitePieces, blackPieces);
                                Menu.ResetPieceStates(whitePieces, blackPieces);
                                Menu.ResetPieceCounter(blackPieceCounter, whitePieceCounter);
                                Menu.ResetFieldColors(Field);
                                Menu.InitFieldColors(Field);
                                activePiece = null;
                                activePlayer = 1;
                                Menu.SetPlayerText(TurnLine, activePlayer);
                                gameOver = 1;
                            }
                        }
                        if (activePieceColor == 0) //if captured piece is white
                        {
                            if (selectedPiece is Pawn)
                            {
                                Grid.SetColumn(selectedPiece, 1);
                                blackPieceCounter[0].IncreaseCounter();
                            }
                            if (selectedPiece is Lance)
                            {
                                Grid.SetColumn(selectedPiece, 2);
                                blackPieceCounter[1].IncreaseCounter();
                            }
                            if (selectedPiece is Knight)
                            {
                                Grid.SetColumn(selectedPiece, 3);
                                blackPieceCounter[2].IncreaseCounter();
                            }
                            if (selectedPiece is SilverGeneral)
                            {
                                Grid.SetColumn(selectedPiece, 4);
                                blackPieceCounter[3].IncreaseCounter();
                            }
                            if (selectedPiece is GoldGeneral)
                            {
                                Grid.SetColumn(selectedPiece, 5);
                                blackPieceCounter[4].IncreaseCounter();
                            }
                            if (selectedPiece is Tower)
                            {
                                Grid.SetColumn(selectedPiece, 6);
                                blackPieceCounter[5].IncreaseCounter();
                            }
                            if (selectedPiece is Bishop)
                            {
                                Grid.SetColumn(selectedPiece, 7);
                                blackPieceCounter[6].IncreaseCounter();
                            }
                            Grid.SetRow(selectedPiece, 10);//move captured piece to designated spot outside of board
                            selectedPiece.SetColor(0);//change captured piece color to black
                            selectedPiece.SetIsCaptured(true);//flag captured piece as captured
                            selectedPiece.SetIsPromoted(false);//strip capturedPiece of Promotion
                            activePiece.SetColor(0);//reset activePiece color
                            if (Menu.WinCondition(selectedPiece is King, 0) == 1)//check if captured piece is King and possibly reset board
                            {
                                Hints.HintsOff(Field);
                                Menu.SetPiecePositions(whitePieces, blackPieces);
                                Menu.ResetPieceStates(whitePieces, blackPieces);
                                Menu.ResetFieldColors(Field);
                                Menu.InitFieldColors(Field);
                                Menu.ResetPieceCounter(blackPieceCounter, whitePieceCounter);
                                activePiece = null;
                                activePlayer = 1;
                                Menu.SetPlayerText(TurnLine, activePlayer);
                                gameOver = 1;
                            }
                        }

                        if (gameOver == 0)//only if King is still in play
                        {
                            Hints.HintsOff(Field); //reset field background colors and revoke permissions
                            Field[activePieceRow, activePieceColumn].SetColor(2);//set old field to neutral
                            Grid.SetRow(activePiece, selectedPieceRow);//move activePiece to new field
                            Grid.SetColumn(activePiece, selectedPieceColumn);
                            Field[selectedPieceRow, selectedPieceColumn].SetColor(activePieceColor);//set new field to activePieces color
                            if (selectedPieceRow >= 7 && activePieceColor == 1 && activePiece.GetIsPromoted() == false) //if whitePiece reached enemy Territory and is not yet promoted ask for promotion 
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Promote?", "", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    activePiece.SetIsPromoted(true);
                                }
                            }
                            if (selectedPieceRow <= 3 && activePieceColor == 0 && activePiece.GetIsPromoted() == false) //if blackPiece reached enemy Territory and is not yet promoted ask for promotion
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Promote?", "", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    activePiece.SetIsPromoted(true);
                                }
                            }
                            activePiece = null; // let go of active Piece
                            activePlayer = activePlayer == 1 ? 0 : 1; //Switch Player
                            Menu.SetPlayerText(TurnLine, activePlayer);
                        }
                    }
                }
            }
        }

        public void Move(object sender, RoutedEventArgs e)
        {
            Field activeField = sender as Field;

            if (activeField.GetColor() != 2) return;//don't do anything if the field is occupied (Player should select the Piece on it instead)
            if (activePiece != null) //Do nothing if no Piece is active
            {
                int pieceRow = Grid.GetRow(activePiece);
                int pieceColumn = Grid.GetColumn(activePiece);
                int fieldRow = Grid.GetRow(activeField);
                int fieldColumn = Grid.GetColumn(activeField);
                int activePieceColor = activePiece.GetColor();

                if (activeField.GetPermission()) //check if Movement is allowed if yes move
                {
                    if (activePiece.GetIsCaptured()) //for pieces outside the board
                    {
                        activePiece.SetIsCaptured(false);
                        if (activePlayer == 1) whitePieceCounter[pieceColumn - 1].DecreaseCounter();
                        if (activePlayer == 0) blackPieceCounter[pieceColumn - 1].DecreaseCounter();
                    }
                    else Field[pieceRow, pieceColumn].SetColor(2); //make old field neutral

                    if (activePieceColor == 1)//for white pieces
                    {
                        activePiece.SetColor(1);
                        activeField.SetColor(1);
                        if (fieldRow >= 7 && activePiece.GetIsPromoted() == false)//if reached enemy Territory and is not promoted ask for promotion
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Promote?", "", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                activePiece.SetIsPromoted(true);
                            }
                        }
                    }
                    else//for black pieces
                    {
                        activePiece.SetColor(0);
                        activeField.SetColor(0);
                        if (fieldRow <= 3 && activePiece.GetIsPromoted() == false)//if reached enemy Territory and is not promoted ask for promotion
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Promote?", "", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                activePiece.SetIsPromoted(true);
                            }
                        }
                    }

                    Hints.HintsOff(Field); //delete all hints and permissions
                    Grid.SetRow(activePiece, fieldRow);
                    Grid.SetColumn(activePiece, fieldColumn);
                    activePiece = null;
                    activePlayer = activePlayer == 1 ? 0 : 1;
                    Menu.SetPlayerText(TurnLine, activePlayer);
                }
                else //if move not allowed
                {
                    Hints.HintsOff(Field); //delete all hints and permissions
                    if (activePieceColor == 1)
                    {
                        activePiece.SetColor(1);
                    }
                    else
                    {
                        activePiece.SetColor(0);
                    }
                    activePiece = null;
                }
            }
        }


        //Close the application
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //return to initial board conditions
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Menu.ResetFieldColors(Field);//set all fields to neutral and free fields above and below the board
            Hints.HintsOff(Field);//delete all move permissions and reset highlightings
            Menu.SetPiecePositions(whitePieces, blackPieces);//return all Pieces to starting positions
            Menu.ResetPieceStates(whitePieces, blackPieces);//return all Piece colors to normal and reset IsCaptured value
            Menu.InitFieldColors(Field);//set all now occupied fields to their respective color
            Menu.ResetPieceCounter(blackPieceCounter, whitePieceCounter);
            activePiece = null; //delete active Piece value
            activePlayer = 1; //reset starting player to white
            Menu.SetPlayerText(TurnLine, activePlayer);
        }

        //Can't get Read or Write Permissions from Windows UAC. Don't know why.
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult notification;
            notification = MessageBox.Show("Sorry this feature is not yet implemented", "", MessageBoxButton.OK);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult notification;
            notification = MessageBox.Show("Sorry this feature is not yet implemented", "", MessageBoxButton.OK);
        }
    }
}
