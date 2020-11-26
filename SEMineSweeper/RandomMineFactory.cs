using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SEMineSweeper.Interfaces;

namespace SEMineSweeper
{
    public class RandomMineFactory : IMineFactory
    {
        readonly Random random = new Random();

        public IEnumerable<BoardPosition> GenerateMines(int gridSize, int numberOfMines)
        {
            if (numberOfMines > gridSize * gridSize) throw new ArgumentOutOfRangeException("Number of mines to generate out of range.");
            var mines = new List<BoardPosition>();
            while (mines.Count < numberOfMines) {
                var col = random.Next(0, gridSize);
                var row = random.Next(0, gridSize);

                if(!mines.Any(m => m.GetZeroBasedPositions() == (col, row)))
                {
                    mines.Add(new BoardPosition(col, row));
                }
            }
            return mines;
        }
    }
}
