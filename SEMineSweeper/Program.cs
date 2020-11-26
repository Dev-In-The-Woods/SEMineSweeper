using SEMineSweeper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEMineSweeper
{
    class Program
    {
        // Setting up some default game parameters. Perhaps gridsize should be set? Perhaps number of mines should be calculated based on grid size?
        const int gridSize = 12;
        const int numberOfLives = 3;
        const int numberOfMines = 4;

        static Game game;

        static void Main()
        {
            Start();
        }

        static void Start()
        {
            game = new Game(gridSize, new RandomMineFactory(), numberOfLives, numberOfMines);

            Play(game.Start(GetValidInput(1, gridSize, $"Input your starting row, from 1 to {gridSize}:")));
        }

        static void Play(GameState gameState)
        {
            Console.Clear();

            if (gameState.MineFound) Console.WriteLine("BOOM! Mine hit!");

            Console.WriteLine($"Current position: {gameState.CurrentPosition}\nLives left: {gameState.LivesRemaining}\nMoves: {gameState.Moves}");

            switch (gameState.Status)
            {
                case GameStatus.Lost:
                    ShowLost();
                    break;
                case GameStatus.Won:
                    ShowWon();
                    break;
                default:
                    ShowInProgress();
                    break;
            }
        }

        static void ShowInProgress()
        {
            bool invalidMove;
            do
            {
                try
                {

                    switch (char.ToLower(GetValidInput(new char[] {'u','d','l','r','q','s' }, "\n\nPress a key to move: (U)p (D)own (L)eft (R)ight\nAlternatively, (Q)uit or (S)tart again")))
                    {
                        case 'u':
                            Play(game.Move(MoveDirection.Up));
                            break;
                        case 'd':
                            Play(game.Move(MoveDirection.Down));
                            break;
                        case 'l':
                            Play(game.Move(MoveDirection.Left));
                            break;
                        case 'r':
                            Play(game.Move(MoveDirection.Right));
                            break;
                        case 'q':
                            Environment.Exit(0);
                            break;
                        case 's':
                            Start();
                            break;
                    }
                    invalidMove = false;
                }
                catch (InvalidOperationException ex)
                {
                    invalidMove = true;
                    Console.WriteLine(ex.Message);
                }
            } while (invalidMove);
        }

        static void ShowLost()
        {
            Console.WriteLine("Sadly you have lost :(");
            RestartOrQuit();
        }

        static void ShowWon()
        {
            Console.WriteLine("Yay, you beat the game :)");
            RestartOrQuit();
        }

        static void RestartOrQuit()
        {
            switch (char.ToLower(GetValidInput(new char[] { 'q', 's' }, "\nPress, (Q)uit or (S)tart again")))
            {
                case 'q':
                    Environment.Exit(0);
                    break;
                case 's':
                    Start();
                    break;
            }
        }

        static char GetValidInput(IEnumerable<char> expectedInputs, string message)
        {
            var input = "";
            while (string.IsNullOrWhiteSpace(input) || !expectedInputs.Any(i => char.ToLower(i) == char.ToLower(input[0])))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            return input[0];
        }

        static int GetValidInput(int min, int max, string message)
        {
            var inputString = "";
            int inputInt;
            while (!int.TryParse(inputString, out inputInt) || inputInt < min || inputInt > max)
            {
                Console.WriteLine(message);
                inputString = Console.ReadLine();
            }
            return inputInt;
        }
    }
}
