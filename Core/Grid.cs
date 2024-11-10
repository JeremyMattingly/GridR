namespace ActivTrak.Assessment.GridR.Core
{
    public class Grid
    {
        private readonly int[,] _sourceArray;
        private readonly Coordinate _maxCoordinate;

        public Dictionary<Coordinate, Cell> Cells { get; } = [];
        public int Threshold { get; }
        public Dictionary<uint, Subregion> Subregions { get; } = [];

        public Grid(int[,] sourceArray, int threshold)
        {
            if (sourceArray.Length == 0) throw new ArgumentException($"{nameof(sourceArray)} cannot be empty.");
            if (sourceArray.GetLength(0) != sourceArray.GetLength(1)) throw new ArgumentException($"{nameof(sourceArray)} dimension lengths must match.");

            _sourceArray = sourceArray;

            _maxCoordinate = new Coordinate((uint)_sourceArray.GetLength(0), (uint)_sourceArray.GetLength(1));

            Threshold = threshold;

            LoadCells();

            FindSubRegions();
        }

        private void LoadCells()
        {
            for (uint x = 0; x < _sourceArray.GetLength(0); x++)
            {
                for (uint y = 0; y < _sourceArray.GetLength(1); y++)
                {
                    Coordinate coordinate = new(x, y);
                    Cells.Add(coordinate, new Cell(coordinate, _sourceArray[x, y]));
                }
            }
        }

        private void FindSubRegions()
        {
            Dictionary<Coordinate, Cell> interestingCells = GetInterestingCells();

            foreach (var interestingCell in interestingCells)
            {
                if (Subregions.Count == 0)
                {
                    CreatFirstSubRegionAndAddCellToIt(interestingCell);
                }
                else
                {
                    DetermineTargetSubregionAndAddCellToIt(interestingCells, interestingCell);
                }
            }
        }

        private Dictionary<Coordinate, Cell> GetInterestingCells()
        {
            Dictionary<Coordinate, Cell> interestingCells = [];

            foreach (var cell in Cells)
            {
                if (cell.Value.Signal >= Threshold)
                {
                    interestingCells.Add(cell.Key, cell.Value);
                }
            }

            return interestingCells;
        }

        private void CreatFirstSubRegionAndAddCellToIt(KeyValuePair<Coordinate, Cell> interestingCell)
        {
            Subregions.Add(0, new Subregion(0));
            Subregions[0].Cells.Add(interestingCell.Key, interestingCell.Value);
        }

        private void DetermineTargetSubregionAndAddCellToIt(Dictionary<Coordinate, Cell> interestingCells, KeyValuePair<Coordinate, Cell> interestingCell)
        {
            //See if a neighbor is accounted for
            foreach (var neighbor in GetCellNeighbors(interestingCell.Value))
            {
                if (interestingCells.ContainsKey(neighbor))
                {
                    //See if the interesting neighbor is accounted for in a subregion yet
                    var neighborsSubregion = CellFoundInSubregion(neighbor);
                    if (neighborsSubregion is not null)
                    {
                        // add interesting cell to region containing neighbor
                        neighborsSubregion.Cells.Add(interestingCell.Key, interestingCell.Value);
                        break;
                    }
                    else
                    {
                        //Look for neighbor's neighbors. if one of those are accounted for in regions, use that region
                        var neighborsNeighbors = GetCellNeighbors(Cells[neighbor]);
                        var cellAddedViaNeighborsNeighbor = false;
                        foreach (var neighborsNeighbor in neighborsNeighbors)
                        {
                            if (neighborsNeighbor != interestingCell.Key) //Don't check yourself - endless loop
                            {
                                if (interestingCells.ContainsKey(neighborsNeighbor))
                                {
                                    //neighbor's neighbor is interesting
                                    var neighborsNeighborsSubregion = CellFoundInSubregion(neighborsNeighbor);
                                    if (neighborsNeighborsSubregion is not null)
                                    {
                                        //neighbor's neighbor is accounted for in a region so add interesting cell to that neighbor's neighbor's subregion
                                        neighborsNeighborsSubregion.Cells.Add(interestingCell.Key, interestingCell.Value);
                                        cellAddedViaNeighborsNeighbor = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (cellAddedViaNeighborsNeighbor)
                        {
                            //cell has been added, so stop everything and move on to the next interesting cell
                            break;
                        }
                        else
                        {
                            // at this point, no neighbor's neighbors that weren't yourself were interesting or if interesting were already in a subregion. So create the new subregion and interesting cell to it
                            AddCellToNewSubregion(interestingCell);
                            break;
                        }
                    }
                }
            }
        }

        private List<Coordinate> GetCellNeighbors(Cell cell)
        {
            List<Coordinate> neighbors = [];

            //topMiddle
            if (cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X, cell.GridCoordinate.Y + 1));
            }

            //topRight
            if (cell.GridCoordinate.X < _maxCoordinate.X && cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y + 1));
            }

            //right
            if (cell.GridCoordinate.X < _maxCoordinate.X)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y));
            }

            //bottomRight
            if (cell.GridCoordinate.Y > 0 && cell.GridCoordinate.X < _maxCoordinate.X)
            {
                neighbors.Add(new(cell.GridCoordinate.X + 1, cell.GridCoordinate.Y - 1));
            }

            //bottomMiddle
            if (cell.GridCoordinate.Y > 0)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X, cell.GridCoordinate.Y - 1));
            }

            //bottomLeft
            if (cell.GridCoordinate.X > 0 && cell.GridCoordinate.Y > 0)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y - 1));
            }

            //left
            if (cell.GridCoordinate.X > 0)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y));
            }

            //topLeft
            if (cell.GridCoordinate.X > 0 && cell.GridCoordinate.Y < _maxCoordinate.Y)
            {
                neighbors.Add(new Coordinate(cell.GridCoordinate.X - 1, cell.GridCoordinate.Y + 1));
            }

            return neighbors;
        }

        private Subregion? CellFoundInSubregion(Coordinate interestingCell)
        {
            Subregion? returnValue = null;

            foreach (var subregion in Subregions)
            {
                if (subregion.Value.Cells.ContainsKey(interestingCell))
                {
                    returnValue = subregion.Value;
                    break;
                }
            }

            return returnValue;
        }

        private void AddCellToNewSubregion(KeyValuePair<Coordinate, Cell> interestingCell)
        {
            Subregion newSubregion = new((uint)Subregions.Count);
            newSubregion.Cells.Add(interestingCell.Key, interestingCell.Value);
            Subregions.Add(newSubregion.Id, newSubregion);
        }
    }
}
