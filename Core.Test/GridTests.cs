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

            var ex = Assert.ThrowsException<ArgumentException>(() => _ = new Grid(input, -1));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void Constructor_ArrayDimensionLengthsDoNotMatch_ThrowsArgumentException()
        {
            var input = new int[6, 1];
            var expectedMessage = "sourceArray dimension lengths must match.";

            var ex = Assert.ThrowsException<ArgumentException>(() => _ = new Grid(input, -1));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestMethod]
        public void Constructor_ValidInput_ResultsInValidCells()
        {
            int[,] input = GetSampleArray();

            var expectedCells = GetExpectedCellsForSampleArray();

            var results = new Grid(input, -1);

            Assert.AreEqual(expectedCells.Count, results.Cells.Count);

            foreach (var cell in expectedCells)
            {
                Assert.IsTrue(results.Cells.ContainsValue(cell.Value));
            }
        }

        [TestMethod]
        public void Constructor_ValidInput_ResultsInValidThreshold()
        {
            var input = 200;
            var array = GetSampleArray();

            var result = new Grid(array, input);

            Assert.AreEqual(input, result.Threshold);
        }

        [TestMethod]
        public void Constructor_ValidInput_ProperlyFindsSubregions()
        {
            var input = GetSampleArray();
            var threshold = 200;
            var expectedCellsForInput = GetExpectedCellsForSampleArray();

            var expectedSubregions = new Dictionary<uint, Subregion>();

            var subregion1 = new Subregion(0);
            subregion1.Cells.Add(new(1, 1), expectedCellsForInput[new(1, 1)]);
            expectedSubregions.Add(subregion1.Id, subregion1);

            var subregion2 = new Subregion(1);
            subregion2.Cells.Add(new(3, 3), expectedCellsForInput[new(3, 3)]);
            subregion2.Cells.Add(new(3, 2), expectedCellsForInput[new(3, 2)]);
            subregion2.Cells.Add(new(4, 2), expectedCellsForInput[new(4, 2)]);
            subregion2.Cells.Add(new(4, 1), expectedCellsForInput[new(4, 1)]);
            subregion2.Cells.Add(new(5, 1), expectedCellsForInput[new(5, 1)]);
            expectedSubregions.Add(subregion2.Id, subregion2);

            var subregion3 = new Subregion(2);
            subregion3.Cells.Add(new(5, 4), expectedCellsForInput[new Coordinate(5, 4)]);
            subregion3.Cells.Add(new(5, 5), expectedCellsForInput[new Coordinate(5, 5)]);
            expectedSubregions.Add(subregion3.Id, subregion3);

            var result = new Grid(input, threshold);

            Assert.IsTrue(result.Subregions.Count == 3);

            foreach (var expectedSubregion in expectedSubregions)
            {
                Assert.IsTrue(result.Subregions.ContainsKey(expectedSubregion.Key) && result.Subregions[expectedSubregion.Key] == expectedSubregion.Value);

                Assert.AreEqual(expectedSubregion.Value.Cells.Count, result.Subregions[expectedSubregion.Key].Cells.Count);

                foreach (var expectedCell in expectedSubregion.Value.Cells)
                {
                    Assert.IsTrue(result.Subregions[expectedSubregion.Key].Cells.ContainsKey(expectedCell.Key) && result.Subregions[expectedSubregion.Key].Cells[expectedCell.Key].Equals(expectedCell.Value));
                }
            }
        }

        private static int[,] GetSampleArray()
        {
            int[,] input = new int[6, 6]
            {
                { 0, 115, 5, 15, 0, 5 },
                { 80, 210, 0, 5, 5, 0 },
                { 45, 60, 145, 175, 95, 25 },
                { 95, 5, 250, 250, 115, 5 },
                { 170, 230, 245, 185, 165, 145 },
                { 145, 220, 140, 160, 250, 250 }
            };
            return input;
        }

        private static Dictionary<Coordinate, Cell> GetExpectedCellsForSampleArray()
        {
            Dictionary<Coordinate, Cell> expectedCells = [];

            expectedCells.Add(new(0, 0), new(new(0, 0), 0));
            expectedCells.Add(new(0, 1), new(new(0, 1), 115));
            expectedCells.Add(new(0, 2), new(new(0, 2), 5));
            expectedCells.Add(new(0, 3), new(new(0, 3), 15));
            expectedCells.Add(new(0, 4), new(new(0, 4), 0));
            expectedCells.Add(new(0, 5), new(new(0, 5), 5));
            expectedCells.Add(new(1, 0), new(new(1, 0), 80));
            expectedCells.Add(new(1, 1), new(new(1, 1), 210));
            expectedCells.Add(new(1, 2), new(new(1, 2), 0));
            expectedCells.Add(new(1, 3), new(new(1, 3), 5));
            expectedCells.Add(new(1, 4), new(new(1, 4), 5));
            expectedCells.Add(new(1, 5), new(new(1, 5), 0));
            expectedCells.Add(new(2, 0), new(new(2, 0), 45));
            expectedCells.Add(new(2, 1), new(new(2, 1), 60));
            expectedCells.Add(new(2, 2), new(new(2, 2), 145));
            expectedCells.Add(new(2, 3), new(new(2, 3), 175));
            expectedCells.Add(new(2, 4), new(new(2, 4), 95));
            expectedCells.Add(new(2, 5), new(new(2, 5), 25));
            expectedCells.Add(new(3, 0), new(new(3, 0), 95));
            expectedCells.Add(new(3, 1), new(new(3, 1), 5));
            expectedCells.Add(new(3, 2), new(new(3, 2), 250));
            expectedCells.Add(new(3, 3), new(new(3, 3), 250));
            expectedCells.Add(new(3, 4), new(new(3, 4), 115));
            expectedCells.Add(new(3, 5), new(new(3, 5), 5));
            expectedCells.Add(new(4, 0), new(new(4, 0), 170));
            expectedCells.Add(new(4, 1), new(new(4, 1), 230));
            expectedCells.Add(new(4, 2), new(new(4, 2), 245));
            expectedCells.Add(new(4, 3), new(new(4, 3), 185));
            expectedCells.Add(new(4, 4), new(new(4, 4), 165));
            expectedCells.Add(new(4, 5), new(new(4, 5), 145));
            expectedCells.Add(new(5, 0), new(new(5, 0), 145));
            expectedCells.Add(new(5, 1), new(new(5, 1), 220));
            expectedCells.Add(new(5, 2), new(new(5, 2), 140));
            expectedCells.Add(new(5, 3), new(new(5, 3), 160));
            expectedCells.Add(new(5, 4), new(new(5, 4), 250));
            expectedCells.Add(new(5, 5), new(new(5, 5), 250));

            return expectedCells;
        }
    }
}
