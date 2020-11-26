using SEMineSweeper.Enums;
using SEMineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEMineSweeper
{
    public class Game
    {
        readonly IMineFactory _mineFactory;
        readonly int _gridSize;
        private IEnumerable<BoardPosition> _mines;
        readonly int _numberOfMines;
        
        int _livesRemaining;
        BoardPosition _currentPosition = null;
        int _totalMoves = 0;

        public Game(int gridSize, IMineFactory mineFactory, int numberOfLives, int numberOfMines)
        {
            _gridSize = gridSize;
            _mineFactory = mineFactory;
            _livesRemaining = numberOfLives;
            _numberOfMines = numberOfMines;
        }

        public GameState Start(int row)
        {
            if (row < 1 || row > _gridSize) throw new ArgumentOutOfRangeException($"Start row should be between 1 and {_gridSize}.");

            _mines = _mineFactory.GenerateMines(_gridSize, _numberOfMines);

            _currentPosition = new BoardPosition('A', row);
            return UpdateGameState(_currentPosition);
        }

        public GameState Move(MoveDirection direction)
        {
            if (_currentPosition == null) throw new InvalidOperationException("Game must be started.");

            if (_livesRemaining < 1 || _currentPosition.GetZeroBasedPositions().Column == _gridSize) throw new InvalidOperationException("Cannot move, game has ended.");

            var (Column, Row) = _currentPosition.GetZeroBasedPositions();
            switch (direction)
            {
                case MoveDirection.Down:
                    Row++;
                    if (Row > _gridSize) throw new InvalidOperationException("Attempt to move out of bounds.");
                    break;
                case MoveDirection.Left:
                    Column--;
                    if (Column < 0) throw new InvalidOperationException("Attempt to move out of bounds.");
                    break;
                case MoveDirection.Right:
                    Column++;
                    if (Column > _gridSize) throw new InvalidOperationException("Attempt to move out of bounds.");
                    break;
                case MoveDirection.Up:
                    Row--;
                    if (Row < 0) throw new InvalidOperationException("Attempt to move out of bounds.");
                    break;
            }
            return UpdateGameState(new BoardPosition(Column, Row));
        }

        private GameState UpdateGameState(BoardPosition newPosition)
        {
            _currentPosition = newPosition;
            _totalMoves++;
            var hitMine = CheckForMine(newPosition);
            if (hitMine) _livesRemaining--;
            var status = _livesRemaining == 0 ? GameStatus.Lost : 
                (_currentPosition.GetZeroBasedPositions().Column == _gridSize - 1 ? GameStatus.Won : 
                GameStatus.InProgress);
            
            return new GameState (newPosition, _livesRemaining, hitMine, _totalMoves, status);
        }

        private bool CheckForMine(BoardPosition position)
        {
            return _mines.Any(m => m.Row == position.Row && m.Column == position.Column);
        }
    }
}
