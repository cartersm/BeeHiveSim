using System.Collections;
using System.Collections.Generic;

public class BeeHiveGrid
{
    private List<Layer> layers;

    private class Layer
    {
        private List<Cell> cells;
    }
    private class Cell
    {
        private bool hasBrick;
    }
}