using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //Class Bishop - can move diagonally
    class Bishop : Piece
    {
        int allegiance;

        public Bishop(int row, int column, int color) : base(row, column, color)
        {
            BitmapImage imageWhite = new BitmapImage();
            imageWhite.BeginInit();
            if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\BishopPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopWhite.bmp", UriKind.RelativeOrAbsolute);
            imageWhite.EndInit();
            ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };

            BitmapImage imageBlack = new BitmapImage();
            imageBlack.BeginInit();
            if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
            if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopBlack.bmp", UriKind.RelativeOrAbsolute);
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
                if (GetIsPromoted()) imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopPromotedWhite.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopWhite.bmp", UriKind.RelativeOrAbsolute);
                imageWhite.EndInit();
                ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };
                Background = BackgroundImageWhite;
                allegiance = 1;
            }
            else
            {
                BitmapImage imageBlack = new BitmapImage();
                imageBlack.BeginInit();
                if (GetIsPromoted()) imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopPromotedBlack.bmp", UriKind.RelativeOrAbsolute);
                if (!GetIsPromoted()) imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\BishopBlack.bmp", UriKind.RelativeOrAbsolute);
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
}