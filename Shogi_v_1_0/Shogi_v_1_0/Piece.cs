using System.Windows;
using System.Windows.Controls;

namespace Shogi_v_1_0
{
    //general constructor for all moveable objects
    class Piece : Border
    {
        //needed variables for getter and setter methods
        //allegiance is int instead of bool to accomodate possible future gamemodes with more than 2 parties
        bool isCaptured;
        bool isPromoted = false;

        //constructor
        public Piece(int row, int column, int color)
        {
            //allegiance = color;

            BorderThickness = new Thickness(2, 2, 2, 2);
            isCaptured = false;
            Height = 30;
            Width = 30;
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
        }

        public void SetIsCaptured(bool value)
        {
            isCaptured = value;
        }

        public bool GetIsCaptured()
        {
            return isCaptured;
        }

        public bool GetIsPromoted()
        {
            return isPromoted;
        }

        public void SetIsPromoted(bool value)
        {
            isPromoted = value;
            SetColor(GetColor());
        }

        virtual public int GetColor()
        {
            return 1;
        }

        virtual public void SetColor(int color)
        {
            return;
        }

        //Piece needs the method
        //MainWindow uses activePiece to invoke movement methods
        //activePiece is defined as general Piece
        virtual public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            return false;
        }
    }
}
