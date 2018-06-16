using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Schach
{
    class Schachfeld : TextBlock
    {
        int x;
        int y;
        Schachfigur f;

        public Schachfeld(int x, int y)
        {
            this.x = x;
            this.y = y;
            f = null;

            Background = (x + y) % 2 == 0 ? Brushes.Gainsboro : Brushes.White;
            Text = string.Empty;
            Grid.SetColumn(this, x);
            Grid.SetRow(this, y);
            Width = 80.0;
            Height = 80.0;
            FontSize = 60.0;
            TextAlignment = TextAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            FontFamily = new FontFamily("Open Sans");
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public Schachfigur Figur
        {
            get { return f; }
            set
            {
                f = value;
                Schachfigur.SetzeKoordinaten(value, x, y);
                Text = f.ToString();
            }
        }

        public Schachfigur EntferneFigur()
        {
            Schachfigur d = f;
            f = null;
            Text = string.Empty;

            return d; 
        }
        public void LöscheHinweis()
        {
            Background = (x + y) % 2 == 0 ? Brushes.Gainsboro : Brushes.White;
        }

        public void ZeigeHinweis()
        {
            Background = (x + y) % 2 == 0 ? Brushes.LightGreen : Brushes.PaleGreen; ;
        }
    }
}
