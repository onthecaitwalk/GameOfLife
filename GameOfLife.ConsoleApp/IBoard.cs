namespace GameOfLife.ConsoleApp
{
    public interface IBoard
    {
        int Height { get; set; }
        int Width { get; set; }
        Cell[,] Cells { get; }

        Cell GetCell(Coordinate coordinate);
    }
}
