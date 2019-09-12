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
            
            // This creates a board Object, this is the foundation for the entire game
            Board board = game.initialise(lblScore, lblWave, btnStart, btnMainManu);
            // This is where a grid is dynamically created and then added to an existing grid on the XMAL
            this.tempGrid.Children.Add(game.start(board, out List<Colours> colours));
            // Allows the board Obj to be available for other methods
            this.board = board;
            // Allows the board Obj to be available for other methods
            this.colours = colours;
            
            
        }

        // This Button event will start the game
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            board.StartGame.IsEnabled = false;
            game.begin(board, colours);
        }

        // This button event will stop the game and return the user to the main menu.
        private void BtnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }

    class Game
    {
        private List<Colours> colours;
        private Board board;
        // This is where most of the components are initialised. This includes the random pattern and assignment of settings.
        public Board initialise(Label score, Label wave, Button start, Button closeWindow)
        {
            Random rn = new Random();
            List<int> pattern = new List<int>();
            DBManager db = new DBManager();
            Settings settings = db.checkSettings();
            
            for (int i = 0; i < 1000; i++)
            {
                pattern.Add(rn.Next((settings.GridSize * settings.GridSize)));
            }
            Board board = new Board((settings.GridSize * settings.GridSize), pattern, settings.Difficulty, 40, 40, score, wave, start, new List<int>(), closeWindow);
            return board;
        }

        public Grid start(Board board, out List<Colours> colours)
        {
            // Creates the grid 
            Grid grid = board.createGrids(board.ColourCount, board.Height, board.Width);
            List<Colours> colObj = new List<Colours>();
            int count = 1;
            // These are colours that can be used to make the grids have a multitude of colours
            List<Brush> Hashcolours = new List<Brush>();
            Hashcolours.Add(Brushes.Red);
            Hashcolours.Add(Brushes.Yellow);
            Hashcolours.Add(Brushes.Blue);
            Hashcolours.Add(Brushes.Green);
            Hashcolours.Add(Brushes.Black);
            Hashcolours.Add(Brushes.Purple);
            Hashcolours.Add(Brushes.Pink);
            Hashcolours.Add(Brushes.Cyan);

            Random rnd = new Random();
            int id = 0;
            DBManager db = new DBManager();
            Settings settings = db.checkSettings();

            // This loop goes through all of the grids and assigns a button to it, it also creates a Colour object
            for (int i = 0; i < Math.Sqrt(board.ColourCount); i++)
            {
                for (int j = 0; j < Math.Sqrt(board.ColourCount); j++)
                {
                    int newColour = rnd.Next(8);
                    Button MyControl1 = new Button();
                    MyControl1.Name = "Button"+id.ToString();
                    MyControl1.Click += Button_Click;

                    if (settings.MultiColour)
                    {
                        MyControl1.Background = Hashcolours[newColour];
                        colObj.Add(new Colours(Hashcolours[newColour].ToString(), id, MyControl1));
                    }
                    else {
                        MyControl1.Background = Hashcolours[0];
                        colObj.Add(new Colours(Hashcolours[0].ToString(), id, MyControl1));
                    }

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    grid.Children.Add(MyControl1);

                    count++;
                    id++;
                }

            }


            // Returns a grid, the colours and makes them available for the current class.
            this.colours = colObj;
            this.board = board;
            colours = colObj;
            return grid;

        }

        // Starts the game
        public async void begin(Board board, List<Colours> colours)
        {
            for (int i = 0; i < (Convert.ToInt32(board.Wave.Content)); i++)
            {
                colours[board.RandomPattern[i]].Button.IsEnabled = false;
                Console.Beep();
                await Task.Delay(getWaitTime(board));
                colours[board.RandomPattern[i]].Button.IsEnabled = true;  //Background = new BrushConverter().ConvertFromString(colours[board.RandomPattern[i]].Colour) as SolidColorBrush;
       
            }
        }

        // Gets the wait time based on the difficulty.
        public int getWaitTime(Board board)
        {
            int time = 0;
            switch (board.Difficulty)
            {
                case "Easy":
                    time = 2000;
                    break;
                case "Medium":
                    time = 1500;
                    break;
                case "Hard":
                    time = 1000;
                    break;
                case "Impossible":
                    time = 500;
                    break;
            }
            return time;
        }

        public void Button_Click(object sender, EventArgs e)
        {
            if (!(this.board.StartGame.IsEnabled))
            {
                Button b = (Button)sender;
                string tempString = b.Name.Replace("Button", "");
                this.board.UserChoice.Add(Convert.ToInt32(tempString));
                CheckPattern();
            }
        }

        private void CheckPattern()
        {
            bool willContinue = true;
            
            for (int i = 0; i < board.UserChoice.Count; i++)
            {
                if (!(board.UserChoice[i] == board.RandomPattern[i]))
                {

                    DBManager db = new DBManager();
                    Settings settings = db.checkSettings();
                    if (settings.LeaderBoards)
                    {
                        Leaderboard leaderboard = new Leaderboard(settings.Name, Convert.ToInt32(board.Score.Content), Convert.ToInt32(board.Wave.Content), board.Difficulty, board.ColourCount);
                        db.addToLeaderboards(leaderboard);
                    }
                    MessageBox.Show($"You got to wave {board.Wave.Content}, and scored {board.Score.Content} points");

                    List<int> pattern = new List<int>();
                    Random rn = new Random();
                    for (int x = 0; x < 1000; x++)
                    {
                        pattern.Add(rn.Next((board.ColourCount)));
                    }
                    board.RandomPattern = pattern;
                    board.StartGame.IsEnabled = true;
                    board.Score.Content = "0";
                    board.Wave.Content = "1";
                    board.UserChoice.Clear();

                    willContinue = false;
                    break;

                
                }
            }
                
                board.Score.Content = newScore(board).ToString();
                if (Convert.ToInt32(board.Wave.Content) == this.board.UserChoice.Count)
                {
                    board.UserChoice.Clear();
                    if (willContinue)
                    {
                        updateWave(this.board);
                        begin(board, colours);
                    }
                }
                
                
            
        }

        public void updateWave(Board board)
        {
            int tempWave = Convert.ToInt32(board.Wave.Content);
            tempWave++;
            board.Wave.Content = tempWave.ToString();
        }

        public double newScore(Board board)
        {
            int difficulty = 0;
            switch (board.Difficulty)
            {
                case "Easy":
                    difficulty = 5;
                    break;
                case "Medium":
                    difficulty = 10;
                    break;
                case "Hard":
                    difficulty = 30;
                    break;
                case "Impossible":
                    difficulty = 50;
                    break;
            }
            
            double tempScore = Convert.ToInt32(board.Score.Content);
            tempScore += 1 * (board.UserChoice.Count * (difficulty * Math.Sqrt(board.ColourCount)));

            Random rnd = new Random();
            if (rnd.Next(10) == 3)
            {
                tempScore += 100;
            }

            return tempScore;
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
        private Button closeGame;

        

        public Board(int colourCount, List<int> randomPattern, string difficulty, int height, int width, Label score, Label wave, Button start, List<int> userList, Button closeGame)
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
            this.closeGame = closeGame;

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
        public Button CloseGame { get => closeGame; set => closeGame = value; }

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
