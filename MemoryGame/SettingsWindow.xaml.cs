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
            DBManager db = new DBManager();
            Settings tempsettings = db.checkSettings();
            if (!(tempsettings == null))
            {
                txtName.Text = tempsettings.Name;
                cmbDifficulty.Text = tempsettings.Difficulty;
                cmbGridSize.Text = $"{tempsettings.GridSize.ToString()}*{tempsettings.GridSize.ToString()}";
                cmbMultiColour.Text = tempsettings.MultiColour.ToString();
                cmbLeaderboards.Text = tempsettings.LeaderBoards.ToString();

            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            rctSaved.Fill = Brushes.Green;
            Settings settings = new Settings(txtName.Text, cmbDifficulty.Text,
                Convert.ToInt32(cmbGridSize.Text.Substring(0,1)), Convert.ToBoolean(cmbMultiColour.Text),
                Convert.ToBoolean(cmbLeaderboards.Text));

            DBManager db = new DBManager();
            Settings tempSettings = db.checkSettings();
            if (tempSettings == null)
            {
                db.addSettings(settings);
            }
            else
            {
                db.updateSettings(settings);
            }
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

    public class Settings
    {
        private string name;
        private string difficulty;
        private int gridSize;
        private bool multiColour;
        private bool leaderBoards;

        public Settings(string name, string difficulty, int gridSize, bool multiColour, bool leaderBoards)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.gridSize = gridSize;
            this.multiColour = multiColour;
            this.leaderBoards = leaderBoards;
        }

        public string Name { get => name; set => name = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public int GridSize { get => gridSize; set => gridSize = value; }
        public bool MultiColour { get => multiColour; set => multiColour = value; }
        public bool LeaderBoards { get => leaderBoards; set => leaderBoards = value; }
    }
}
