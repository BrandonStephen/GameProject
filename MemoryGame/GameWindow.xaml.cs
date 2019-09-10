using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Board board;
        private List<Colours> colours;
        public GameWindow()
        {

            Game game = new Game();
            InitializeComponent();
            
            
            Board board = game.initialise(lblScore, lblWave, btnStart);
            this.tempGrid.Children.Add(game.start(board, out List<Colours> colours));
            this.board = board;
            this.colours = colours;
            
            
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            board.StartGame.IsEnabled = false;
            game.begin(board, colours);
        }
    }

    class Game
    {
        private List<Colours> colours;
        private Board board;
        public Board initialise(Label score, Label wave, Button start)
        {
            Random rn = new Random();
            List<int> pattern = new List<int>();
            int amount = 64;
            for (int i = 0; i < 1000; i++)
            {
                pattern.Add(rn.Next(16));
            }
            Board board = new Board(amount, pattern, "easy", 40, 40, score, wave, start, new List<int>());
            return board;
        }

        public Grid start(Board board, out List<Colours> colours)
        {

            Grid grid = board.createGrids(board.ColourCount, board.Height, board.Width);
            List<Colours> colObj = new List<Colours>();
            int count = 1;
            List<Brush> Hashcolours = new List<Brush>();
            Hashcolours.Add(Brushes.Red);
            Hashcolours.Add(Brushes.Yellow);
            Hashcolours.Add(Brushes.Blue);
            Hashcolours.Add(Brushes.Green);
            int colourSelect = 0;
            int id = 0;


            for (int i = 0; i < Math.Sqrt(board.ColourCount); i++)
            {
                for (int j = 0; j < Math.Sqrt(board.ColourCount); j++)
                {
                    Button MyControl1 = new Button();
                    MyControl1.Name = "Button"+id.ToString();
                    MyControl1.Click += Button_Click;
                    MyControl1.Background = Hashcolours[colourSelect];
                    colObj.Add(new Colours(Hashcolours[colourSelect].ToString(), id, MyControl1));
                    if (!(colourSelect >= 3)) { colourSelect++; }
                    else { colourSelect = 0; }
                    

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    grid.Children.Add(MyControl1);

                    count++;
                    id++;
                }

            }

            this.colours = colObj;
            this.board = board;
            colours = colObj;
            return grid;

        }

        public async void begin(Board board, List<Colours> colours)
        {
            for (int i = 0; i < (Convert.ToInt32(board.Wave.Content)); i++)
            {
                colours[board.RandomPattern[i]].Button.IsEnabled = false;
                await Task.Delay(2000);
                colours[board.RandomPattern[i]].Button.IsEnabled = true;  //Background = new BrushConverter().ConvertFromString(colours[board.RandomPattern[i]].Colour) as SolidColorBrush;
       
            }
            int tempWave = Convert.ToInt32(board.Wave.Content);
            tempWave++;
            board.Wave.Content = tempWave.ToString();

        }

        public void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string tempString = b.Name.Replace("Button", "");
            this.board.UserChoice.Add(Convert.ToInt32(tempString));
            CheckPattern();
        }

        private void CheckPattern()
        {
            if (board.UserChoice.Count == Convert.ToInt32(board.Wave.Content) - 1)
            {
                for (int i = 0; i < board.UserChoice.Count; i++)
                {
                    if (!(board.UserChoice[i] == board.RandomPattern[i]))
                    {

                        MessageBox.Show("Failed");
                        break;
                        
                    }
                }
                board.UserChoice.Clear();
                begin(board, colours);
            }
        }
    }

    class Board
    {
        private int colourCount;
        private List<int> randomPattern;
        private string difficulty;
        private int height;
        private int width;
        private Label score;
        private Label wave;
        private Button startGame;
        private List<int> userChoice;
        

        public Board(int colourCount, List<int> randomPattern, string difficulty, int height, int width, Label score, Label wave, Button start, List<int> userList)
        {
            this.colourCount = colourCount;
            this.randomPattern = randomPattern;
            this.difficulty = difficulty;
            this.height = height;
            this.width = width;
            this.score = score;
            this.wave = wave;
            this.startGame = start;
            this.userChoice = userList;

        }

        public int ColourCount { get => colourCount; set => colourCount = value; }
        public List<int> RandomPattern { get => randomPattern; set => randomPattern = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public Label Score { get => score; set => score = value; }
        public Label Wave { get => wave; set => wave = value; }
        public Button StartGame { get => startGame; set => startGame = value; }
        public List<int> UserChoice { get => userChoice; set => userChoice = value; }

        public Grid createGrids(int size, int width, int height)
        {
            Grid gamegrid = new Grid();
            gamegrid.Width = width * Math.Sqrt(size);
            gamegrid.Height = height * Math.Sqrt(size);
            gamegrid.HorizontalAlignment = HorizontalAlignment.Center;
            gamegrid.VerticalAlignment = VerticalAlignment.Center;
            gamegrid.ShowGridLines = false;

            List<ColumnDefinition> gridCol = new List<ColumnDefinition>();
            List<RowDefinition> gridRow = new List<RowDefinition>();

            for (int i = 0; i < Math.Sqrt(size); i++)
            {
                ColumnDefinition tempCol = new ColumnDefinition();
                tempCol.Width = new GridLength(height);
                gridCol.Add(tempCol);

                RowDefinition temprow = new RowDefinition();
                temprow.Height = new GridLength(width);
                gridRow.Add(temprow);
            }

            foreach (ColumnDefinition cd in gridCol)
            {
                gamegrid.ColumnDefinitions.Add(cd);
            }
            foreach (RowDefinition rd in gridRow) gamegrid.RowDefinitions.Add(rd);

            return gamegrid;
        }
    }

    class Colours
    {
        private string colour;
        private int id;
        private Button button;
        

        public Colours(string colour, int id, Button button)
        {
            this.colour = colour;
            this.id = id;
            this.button = button;
        }

        public string Colour { get => colour; set => colour = value; }
        public int Id { get => id; set => id = value; }
        public Button Button { get => button; set => button = value; }
    }

    class DbManager
    {
        public string getDifficulty()
        {
            string difficulty = "";
            return difficulty;
        }
    }
}
