using System;

namespace GameOfLife.ConsoleApp
{
    public class Board : IBoard
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Cell[,] Cells => GenerateCells();

        public Cell GetCell(Coordinate coordinate)
        {
            return Cells[coordinate.X, coordinate.Y];
        }

        /// <summary>
        /// Initializes the field with random boolean values.
        /// </summary>
        internal Cell[,] GenerateCells()
        {
            var cells = new Cell[Height, Width];
            Random generator = new Random();
            int number;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    number = generator.Next(2);
                    var cell = new Cell 
                    { 
                        IsAlive = (number == 0) ? false : true 
                    };
                    cells[i, j] = cell;
                }
            }

            return cells;
        }
    }
}
