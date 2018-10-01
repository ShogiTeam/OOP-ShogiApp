using System;
using System.Windows;
using System.Windows.Controls;

namespace Shogi_v_1_0
{
    class Menu
    {
        static public int WinCondition(bool isKing, int color)
        {
            if (isKing == true && color == 1)
            {
                MessageBoxResult result;
                result = MessageBox.Show("White Wins!", "GAME OVER", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    Environment.Exit(0);
                    return 0;
                }
                if (result == MessageBoxResult.Yes) return 1;
            }
            if (isKing == true && color == 0)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Black Wins!", "GAME OVER", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    Environment.Exit(0);
                    return 0;
                }
                if (result == MessageBoxResult.Yes) return 1;
            }
            return -1;
        }

        //assings starting positions to all Pieces
        static public void SetPiecePositions(Piece[] whitePieces, Piece[] blackPieces)
        {
            int index;

            //set initial Pawn positions
            for (index = 0; index <= 9; index++)
            {
                Grid.SetRow(whitePieces[index], 3);
                Grid.SetRow(blackPieces[index], 7);
                Grid.SetColumn(whitePieces[index], index + 1);
                Grid.SetColumn(blackPieces[index], index + 1);
            }

            //set initial Tower positions
            Grid.SetRow(whitePieces[9], 2);
            Grid.SetColumn(whitePieces[9], 3);
            Grid.SetRow(blackPieces[9], 8);
            Grid.SetColumn(blackPieces[9], 7);
            //set initial Bishop positions
            Grid.SetRow(whitePieces[10], 2);
            Grid.SetColumn(whitePieces[10], 7);
            Grid.SetRow(blackPieces[10], 8);
            Grid.SetColumn(blackPieces[10], 3);

            //set initial special Piece Rows
            for (index = 11; index < 20; index++)
            {
                Grid.SetRow(whitePieces[index], 1);
                Grid.SetRow(blackPieces[index], 9);
            }

            //set initial Lance Columns
            Grid.SetColumn(whitePieces[11], 1);
            Grid.SetColumn(whitePieces[12], 9);
            Grid.SetColumn(blackPieces[11], 1);
            Grid.SetColumn(blackPieces[12], 9);
            //set initial Knight Columns
            Grid.SetColumn(whitePieces[13], 2);
            Grid.SetColumn(whitePieces[14], 8);
            Grid.SetColumn(blackPieces[13], 2);
            Grid.SetColumn(blackPieces[14], 8);
            //set initial SilverGeneral Columns
            Grid.SetColumn(whitePieces[15], 3);
            Grid.SetColumn(whitePieces[16], 7);
            Grid.SetColumn(blackPieces[15], 3);
            Grid.SetColumn(blackPieces[16], 7);
            //set initial GoldGeneral Columns
            Grid.SetColumn(whitePieces[17], 4);
            Grid.SetColumn(whitePieces[18], 6);
            Grid.SetColumn(blackPieces[17], 4);
            Grid.SetColumn(blackPieces[18], 6);

            //set initial King positions
            Grid.SetColumn(whitePieces[19], 5);
            Grid.SetColumn(blackPieces[19], 5);
        }

        //sets correct Field colors for default board state
        static public void InitFieldColors(Field[,] Field)
        {
            //  int indexRow;
            int indexColumn;

            for (indexColumn = 1; indexColumn <= 9; indexColumn++)
            {
                //set rows 1 and 3 to white color
                Field[1, indexColumn].SetColor(1);
                Field[3, indexColumn].SetColor(1);
                //set row 2 bishop and tower position fields to white color
                Field[2, 3].SetColor(1);
                Field[2, 7].SetColor(1);
                //set rows 1 and 3 to black color
                Field[7, indexColumn].SetColor(0);
                Field[9, indexColumn].SetColor(0);
                //set row 2 bishop and tower position fields to black color
                Field[8, 3].SetColor(0);
                Field[8, 7].SetColor(0);
            }
        }

        //resets color, promotion and capture values to default
        static public void ResetPieceStates(Piece[] whitePieces, Piece[] blackPieces)
        {
            int index;

            for (index = 0; index < 20; index++)
            {
                whitePieces[index].SetColor(1);
                whitePieces[index].SetIsCaptured(false);
                whitePieces[index].SetIsPromoted(false);
                blackPieces[index].SetColor(0);
                blackPieces[index].SetIsCaptured(false);
                blackPieces[index].SetIsPromoted(false);
            }
        }

        //sets all playable fieldcolors to neutral value(2)
        static public void ResetFieldColors(Field[,] Field)
        {
            int column;
            int row;

            for (column = 1; column < 10; column++)
            {
                for (row = 0; row < 11; row++)
                {
                    Field[row, column].SetColor(2);
                }
            }
        }

        static public void ResetPieceCounter(PieceCounter[] blackPieceCounter, PieceCounter[] whitePieceCounter)
        {
            int index;
            for (index = 0; index < 7; index++)
            {
                blackPieceCounter[index].ResetCounter();
                whitePieceCounter[index].ResetCounter();
            }
        }

        //Write active Player Message on Board
        static public void Player(TextBlock TurnLine, Grid TheGrid)
        {
            TurnLine.TextAlignment = TextAlignment.Center;
            TurnLine.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumnSpan(TurnLine, 2);
            TurnLine.Text = "Whites Turn";
            TheGrid.Children.Add(TurnLine);
            Grid.SetRow(TurnLine, 0);
            Grid.SetColumn(TurnLine, 9);
        }

        static public void SetPlayerText(TextBlock TurnLine, int Player)
        {
            if (Player == 1)
            {
                TurnLine.Text = "Whites Turn";
            }
            else
            {
                TurnLine.Text = "Blacks Turn";
            }
        }
    }
}
