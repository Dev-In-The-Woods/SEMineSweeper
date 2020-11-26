using System;
using System.Collections.Generic;
using System.Text;

namespace SEMineSweeper
{
    public class BoardPosition
    {
        public char Column { get; private set; }
        public int Row { get; private set; }

        public BoardPosition(char column, int row)
        {
            if (ConvertCharToColumnInt(column) > 26 || ConvertCharToColumnInt(column) < 1) throw new ArgumentOutOfRangeException("Maximum 26 columns supported, A through Z.");

            Column = column;
            Row = row;
        }

        public BoardPosition(int zeroBasedColumnIndex, int zeroBasedRowIndex) : this((char)('A' + (zeroBasedColumnIndex)), zeroBasedRowIndex + 1)
        {
        }

        public (int Column, int Row) GetZeroBasedPositions()
        {
            return (Column: ConvertCharToColumnInt(Column) - 1, Row - 1);
        }

        private int ConvertCharToColumnInt(char column)
        {
            return (Column % 32);   // returns 1 for a, 2 for b, etc
        }

        public override string ToString()
        {
            return $"{Column}{Row}";
        }
    }
}
