using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //general constructor for all moveable objects
    class Piece : Panel
    {
        //BitmapImage image;

        //needed variables for getter and setter methods
        //allegiance is int instead of bool to accomodate possible future gamemodes with more than 2 parties
        int allegiance;
        bool isCaptured;
        bool isKing;
        bool isPromoted = false;

        //constructor
        public Piece(int row, int column, int color)
        {
            //allegiance = color;
            
            isCaptured = false;
            Height = 30;
            Width = 30;
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);
            SetColor(color);
            GetColor();
            //image = new BitmapImage() {UriSource = new Uri(@"C:\Users\Heike\Desktop\Shogi_Fuyo.png", UriKind.Absolute) };
            //ImageBrush BackgroundImage = new ImageBrush() { ImageSource = image, Stretch = Stretch.Fill, Viewbox = new Rect(0, 0, 30, 30) };
            //Background = BackgroundImage;
        }

        //public BitmapImage Symbol
        //{
        //    get { return this.image; }
        //}

        public void SetIsKing(bool value)
        {
            isKing = value;
        }

        public bool GetIsKing()
        {
            return isKing;
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

        public bool GetIsPromoted()
        {
            return isPromoted;
        }

        public void SetIsPromoted(bool value)
        {
            isPromoted = value;
        }

        //Piece needs the method
        //MainWindow uses activePiece to invoke movement methods
        //activePiece is defined as general Piece
        virtual public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            return false;
        }
    }

    //Pawn Class - can move 1 field towards enemy side
    class Pawn : Piece
    {
        public Pawn(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)//field one above in same column if piece is black
            {
                if(GetIsPromoted())//promoted Pawn moves like GoldGeneral
                {
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldRow == pieceRow - 1 && fieldColumn == pieceColumn) 
                {
                    return true;
                }
            }
            else//field one below in same column if piece is white
            {
                if (GetIsPromoted())//promoted Pawn moves like GoldGeneral
                {
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldRow == pieceRow + 1 && fieldColumn == pieceColumn)
                {
                    return true;
                }
            }
            
            return false;
        }
    }

    //Lance Class - can move in straight line towards enemy side
    class Lance : Piece
    {
        public Lance(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)//field is in same column and above current field if piece is black
            {
                if(GetIsPromoted())//promoted Lance moves like GoldGeneral
                {
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldColumn == pieceColumn && fieldRow <= pieceRow)
                {
                    return true;
                }
            }
            else//field is in same column and below current field if piece is white
            {
                if (GetIsPromoted())//promoted Lance moves like GoldGeneral
                {
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldColumn == pieceColumn && fieldRow >= pieceRow)
                {
                    return true;
                }
            }
            return false;
        }
    }

    //Class Tower - can move in straight line horizontally or vertically
    class Tower : Piece
    {
        public Tower(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetIsPromoted())//promoted Tower moves additionally like King
            {
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if 1 field diagonally up
                {
                    return true;
                }
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if 1 field diagonally down
                {
                    return true;
                }
            }
            //normal movement
            if (fieldColumn == pieceColumn || fieldRow == pieceRow)//either row or column must be same
            {
                return true;
            }
            return false;
        }
    }

    //Class SilverGeneral - can move 1 field diagonally and 1 field towards enemy
    class SilverGeneral : Piece
    {
        public SilverGeneral(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)//if black
            {
                if (GetIsPromoted())//promoted SilverGeneral moves like GoldGeneral
                {
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                {
                    return true;
                }
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 diagonally away from enemy
                {
                    return true;
                }
                return false;
            }
            else //if white
            {
                if (GetIsPromoted())//promoted SilverGeneral moves like GoldGeneral
                {
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 diagonally away from enemy
                {
                    return true;
                }
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                {
                    return true;
                }
                return false;
            }
        }
    }

    //Class GoldGeneral - can move 1 field around except diagonally away from enemy
    class GoldGeneral : Piece
    {
        public GoldGeneral(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)//if black
            {
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                {
                    return true;
                }
                if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                {
                    return true;
                }
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                {
                    return true;
                }
                return false;
            }
            else//if white
            {
                if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                {
                    return true;
                }
                if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                {
                    return true;
                }
                if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                {
                    return true;
                }
                return false;
            }
        }
    }

    //Class Kind - can move 1 field in all directions
    class King : Piece
    {
        public King(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if 1 field straight or diagonally up
            {
                return true;
            }
            if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if 1 field to either side
            {
                return true;
            }
            if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if 1 field straight oder diagonally down
            {
                return true;
            }
            return false;
        }
    }

    //Class Bishop - can move diagonally
    class Bishop : Piece
    {
        public Bishop(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);
            int index;

            //prmoted Bishop additionally moves like King
            if (GetIsPromoted())
            {
                if (fieldColumn == pieceColumn && (fieldRow == pieceRow + 1 || fieldRow == pieceRow - 1))//if 1 field straight up or down
                {
                    return true;
                }
                if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if 1 field to either side
                {
                    return true;
                }
            }
            //normal movement
            for (index = 1; index <= 8; index++)
            {
                if (fieldColumn == pieceColumn + index && (fieldRow == pieceRow + index || fieldRow == pieceRow - index))//if field is diagonally right
                {
                    return true;
                }
                if (fieldColumn == pieceColumn - index && (fieldRow == pieceRow + index || fieldRow == pieceRow - index))//if field is diagonally left
                {
                    return true;
                }
            }
            return false;
        }
    }

    //Class Knight - can move to a field thats 2 towards enemy and one left or right (similar to knight in western chess, but can't go back or sideways)
    class Knight : Piece
    {
        public Knight(int row, int column, int color) : base(row, column, color) { }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);
            
            if (GetColor() == 0)//if black
            {
                if (GetIsPromoted())//promoted Knight moves like GoldGeneral
                {
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if (fieldRow == pieceRow - 2 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 2 towards enemy and 1 to either side
                {
                    return true;
                }
            }
            else//if white
            {
                if(GetIsPromoted())//promoted Knight moves like GoldGeneral
                {
                    if (fieldRow == pieceRow + 1 && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1 || fieldColumn == pieceColumn))//if field is 1 towards enemy straight or diagonally
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow && (fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1))//if field is 1 to either side
                    {
                        return true;
                    }
                    if (fieldRow == pieceRow - 1 && (fieldColumn == pieceColumn))//if field is 1 away from enemy
                    {
                        return true;
                    }
                    return false;
                }
                //normal movement
                if ((fieldColumn == pieceColumn + 1 || fieldColumn == pieceColumn - 1) && fieldRow == pieceRow + 2)//if field is 2 towards enemy and 1 to either side
                {
                    return true;
                }
            }
            return false;
        }
    }
}
