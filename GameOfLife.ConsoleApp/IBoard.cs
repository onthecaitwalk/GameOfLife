using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.ConsoleApp
{
    public interface IBoard
    {
        int Height { get; set; }
        int Width { get; set; }
        bool[,] Cells { get; }

        bool IsCellAlive(Coordinate coordinate);
        void SetCellToDead(Coordinate coordinate);
        void SetCellToAlive(Coordinate coordinate);
    }
}
