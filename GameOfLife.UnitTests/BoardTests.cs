using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using GameOfLife.ConsoleApp;
using NUnit.Framework;

namespace GameOfLife.UnitTests
{
    [TestFixture]
    public class BoardTests
    {

        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void HeightIsZero_CellsEmpty()
        {
            var board = new Board
            {
                Height = 0,
                Width = _fixture.Create<int>()
            };

            Assert.IsEmpty(board.Cells, "Should not generate cells if height is zero.");
        }

        [Test]
        public void WidthIsZero_CellsEmpty()
        {
            var board = new Board
            {
                Height = _fixture.Create<int>(),
                Width = 0
            };

            Assert.IsEmpty(board.Cells, "Should not generate cells if width is zero.");
        }

        [Test]
        public void HeightAndWidthAreGreaterThanZero_ReturnsCells_HeightIsFirstDimension_WidthIsSecondDimension()
        {
            var board = new Board
            {
                Height = 2,
                Width = 3
            };

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, board.Cells.GetLength(0), "Should set first dimension of cells to height.");
                Assert.AreEqual(3, board.Cells.GetLength(1), "Should set second dimension of cells to width.");
                Assert.AreEqual(6, board.Cells.Length, "Should set total cells to height * width.");
            });
        }

        [Test]
        public void GenerateCells_HeightAndWidthAreGreaterThanZero_ReturnsCells_HeightIsFirstDimension_WidthIsSecondDimension_PopulatedWithCells()
        {
            var board = new Board
            {
                Height = 2,
                Width = 3
            };

            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<Cell>(board.Cells[0, 0], "Should populate with instance of cell.");
                Assert.IsInstanceOf<Cell>(board.Cells[0, 1], "Should populate with instance of cell.");
                Assert.IsInstanceOf<Cell>(board.Cells[0, 2], "Should populate with instance of cell.");
                Assert.IsInstanceOf<Cell>(board.Cells[1, 0], "Should populate with instance of cell.");
                Assert.IsInstanceOf<Cell>(board.Cells[1, 1], "Should populate with instance of cell.");
                Assert.IsInstanceOf<Cell>(board.Cells[1, 2], "Should populate with instance of cell.");
            });
        }
    }
}
