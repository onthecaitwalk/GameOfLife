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
                    TransitionCell(coordinate, NumberOfLiveNeighbours(coordinate));
                }
            }
        }

        internal void TransitionCell(Coordinate coordinate, int numberOfLiveNeighbours)
        {
            if (ShouldCellTransitionToAlive(coordinate, numberOfLiveNeighbours))
            {
                _board.SetCellToDead(coordinate);
            }
            else if (ShouldCellTransitionToDead(coordinate, numberOfLiveNeighbours))
            {
                _board.SetCellToAlive(coordinate);
            }
        }

        internal bool ShouldCellTransitionToAlive(Coordinate coordinate, int numberOfLiveNeighbours)
        {
            return _board.IsCellAlive(coordinate) &&
                (numberOfLiveNeighbours < 2 || numberOfLiveNeighbours > 3);
        }

        internal bool ShouldCellTransitionToDead(Coordinate coordinate, int numberOfLiveNeighbours)
        {
            return !_board.IsCellAlive(coordinate) &&
                   numberOfLiveNeighbours == 3;
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
                            var currentCellCoordinate = new Coordinate { X = i, Y = j };
                            if (_board.IsCellAlive(currentCellCoordinate)) numberOfLiveNeighbours++;
                        }
                    }
                }
            }
            return numberOfLiveNeighbours;
        }

        internal string GetCellSymbol(Coordinate coordinate)
        {
            return _board.IsCellAlive(coordinate) ? Constants.Cell.LiveSymbol : Constants.Cell.DeadSymbol;
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
                    Console.Write(GetCellSymbol(coordinate));
                    if (j == _board.Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }
    }
}
