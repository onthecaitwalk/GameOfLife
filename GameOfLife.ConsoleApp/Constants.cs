namespace GameOfLife.ConsoleApp
{
    public static class Constants
    {
        public static class Cell
        {
            public const string LiveSymbol = "x";
            public const string DeadSymbol = " ";
        }

        public static class UserMessage
        {
            public const string NumberOfGenerations = "Please enter the number of generations you would like to run: (example: 100)";
            public const string BoardHeight = "Please enter the height of the board you would like to create: (example: 10)";
            public const string BoardWidth = "Please enter the width of the board you would like to create: (example: 30)";
        }
    }
}
