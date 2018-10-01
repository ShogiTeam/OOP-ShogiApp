using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Shogi_v_1_0
{
    class Field : Border
    {
        int allegiance;
        bool allowed;
        Piece piece;

        public Field(int row, int column, int color)
        {
            Background = Brushes.LightGray;
            BorderThickness = new Thickness(1, 1, 0, 0);
            BorderBrush = Brushes.Black;
            allegiance = color;
            allowed = false;
            piece = null;
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
            GetColor();
            GetPermission();
        }

        public Field(int row, int column)
        {
            allowed = true;
        }

        public int GetColor()
        {
            return allegiance;
        }

        public void SetColor(int color)
        {
            this.allegiance = color;
        }

        public bool GetPermission()
        {
            return allowed;
        }

        public void SetPermission(bool value)
        {
            allowed = value;
        }

        public Piece GetPiece()
        {
            return piece;
        }

        public void SetPiece(Piece piece)
        {
            this.piece = piece;
        }
    }
}
