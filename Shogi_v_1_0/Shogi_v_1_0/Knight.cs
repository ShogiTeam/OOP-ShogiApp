using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //Class Knight - can move to a field thats 2 towards enemy and one left or right (similar to knight in western chess, but can't go back or sideways)
    class Knight : Piece
    {
        int allegiance;

        public Knight(int row, int column, int color) : base(row, column, color)
        {
            BitmapImage imageWhite = new BitmapImage();
            imageWhite.BeginInit();
            if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightWhite.bmp", UriKind.RelativeOrAbsolute);
            imageWhite.EndInit();
            ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };

            BitmapImage imageBlack = new BitmapImage();
            imageBlack.BeginInit();
            if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightBlack.bmp", UriKind.RelativeOrAbsolute);
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
                if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightWhite.bmp", UriKind.RelativeOrAbsolute);
                imageWhite.EndInit();
                ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };
                Background = BackgroundImageWhite;
                allegiance = 1;
            }
            else
            {
                BitmapImage imageBlack = new BitmapImage();
                imageBlack.BeginInit();
                if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\KnightBlack.bmp", UriKind.RelativeOrAbsolute);
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
                if (GetIsPromoted())//promoted Knight moves like GoldGeneral
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