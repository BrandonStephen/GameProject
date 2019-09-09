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
            Grid grid = gamegrid;
            game.initialise(grid);
        }
    }

    class Game
    {
        public void initialise(Grid grid)
        {
            Random rn = new Random();
            List<int> pattern = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                pattern.Add(rn.Next(1, 17));
            }
            Board board = new Board(16, pattern, "easy");
            start(grid, board);
        }

        public void start(Grid grid, Board board)
        {
            int count = 1;

            List<Brush> colours = new List<Brush>();
            colours.Add(Brushes.Red);
            colours.Add(Brushes.Yellow);
            colours.Add(Brushes.Blue);
            colours.Add(Brushes.Green);
            int colourSelect = 0;
            int id = 0;


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button MyControl1 = new Button();
                    MyControl1.Name = "Button"+id.ToString();
                    MyControl1.Click += Button_Click;
                    MyControl1.Background = colours[colourSelect];
                    Colours col = new Colours(colours[colourSelect].ToString(), id);
                    if (!(colourSelect >= 3)) { colourSelect++; }
                    else { colourSelect = 0; }


                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    grid.Children.Add(MyControl1);

                    count++;
                    id++;
                }

            }


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

        public Board(int colourCount, List<int> randomPattern, string difficulty)
        {
            this.colourCount = colourCount;
            this.randomPattern = randomPattern;
            this.difficulty = difficulty;
        }

        public int ColourCount { get => colourCount; set => colourCount = value; }
        public List<int> RandomPattern { get => randomPattern; set => randomPattern = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }

        public void createGrids(int size)
        {
            Grid gamegrid = new Grid();
            gamegrid.Width = 400;
            gamegrid.HorizontalAlignment = HorizontalAlignment.Left;
            gamegrid.VerticalAlignment = VerticalAlignment.Top;
            gamegrid.ShowGridLines = true;

            List<ColumnDefinition> gridCol = new List<ColumnDefinition>();
            List<RowDefinition> gridRow = new List<RowDefinition>();

            for (int i = 0; i < Math.Sqrt(size); i++)
            {
                gridCol.Add(new ColumnDefinition());
                gridRow.Add(new RowDefinition());
            }

            foreach (ColumnDefinition cd in gridCol) gamegrid.ColumnDefinitions.Add(cd);
            foreach (RowDefinition rd in gridRow) gamegrid.RowDefinitions.Add(rd);
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
