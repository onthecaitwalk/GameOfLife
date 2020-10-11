using System;

namespace GameOfLife.ConsoleApp
{
    public class LifeSimulation : ILifeSimulation
    {
        private readonly IBoard _board;

        /// <summary>
        /// Initializes a new Game of Life.
        /// </summary>
        /// <param name="IBoard">Board with randomly generated life and dead cells.</param>
        public LifeSimulation(IBoard board)
        {
            _board = board;
        }

        /// <summary>
        /// Advances the game by one generation and prints the current state to console.
        /// </summary>
        public void Generate()
        {
            DrawGame();
            TransitionCells();
        }

        /// <summary>
        /// Transition the cells according to GoL's ruleset.
        /// </summary>
        internal void TransitionCells()
        {
            for (int i = 0; i < _board.Height; i++)
            {
                for (int j = 0; j < _board.Width; j++)
                {
                    var coordinate = new Coordinate { X = i, Y = j };
                    var cell = _board.GetCell(coordinate);
                    cell.Transition(NumberOfLiveNeighbours(coordinate));
                }
            }
        }

        /// <summary>
        /// Checks how many alive neighbours in the vicinity of a cell.
        /// </summary>
        /// <param name="coordinate">coordinate of the cell.</param>
        /// <returns>The number of live neighbours.</returns>
        internal int NumberOfLiveNeighbours(Coordinate coordinate)
        {
            int numberOfLiveNeighbours = 0;

            for (int i = coordinate.X - 1; i < coordinate.X + 2; i++)
            {
                for (int j = coordinate.Y - 1; j < coordinate.Y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= _board.Height || j >= _board.Width)))
                    {
                        if (!((i == coordinate.X) && (j == coordinate.Y)))
                        {
                            var cell = _board.GetCell(new Coordinate { X = i, Y = j });
                            if (cell.IsAlive) numberOfLiveNeighbours++;
                        }
                    }
                }
            }
            return numberOfLiveNeighbours;
        }

        /// <summary>
        /// Draws the game to the console.
        /// </summary>
        private void DrawGame()
        {
            for (int i = 0; i < _board.Height; i++)
            {
                for (int j = 0; j < _board.Width; j++)
                {
                    var coordinate = new Coordinate { X = i, Y = j };
                    var cell = _board.GetCell(coordinate);
                    Console.Write(cell.Symbol);
                    if (j == _board.Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }
    }
}
