# Business Requirements

## Functional

Produce a function that takes two inputs and produces the specified output.

### Input

- two-dimensional array of values
- a threshold *T*

### Output

- list of subregions of interest
- for each subregion, a coordinate (X,Y) value pair, that identifies the center of mass of the subregion.

### Business Rules

#### Grid Coordinates

- X,Y axes where each cell is numbered in zero or positive integrers
- Location (0,0) is the bottom left corner of the grid
- Examples in a 6 x 6 grid
    - (0, 5) -  Top Left corner
    - (5,5) - Top Right corner
    - (5,0) - Bottom Right corner

#### Subregions

- All contiguous groups of adjacent cells for which the signal is greater than a threshold value *T*
- Cells that are touching at corners are considered to be (diagonally) adjacent cells

#### Center of Mass

- Average position of the cells in a subregion
- cell locations are weighted by it's signal value
- cell that is closest to average (by absolute difference) is the center
- if multiple cells are equal distance from average, then the cell with the lowest Y value is the center
- if multiple cells are equal distance from average and share the lowest Y value, then the cell with the lowest X value is the center

## Non-Functional

A user interface of some sort to interact with the solution.