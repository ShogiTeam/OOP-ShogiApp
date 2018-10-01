using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shogi_v_1_0
{
    //Class GoldGeneral - can move 1 field around except diagonally away from enemy
    class GoldGeneral : Piece
    {
        int allegiance;

        public GoldGeneral(int row, int column, int color) : base(row, column, color)
        {
            BitmapImage imageWhite = new BitmapImage();
            imageWhite.BeginInit();
            imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\GoldGeneralWhite.bmp", UriKind.RelativeOrAbsolute);
            imageWhite.EndInit();
            ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };

            BitmapImage imageBlack = new BitmapImage();
            imageBlack.BeginInit();
            imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\GoldGeneralBlack.bmp", UriKind.RelativeOrAbsolute);
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
                imageWhite.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\GoldGeneralWhite.bmp", UriKind.RelativeOrAbsolute);
                imageWhite.EndInit();
                ImageBrush BackgroundImageWhite = new ImageBrush() { ImageSource = imageWhite };
                Background = BackgroundImageWhite;
                allegiance = 1;
            }
            else
            {
                BitmapImage imageBlack = new BitmapImage();
                imageBlack.BeginInit();
                imageBlack.UriSource = new Uri(@"..\Shogi_v_1_0\Properties\GoldGeneralBlack.bmp", UriKind.RelativeOrAbsolute);
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
}