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
using System.Windows.Shapes;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            rctSaved.Fill = Brushes.Green;
            await Task.Delay(2000);
            rctSaved.Fill = Brushes.Transparent;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
