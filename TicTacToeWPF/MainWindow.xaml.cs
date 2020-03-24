﻿using System;
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

namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _istErsterSpielerAmZug = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _istErsterSpielerAmZug = true;
            KaestchenLeeren();
        }

        private void Kaestchen_Click(object sender, RoutedEventArgs e)
        {
            Button kaestchen = (Button)sender;

            if (kaestchen.Content != null && kaestchen.Content.ToString() != "")
            {
                MessageBox.Show("Dieses Kästchen ist bereits belegt! Wähle ein anderes.");
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
                _istErsterSpielerAmZug = true;
            }
        }

        private void KaestchenLeeren()
        {
            kaestchen_0_0.Content = string.Empty;
            kaestchen_1_0.Content = string.Empty;
            kaestchen_2_0.Content = string.Empty;

            kaestchen_0_1.Content = string.Empty;
            kaestchen_1_1.Content = string.Empty;
            kaestchen_2_1.Content = string.Empty;

            kaestchen_0_2.Content = string.Empty;
            kaestchen_1_2.Content = string.Empty;
            kaestchen_2_2.Content = string.Empty;
        }
    }
}
