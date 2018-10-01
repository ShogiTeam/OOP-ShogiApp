using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //Lance Class - can move in straight line towards enemy side
    class Lance : Piece
    {
        int allegiance;

        public Lance(int row, int column, int color) : base(row, column, color)
        {
            BitmapImage imageWhite = new BitmapImage();
            imageWhite.BeginInit();
            if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\LancePromotedWhite.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\LanceWhite.bmp", UriKind.RelativeOrAbsolute);
            imageWhite.EndInit();
            ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };

            BitmapImage imageBlack = new BitmapImage();
            imageBlack.BeginInit();
            if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\LancePromotedBlack.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\LanceBlack.bmp", UriKind.RelativeOrAbsolute);
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
                if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\LancePromotedWhite.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"Shogi_v_1_0\Properties\LanceWhite.bmp", UriKind.RelativeOrAbsolute);
                imageWhite.EndInit();
                ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };
                Background = BackgroundImageWhite;
                allegiance = 1;
            }
            else
            {
                BitmapImage imageBlack = new BitmapImage();
                imageBlack.BeginInit();
                if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\LancePromotedBlack.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"Shogi_v_1_0\Properties\LanceBlack.bmp", UriKind.RelativeOrAbsolute);
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

            if (GetColor() == 0)//field is in same column and above current field if piece is black
            {
                if (GetIsPromoted())//promoted Lance moves like GoldGeneral
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
}