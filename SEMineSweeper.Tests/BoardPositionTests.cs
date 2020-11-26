using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace SEMineSweeper.Tests
{
    public class BoardPositionTests
    {
        [Theory]
        [InlineData(0, 0, 'A', 1)]
        [InlineData(1, 4, 'B', 5)]
        [InlineData(2, 2, 'C', 3)]
        [InlineData(3, 0, 'D', 1)]
        [InlineData(0, 5, 'A', 6)]
        public void InstantiatedWithZeroBasedPosition_ReturnsCorrectGridPosition(int zeroBasedColumn, int zeroBasedRow, char chessColumn, int chessRow)
        {
            // arrange
            var sut = new BoardPosition(zeroBasedColumn, zeroBasedRow);

            // act

            // assert
            sut.Column.Should().Be(chessColumn);
            sut.Row.Should().Be(chessRow);
        }

        [Theory]
        [InlineData('A', 1, 0, 0)]
        [InlineData('B', 5, 1, 4)]
        [InlineData('C', 3, 2, 2)]
        [InlineData('D', 1, 3, 0)]
        [InlineData('A', 6, 0, 5)]
        public void InstantiatedWithChessBoardPosition_ReturnsCorrectZeroBasedPosition(char chessColumn, int chessRow, int zeroBasedColumn, int zeroBasedRow)
        {
            // arrange
            var sut = new BoardPosition(chessColumn, chessRow);

            // act
            var (Column, Row) = sut.GetZeroBasedPositions();

            // assert
            Column.Should().Be(zeroBasedColumn);
            Row.Should().Be(zeroBasedRow);
        }
    }
}
