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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            
            InitializeComponent();
            Game game = new Game();
            
            Board board = game.initialise();
            this.Content = game.start(board, out List<Colours> colours);
            
            
        }
    }

    class Game
    {
        public Board initialise()
        {
            Random rn = new Random();
            List<int> pattern = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                pattern.Add(rn.Next(1, 17));
            }
            Board board = new Board(169, pattern, "easy", 40, 40);
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
                    colObj.Add(new Colours(Hashcolours[colourSelect].ToString(), id));
                    if (!(colourSelect >= 3)) { colourSelect++; }
                    else { colourSelect = 0; }
                    

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    grid.Children.Add(MyControl1);

                    count++;
                    id++;
                }

            }

            colours = colObj;
            return grid;

        }

        public void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
        }
    }

    class Board
    {
        private int colourCount;
        private List<int> randomPattern;
        private string difficulty;
        private int height;
        private int width;

        public Board(int colourCount, List<int> randomPattern, string difficulty, int height, int width)
        {
            this.colourCount = colourCount;
            this.randomPattern = randomPattern;
            this.difficulty = difficulty;
            this.height = height;
            this.width = width;
        }

        public int ColourCount { get => colourCount; set => colourCount = value; }
        public List<int> RandomPattern { get => randomPattern; set => randomPattern = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

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
        

        public Colours(string colour, int id)
        {
            this.colour = colour;
            this.id = id;
        }

        public string Colour { get => colour; set => colour = value; }
        public int Id { get => id; set => id = value; }
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
