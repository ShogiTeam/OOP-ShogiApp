using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schach
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Schachfeld[,] spielbrett = new Schachfeld[8, 8];
        Schachfeld gewähltesFeld = null;
        public MainWindow()
        {
            InitializeComponent();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    spielbrett[y, x] = new Schachfeld(x, y);
                    spielbrett[y, x].MouseEnter += ShowHints;
                    spielbrett[y, x].MouseLeave += ClearHints;
                    spielbrett[y, x].MouseLeftButtonDown += BewegeFiguren;
                    theGrid.Children.Add(spielbrett[y, x]);
                }
            }

            spielbrett[1, 1].Figur = new SchwarzerTurm();
            spielbrett[3, 4].Figur = new WeißerTurm();
            spielbrett[2, 6].Figur = new WeißerLäufer();
            spielbrett[4, 3].Figur = new SchwarzerLäufer();
            spielbrett[5, 6].Figur = new SchwarzerKönig();
            spielbrett[3, 6].Figur = new SchwarzerSpringer();
        }

        private void BewegeFiguren(object sender, MouseButtonEventArgs e)
        {
            if (gewähltesFeld == null)
            {
                gewähltesFeld = sender as Schachfeld;
                gewähltesFeld.Foreground = Brushes.Tomato; // Zur Anzeige, dass dieses Feld gerade angewählt ist.
            }
            else
            {
                if (gewähltesFeld.Figur.IstZugErlaubt(sender as Schachfeld))
                {
                    (sender as Schachfeld).Figur = gewähltesFeld.Figur; // Figurenreferenz kopieren
                    gewähltesFeld.EntferneFigur(); // Die Referenz auf dem alten Platz entfernen
                    gewähltesFeld.Foreground = Brushes.Black; // "Selektion" zurücksetzen
                    gewähltesFeld = null;                     // "Selektion" zurücksetzen
                }
                else
                {
                    gewähltesFeld.Foreground = Brushes.Black; // "Selektion" zurücksetzen
                    gewähltesFeld = null;                     // "Selektion" zurücksetzen
                    MessageBox.Show("Zug ist nicht erlaubt!", "Falscher Zug", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
        }

        private void ClearHints(object sender, MouseEventArgs e)
        {
            foreach (Schachfeld feld in spielbrett) feld.LöscheHinweis(); // Die Hintergrundfarbe von jedem Feld zurücksetzten
        }

        private void ShowHints(object sender, MouseEventArgs e)
        {
            if ((sender as Schachfeld).Figur == null) return; // Nichts tun, wenn aus dem Feld keine Figur steht

            Schachfigur f = (sender as Schachfeld).Figur;

            foreach(Schachfeld feld in spielbrett) // Alle Felder durchgehen und prüfen, ob dieses Feld von der Figur erreicht werden kann
            {
                if (f.IstZugErlaubt(feld)) feld.ZeigeHinweis(); // Wenn dieses Feld von der Figur erreicht werden kann, Feld einfärben.
            }
        }
    }
}
