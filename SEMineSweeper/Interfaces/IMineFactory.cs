using System;
using System.Collections.Generic;
using System.Text;

namespace SEMineSweeper.Interfaces
{
    public interface IMineFactory
    {
        IEnumerable<BoardPosition> GenerateMines(int gridSize, int numberOfMines);
    }
}
