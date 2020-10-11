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
            var numberOfGenerations = GetUserInput(Constants.UserMessage.NumberOfGenerations);
            var board = new Board
            {
                Height = GetUserInput(Constants.UserMessage.BoardHeight),
                Width = GetUserInput(Constants.UserMessage.BoardWidth)
            };
            LifeSimulation lifeSimulation = new LifeSimulation(board);
                       
            while (runs++ < numberOfGenerations)
            {
                lifeSimulation.Generate();

                // Give the user a chance to view the game in a more reasonable speed.
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadLine();
        }

        private static int GetUserInput(string userMessage)
        {
            int parsedInput;
            string userInput;
            do
            {
                Console.WriteLine(userMessage);
                userInput = Console.ReadLine().Trim();
            } while (!int.TryParse(userInput, out parsedInput));

            Console.Clear();
            return parsedInput;
        }
    }
}
