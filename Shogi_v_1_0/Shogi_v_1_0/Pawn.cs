using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //Pawn Class - can move 1 field towards enemy side
    class Pawn : Piece
    {
        int allegiance;

        public Pawn(int row, int column, int color) : base(row, column, color)
        {
            BitmapImage imageWhite = new BitmapImage();
            imageWhite.BeginInit();
            if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnWhite.bmp", UriKind.RelativeOrAbsolute);
            imageWhite.EndInit();
            ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };

            BitmapImage imageBlack = new BitmapImage();
            imageBlack.BeginInit();
            if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnBlack.bmp", UriKind.RelativeOrAbsolute);
            imageBlack.EndInit();
            ImageBrush BackgroundImageBlack = new ImageBrush() { ImageSource = imageBlack };
            if (color == 1)
            {
                Background = BackgroundImageWhite;
            }
            else
            {
                Background = BackgroundImageBlack;
            }
            SetColor(color);
        }

        override public void SetColor(int color)
        {
            if (color == 1)
            {
                BitmapImage imageWhite = new BitmapImage();
                imageWhite.BeginInit();
                if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnWhite.bmp", UriKind.RelativeOrAbsolute);
                imageWhite.EndInit();
                ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };
                Background = BackgroundImageWhite;
                allegiance = 1;
            }
            else
            {
                BitmapImage imageBlack = new BitmapImage();
                imageBlack.BeginInit();
                if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\PawnBlack.bmp", UriKind.RelativeOrAbsolute);
                imageBlack.EndInit();
                ImageBrush BackgroundImageBlack = new ImageBrush() { ImageSource = imageBlack };
                Background = BackgroundImageBlack;
                allegiance = 0;
            }
        }

        override public int GetColor()
        {
            return allegiance;
        }

        override public bool IsMoveAllowed(int fieldRow, int fieldColumn)
        {
            int pieceRow = Grid.GetRow(this);
            int pieceColumn = Grid.GetColumn(this);

            if (GetColor() == 0)//field one above in same column if piece is black
            {
                if (GetIsPromoted())//promoted Pawn moves like GoldGeneral
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
}
