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
            var (input, threshold) = Grid.GetExampleArray();

            var result = new Grid(input, threshold);

            Assert.IsTrue(result.Subregions.Count == 3);
        }
    }
}
