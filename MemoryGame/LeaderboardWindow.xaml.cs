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
    /// Interaction logic for LeaderBoardWindow.xaml
    /// </summary>
    public partial class LeaderBoardWindow : Window
    {
        public LeaderBoardWindow()
        {
            InitializeComponent();
            DBManager db = new DBManager();
            List<Leaderboard> leaderboards = db.displayLeaderboards();
            foreach (Leaderboard lb in leaderboards)
            {
                TextBox tx = new TextBox();
                tx.Text = $"{lb.Name} | {lb.Difficulty} | {lb.GridSize} | {lb.Score} | {lb.Wave}";
                lsLeaderboard.Items.Add(tx.Text);
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
