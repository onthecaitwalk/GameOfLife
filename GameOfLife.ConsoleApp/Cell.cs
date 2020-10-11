namespace GameOfLife.ConsoleApp
{
    public class Cell
    {
        public bool IsAlive { get; set; }

        public string Symbol =>
            IsAlive ? Constants.Cell.LiveSymbol : Constants.Cell.DeadSymbol;

        public void Transition(int numberOfLiveNeighbours)
        {
            if (IsAlive &&
                (numberOfLiveNeighbours < 2 || numberOfLiveNeighbours > 3))
            {
                IsAlive = false;
            }
            else if (!IsAlive && numberOfLiveNeighbours == 3)
            {
                IsAlive = true;
            }
        }
    }
}
