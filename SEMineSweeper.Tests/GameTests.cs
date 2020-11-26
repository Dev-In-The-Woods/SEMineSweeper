using System;
using Xunit;
using SEMineSweeper;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using SEMineSweeper.Enums;

namespace SEMineSweeper.Tests
{
    public class GameTests
    {
        [Fact]
        public void Move_IfNotStarted_ThrowsException()
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(0, 0) == new BoardPosition[0]);
            var sut = new Game(12, moqMineFactory, 1, 0);

            // act 
            Action act = () => sut.Move(MoveDirection.Right);

            // assert
            act.Should().Throw<InvalidOperationException>().Where(e => e.Message == "Game must be started.");
        }


        [Theory]
        [InlineData(1, "A1")]
        [InlineData(4, "A4")]
        [InlineData(7, "A7")]
        [InlineData(11, "A11")]
        public void Start_ReturnsCorrectPosition(int startRow, string boardPosition)
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(0, 0) == new BoardPosition[0]);
            var sut = new Game(12, moqMineFactory, 1, 0);

            // act 
            var result = sut.Start(startRow);

            // assert
            result.CurrentPosition.ToString().Should().Be(boardPosition);
        }


        [Theory]
        [InlineData(6, 5)]
        [InlineData(0, 10)]
        [InlineData(11, 10)]
        public void Start_OutOfRangeStartRow_ThrowsException(int startRow, int gridSize)
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(0, 0) == new BoardPosition[0]);
            var sut = new Game(gridSize, moqMineFactory, 1, 0);

            // act 
            Action act = () => sut.Start(startRow);

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(new MoveDirection[] { MoveDirection.Right, MoveDirection.Right, MoveDirection.Right, MoveDirection.Right }, 10, 1, "E1")]
        [InlineData(new MoveDirection[] { MoveDirection.Right, MoveDirection.Right, MoveDirection.Right, MoveDirection.Right, MoveDirection.Down }, 10, 1, "E2")]
        [InlineData(new MoveDirection[] { MoveDirection.Right, MoveDirection.Up, MoveDirection.Right, MoveDirection.Up, MoveDirection.Left }, 10, 10, "B8")]
        public void Move_MultipleMoves_ReturnCorrectPosition(IEnumerable<MoveDirection> moves, int gridSize, int startRow, string finalBoardPosition)
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(0, 0) == new BoardPosition[0]);
            var sut = new Game(gridSize, moqMineFactory, 1, 0);

            // act 
            var result = sut.Start(startRow);
            foreach(var move in moves)
            {
                result = sut.Move(move);
            }

            // assert
            result.CurrentPosition.ToString().Should().Be(finalBoardPosition);
        }

        [Fact]
        public void Move_HitMine_DecrementsLives()
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(3, 2) == new BoardPosition[] { new BoardPosition('A', 1), new BoardPosition('B', 1) });
            var sut = new Game(3, moqMineFactory, 3, 2);

            // act 
            sut.Start(1);
            var result = sut.Move(MoveDirection.Right);

            // assert
            result.LivesRemaining.Should().Be(1);
            result.Status.Should().Be(GameStatus.InProgress);
        }

        [Fact]
        public void Move_LivesRemainingReachesZero_GameStatusIsLost()
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(3, 1) == new BoardPosition[] { new BoardPosition('A', 1) });
            var sut = new Game(3, moqMineFactory, 1, 1);

            // act 
            var result = sut.Start(1);

            // assert
            result.Status.Should().Be(GameStatus.Lost);
        }

        [Fact]
        public void Move_ReachRightmostBoardColumn_GameStatusIsWon()
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(0, 0) == new BoardPosition[0]);
            var sut = new Game(3, moqMineFactory, 1, 0);

            // act 
            sut.Start(1);
            sut.Move(MoveDirection.Right);
            var result = sut.Move(MoveDirection.Right);

            // assert
            result.Status.Should().Be(GameStatus.Won);
        }

        [Fact]
        public void Move_HitMineInRightmostBoardColumnOnLastLife_GameStatusIsLost()
        {
            // arrange
            var moqMineFactory = Mock.Of<Interfaces.IMineFactory>(mf => mf.GenerateMines(3, 1) == new BoardPosition[] { new BoardPosition('C', 1) });
            var sut = new Game(3, moqMineFactory, 1, 1);

            // act 
            sut.Start(1);
            sut.Move(MoveDirection.Right);
            var result = sut.Move(MoveDirection.Right);

            // assert
            result.Status.Should().Be(GameStatus.Lost);
            result.LivesRemaining.Should().Be(0);
        }

    }
}
