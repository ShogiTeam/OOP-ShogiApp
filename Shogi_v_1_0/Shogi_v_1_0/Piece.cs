using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shogi_v_1_0
{
    class Piece : Panel
    {
        int allegiance;
        bool isCaptured;

        public Piece(int row, int column, int color)
        {
            allegiance = color;
            isCaptured = false;
            Height = 30;
            Width = 30;
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
            Background = null;
            SetColor(color);
            GetColor();
        }

        public void SetColor(int color)
        {
            if (color == 1)
            {
                Background = Brushes.White;
                allegiance = 1;
            }
            else
            {
                Background = Brushes.Black;
                allegiance = 0;
            }
        }

        public int GetColor()
        {
            return allegiance;
        }

        public void SetIsCaptured(bool value)
        {
            isCaptured = value;
        }

        public bool GetIsCaptured()
        {
            return isCaptured;
        }

        virtual public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            return false;
        }
    }

    class Pawn : Piece
    {
        int fieldColumn;
        int fieldRow;

        public Pawn(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (fieldRow == pieceRow -1 && fieldColumn == pieceColumn && GetColor() == 0)
            {
                return true;
            }
            if (fieldRow == pieceRow +1 && fieldColumn == pieceColumn && GetColor() == 1)
            {
                return true;
            }
            return false;
        }
    }

    class Lance : Piece
    {
        int fieldColumn;
        int fieldRow;

        public Lance(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 1)
            {
                if (fieldColumn == pieceColumn && fieldRow >= pieceRow)
                {
                    return true;
                }
            }
            else
            {
                if (fieldColumn == pieceColumn && fieldRow <= pieceRow)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Tower : Piece
    {
        int fieldColumn;
        int fieldRow;

        public Tower(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (fieldColumn == pieceColumn || fieldRow == pieceRow)
            {
                return true;
            }
            return false;
        }
    }

    class SilverGeneral : Piece
    {
        int fieldColumn;
        int fieldRow;

        public SilverGeneral(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)
            {
                if (fieldRow == pieceRow -1 && (fieldColumn == pieceColumn +1 || fieldColumn == pieceColumn -1 || fieldColumn == pieceColumn))
                {
                    return true;
                }
                if (fieldRow == pieceRow +1 && (fieldColumn == pieceColumn +1 || fieldColumn == pieceColumn -1))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (fieldRow == pieceRow -1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))
                {
                    return true;
                }
                if (fieldRow == pieceRow +1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))
                {
                    return true;
                }
                return false;
            }
        }
    }

    class GoldGeneral : Piece
    {
        int fieldColumn;
        int fieldRow;

        public GoldGeneral(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)
            {
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))
                {
                    return true;
                }
                if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))
                {
                    return true;
                }
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))
                {
                    return true;
                }
                return false; 
            }
            else
            {
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))
                {
                    return true;
                }
                if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))
                {
                    return true;
                }
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))
                {
                    return true;
                }
                return false;
            }
        }
    }

    class King : Piece
    {
        int fieldColumn;
        int fieldRow;

        public King(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))
            {
                return true;
            }
            if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))
            {
               return true;
            }
            if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))
            {
                return true;
            }
            return false;
        }
    }

    class Bishop : Piece
    {
        int fieldColumn;
        int fieldRow;

        public Bishop(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);
            int index;

            for (index = 1; index <= 8; index++)
            {
                if (fieldColumn == pieceColumn + index && (fieldRow == pieceRow + index || fieldRow == pieceRow - index))
                {
                    return true;
                }
                if (fieldColumn == pieceColumn - index && (fieldRow == pieceRow + index || fieldRow == pieceRow - index))
                {
                    return true;
                }
            }
            return false;            
        }
    }

    class Knight : Piece
    {
        int fieldColumn;
        int fieldRow;

        public Knight(int row, int column, int color) : base(row, column, color)
        {
            IsMoveAllowed(fieldRow, fieldColumn);
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)
            {
                if ((fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1) && fieldRow == pieceRow - 2)
                {
                    return true;
                }
            }
            else
            {
                if ((fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1) && fieldRow == pieceRow + 2)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
