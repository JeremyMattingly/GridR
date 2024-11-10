using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivTrak.Assessment.GridR.Core.Test
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void Constructor_EmptySourceArray_ThrowsArgumentException()
        {
            var input = new int[0, 0];
            var expectedMessage = "sourceArray cannot be empty.";

            var ex = Assert.ThrowsException<ArgumentException>(() => _ = new Grid(input));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void Constructor_ArrayDimensionLengthsDoNotMatch_ThrowsArgumentException()
        {
            var input = new int[6, 1];
            var expectedMessage = "sourceArray dimension lengths must match.";

            var ex = Assert.ThrowsException<ArgumentException>(() => _ = new Grid(input));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void Constructor_ValidInput_ResultsInValidCells()
        {
            int[,] input = new int[2, 2]
            {
                {1, 2},
                {3, 4}
                //{ 5, 0, 15, 5, 115, 0 },
                //{ 0, 5, 5, 0, 210, 80 },
                //{ 25, 95, 175, 145, 60, 45 },
                //{ 5, 115, 250, 250, 5, 95 },
                //{ 145, 165, 185, 245, 230, 170 },
                //{ 250, 250, 160, 140, 220, 145 }
            };

            var cell1 = new Cell(new(0, 0), 1);
            var cell2 = new Cell(new(0, 1), 2);
            var cell3 = new Cell(new(1, 0), 3);
            var cell4 = new Cell(new(1, 1), 4);

            var results = new Grid(input);

            Assert.IsTrue(results.Cells.Contains(cell1));
            Assert.IsTrue(results.Cells.Contains(cell2));
            Assert.IsTrue(results.Cells.Contains(cell3));
            Assert.IsTrue(results.Cells.Contains(cell4));
            
        }
    }
}
