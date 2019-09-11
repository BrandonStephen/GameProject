using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    class Leaderboard
    {
        private string name;
        private int score;
        private int wave;
        private string difficulty;
        private int gridSize;

        public Leaderboard(string name, int score, int wave, string difficulty, int gridSize)
        {
            this.name = name;
            this.score = score;
            this.wave = wave;
            this.difficulty = difficulty;
            this.gridSize = gridSize;
        }

        public string Name { get => name; set => name = value; }
        public int Score { get => score; set => score = value; }
        public int Wave { get => wave; set => wave = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public int GridSize { get => gridSize; set => gridSize = value; }
    }
}
