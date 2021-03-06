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
using System.Windows.Threading;

namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _istErsterSpielerAmZug = true;
        private bool _istSpielBeendet = false;

        private readonly DispatcherTimer _blendeHinweisAusTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _blendeHinweisAusTimer.Tick += BlendeHinweisAus;
        }

        private void BlendeHinweisAus(object sender, EventArgs e)
        {
            _blendeHinweisAusTimer.Stop();
            HinweisLabel.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _istErsterSpielerAmZug = true;
            StarteSpielNeu();
        }

        private void Kaestchen_Click(object sender, RoutedEventArgs e)
        {
            Button kaestchen = (Button)sender;

            if (_istSpielBeendet)
            {
                StarteSpielNeu();
            }
            
            if (kaestchen.Content != null && kaestchen.Content.ToString() != "")
            {
                ZeigeHinweis("Kästchen belegt!", 1);
                return;
            }

            if (_istErsterSpielerAmZug)
            {
                kaestchen.Content = "X";
                _istErsterSpielerAmZug = false;
            }
            else
            {
                kaestchen.Content = "O";
                var vordergrund = kaestchen.Foreground;
                kaestchen.Foreground = kaestchen.Background;
                kaestchen.Background = vordergrund;
                _istErsterSpielerAmZug = true;
            }

            var gewinnReihe = SucheGewinnReihe();

            if (gewinnReihe.Count == 3)
            {
                HebeKaestchenHervor(gewinnReihe[0], gewinnReihe[1], gewinnReihe[2]);

                if (_istErsterSpielerAmZug)
                {
                    ZeigeHinweis("O gewinnt!");
                }
                else
                {
                    ZeigeHinweis("X gewinnt!");
                }
                _istSpielBeendet = true;
                return;
            }

            if (IstSpielfeldVoll())
            {
                ZeigeHinweis("Keiner gewinnt!");
                _istSpielBeendet = true;
            }
        }

        private void ZeigeHinweis(string hinweis, int ausblendeZeitInSekunden = 0)
        {
            HinweisLabel.Content = hinweis;
            HinweisLabel.Visibility = Visibility.Visible;

            if ((ausblendeZeitInSekunden > 0) && (!_blendeHinweisAusTimer.IsEnabled))
            {
                _blendeHinweisAusTimer.Interval = TimeSpan.FromSeconds(ausblendeZeitInSekunden);
                _blendeHinweisAusTimer.Start();
            }
        }

        private List<Button> SucheGewinnReihe()
        {
            var resultat = new List<Button>();

            if (IstGleicherSpielstein(kaestchen_0_0, kaestchen_0_1, kaestchen_0_2))
            {
                resultat.Add(kaestchen_0_0);
                resultat.Add(kaestchen_0_1);
                resultat.Add(kaestchen_0_2);
            }
            else if (IstGleicherSpielstein(kaestchen_1_0, kaestchen_1_1, kaestchen_1_2))
            {
                resultat.Add(kaestchen_1_0);
                resultat.Add(kaestchen_1_1);
                resultat.Add(kaestchen_1_2);
            }
            else if (IstGleicherSpielstein(kaestchen_2_0, kaestchen_2_1, kaestchen_2_2))
            {
                resultat.Add(kaestchen_2_0);
                resultat.Add(kaestchen_2_1);
                resultat.Add(kaestchen_2_2);
            }
            else if (IstGleicherSpielstein(kaestchen_0_0, kaestchen_1_0, kaestchen_2_0))
            {
                resultat.Add(kaestchen_0_0);
                resultat.Add(kaestchen_1_0);
                resultat.Add(kaestchen_2_0);
            }
            else if (IstGleicherSpielstein(kaestchen_0_1, kaestchen_1_1, kaestchen_2_1))
            {
                resultat.Add(kaestchen_0_1);
                resultat.Add(kaestchen_1_1);
                resultat.Add(kaestchen_2_1);
            }
            else if (IstGleicherSpielstein(kaestchen_0_2, kaestchen_1_2, kaestchen_2_2))
            {
                resultat.Add(kaestchen_0_2);
                resultat.Add(kaestchen_1_2);
                resultat.Add(kaestchen_2_2);
            }
            else if (IstGleicherSpielstein(kaestchen_0_0, kaestchen_1_1, kaestchen_2_2))
            {
                resultat.Add(kaestchen_0_0);
                resultat.Add(kaestchen_1_1);
                resultat.Add(kaestchen_2_2);
            }
            else if (IstGleicherSpielstein(kaestchen_2_0, kaestchen_1_1, kaestchen_0_2))
            {
                resultat.Add(kaestchen_2_0);
                resultat.Add(kaestchen_1_1);
                resultat.Add(kaestchen_0_2);
            }

            return resultat;
        }

        private bool IstGleicherSpielstein(Button erstesKaestchen, Button zweitesKaestchen, Button drittesKaestchen)
        {
            if (erstesKaestchen.Content.ToString() != ""
                && erstesKaestchen.Content.ToString() == zweitesKaestchen.Content.ToString()
                && zweitesKaestchen.Content.ToString() == drittesKaestchen.Content.ToString())
            {
                return true;
            }

            return false;
        }

        private void HebeKaestchenHervor(Button erstesKaestchen, Button zweitesKaestchen, Button drittesKaestchen)
        {
            erstesKaestchen.Background = (Brush)new BrushConverter().ConvertFrom("#FFCD00");
            zweitesKaestchen.Background = (Brush)new BrushConverter().ConvertFrom("#FFCD00");
            drittesKaestchen.Background = (Brush)new BrushConverter().ConvertFrom("#FFCD00");
        }

        private bool IstSpielfeldVoll()
        {
            foreach (var item in Spielfeld.Children)
            {
                Button kaestchen = item as Button;

                if (kaestchen != null && kaestchen.Content.ToString() == "")
                {
                    return false;
                }
            }

            return true;
        }

        private void StarteSpielNeu()
        {
            _istSpielBeendet = false;
            _istErsterSpielerAmZug = true;

            HinweisLabel.Visibility = Visibility.Hidden;
            HinweisLabel.Content = string.Empty;

            kaestchen_0_0.Content = string.Empty;
            kaestchen_1_0.Content = string.Empty;
            kaestchen_2_0.Content = string.Empty;
            kaestchen_0_1.Content = string.Empty;
            kaestchen_1_1.Content = string.Empty;
            kaestchen_2_1.Content = string.Empty;
            kaestchen_0_2.Content = string.Empty;
            kaestchen_1_2.Content = string.Empty;
            kaestchen_2_2.Content = string.Empty;

            kaestchen_0_0.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_1_0.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_2_0.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_0_1.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_1_1.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_2_1.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_0_2.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_1_2.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");
            kaestchen_2_2.Background = (Brush)new BrushConverter().ConvertFrom("#00A8C6");

            kaestchen_0_0.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_1_0.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_2_0.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_0_1.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_1_1.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_2_1.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_0_2.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_1_2.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
            kaestchen_2_2.Foreground = (Brush)new BrushConverter().ConvertFrom("#F9F2E7");
        }
    }
}
