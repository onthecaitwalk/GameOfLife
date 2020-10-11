using GameOfLife.ConsoleApp;
using NUnit.Framework;

namespace GameOfLife.UnitTests
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void Symbol_CellIsAlive_ReturnsLiveCellSymbol()
        {
            var cell = new Cell { IsAlive = true};

            Assert.AreEqual(Constants.Cell.LiveSymbol, cell.Symbol, "Should return live cell symbol.");
        }

        [Test]
        public void Symbol_CellIsDead_ReturnsDeadCellSymbol()
        {
            var cell = new Cell { IsAlive = false };

            Assert.AreEqual(Constants.Cell.DeadSymbol, cell.Symbol, "Should return dead cell symbol.");
        }

        [Test]
        public void Transition_WhenCellIsAlive_NumberOfLiveNeighboursIsLessThan2_CellDies()
        { 
            var cell = new Cell { IsAlive = true };

            cell.Transition(1);

            Assert.IsFalse(cell.IsAlive, "Should kill a live cell with less than 2 live neighbours.");
        }

        [Test]
        public void Transition_WhenCellIsAlive_NumberOfLiveNeighboursIsGreaterThan3_CellDies()
        {
            var cell = new Cell { IsAlive = true };

            cell.Transition(4);

            Assert.IsFalse(cell.IsAlive, "Should kill a live cell with more than 3 live neighbours.");
        }
        
        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void Transition_WhenCellIsAlive_NumberOfLiveNeighboursEquals2or3_CellStayAlive(int numberOfLiveNeighbours)
        {
            var cell = new Cell { IsAlive = true };

            cell.Transition(numberOfLiveNeighbours);

            Assert.IsTrue(cell.IsAlive, "Should not transition a live cell with 2 or 3 live neighbours.");
        }
        
        [Test]
        public void Transition_WhenCellIsDead_NumberOfLiveNeighboursEquals3_CellComesAlive()
        {
            var cell = new Cell { IsAlive = false };

            cell.Transition(3);

            Assert.IsTrue(cell.IsAlive, "Should transition dead cell with 3 live neighbours to alive.");
        }

        [Test]
        public void Transition_WhenCellIsDead_NumberOfLiveNeighboursIsLessThan3_CellStaysDead()
        {
            var cell = new Cell { IsAlive = false };

            cell.Transition(2);

            Assert.IsFalse(cell.IsAlive, "Should not transition dead cell with less than 3 live neighbours to alive.");
        }

        [Test]
        public void Transition_WhenCellIsDead_NumberOfLiveNeighboursIsGreaterThan3_CellStaysDead()
        {
            var cell = new Cell { IsAlive = false };

            cell.Transition(4);

            Assert.IsFalse(cell.IsAlive, "Should not transition dead cell with more than 3 live neighbours to alive.");
        }
    }
}
