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
            var (input, _) = Grid.GetExampleArray();

            var expectedCells = ExpectedCellsForExampleArray.Cells;

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
            var (array, input) = Grid.GetExampleArray();

            var result = new Grid(array, input);

            Assert.AreEqual(input, result.Threshold);
        }

        [TestMethod]
        public void Constructor_ExampleArray_ProperlyFindsSubregions()
        {
            var (exampleArray, threshold) = Grid.GetExampleArray();
            var expectedCellsForInput = ExpectedCellsForExampleArray.Cells;

            var expectedSubregions = new Dictionary<uint, Dictionary<Coordinate, Cell>>();

            var subregion1 = new Dictionary<Coordinate, Cell>
            {
                { new(1, 1), expectedCellsForInput[new(1, 1)] }
            };
            expectedSubregions.Add(0, subregion1);

            var subregion2 = new Dictionary<Coordinate, Cell>
            {
                { new(3, 3), expectedCellsForInput[new(3, 3)] },
                { new(3, 2), expectedCellsForInput[new(3, 2)] },
                { new(4, 2), expectedCellsForInput[new(4, 2)] },
                { new(4, 1), expectedCellsForInput[new(4, 1)] },
                { new(5, 1), expectedCellsForInput[new(5, 1)] }
            };
            expectedSubregions.Add(1, subregion2);

            var subregion3 = new Dictionary<Coordinate, Cell>
            {
                { new(5, 4), expectedCellsForInput[new Coordinate(5, 4)] },
                { new(5, 5), expectedCellsForInput[new Coordinate(5, 5)] }
            };
            expectedSubregions.Add(2, subregion3);

            var result = new Grid(exampleArray, threshold);

            Assert.IsTrue(result.Subregions.Count == 3);

            foreach (var expectedSubregion in expectedSubregions)
            {
                Assert.IsTrue(result.Subregions.ContainsKey(expectedSubregion.Key));

                Assert.AreEqual(expectedSubregion.Value.Count, result.Subregions[expectedSubregion.Key].Cells.Count);

                foreach (var expectedCell in expectedSubregion.Value.Values)
                {
                    Assert.IsTrue(result.Subregions[expectedSubregion.Key].Cells.ContainsKey(expectedCell.GridCoordinate)
                                  && result.Subregions[expectedSubregion.Key].Cells[expectedCell.GridCoordinate].Equals(expectedCell));
                }
            }
        }
    }
}
