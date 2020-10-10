using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GameOfLife.UnitTests")]
namespace GameOfLife.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int runs = 0;
            var board = new Board
            {
                Height = GetDimension("height", "10"),
                Width = GetDimension("width", "30")
            };
            LifeSimulation sim = new LifeSimulation(board);

            while (runs++ < Constants.MaxRuns)
            {
                sim.Generate();

                // Give the user a chance to view the game in a more reasonable speed.
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadLine();
        }

        private static int GetDimension(string input, string example)
        {
            int dimension;
            string userInput;
            do
            {
                userInput = GetUserInput(input, example);
            } while (!int.TryParse(userInput, out dimension));

            Console.Clear();
            return dimension;
        }

        private static string GetUserInput(string input, string example)
        {
            Console.WriteLine($"\nPlease enter the {input} of the board you would like to create: (example: {example})");
            return Console.ReadLine().Trim();
        }
    }
}
