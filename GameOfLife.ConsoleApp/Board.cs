using System;

namespace GameOfLife.ConsoleApp
{
    public class Board : IBoard
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool[,] Cells => GenerateCells();

        public bool IsCellAlive(Coordinate coordinate)
        {
            return Cells[coordinate.X, coordinate.Y] == true;
        }

        public void SetCellToDead(Coordinate coordinate)
        {
            Cells[coordinate.X, coordinate.Y] = false;
        }

        public void SetCellToAlive(Coordinate coordinate)
        {
            Cells[coordinate.X, coordinate.Y] = true;
        }

        /// <summary>
        /// Initializes the field with random boolean values.
        /// </summary>
        internal bool[,] GenerateCells()
        {
            var cells = new bool[Height, Width];
            Random generator = new Random();
            int number;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    number = generator.Next(2);
                    cells[i, j] = ((number == 0) ? false : true);
                }
            }

            return cells;
        }
    }
}
