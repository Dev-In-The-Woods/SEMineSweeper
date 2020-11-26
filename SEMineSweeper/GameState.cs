using SEMineSweeper.Enums;

namespace SEMineSweeper
{
    public class GameState
    {
        public GameState(BoardPosition position, int livesRemaining, bool mineFound, int moves, GameStatus status)
        {
            CurrentPosition = position;
            LivesRemaining = livesRemaining;
            MineFound = mineFound;
            Moves = moves;
            Status = status;
        }
        public BoardPosition CurrentPosition { get; private set; }
        public int LivesRemaining { get; private set; }
        public bool MineFound { get; private set; }
        public int Moves { get; private set; }
        public GameStatus Status { get; private set; }
    }
}
