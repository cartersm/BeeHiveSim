using System;
using System.Collections.Generic;

namespace Assets.Editor
{
    public class BeeHiveGrid
    {
        // x, y, z
        public Cell[,,] Cells { get; private set; }

        public BeeHiveGrid()
        {
            this.Cells = new Cell[20, 20, 20];
        }

        public Cell[,] GetAdjacentCells(Point3D location)
        {
            var cells = new Cell[3, 6];
            throw new System.NotImplementedException();
        }

        private Cell _getCell(Point3D location)
        {
            try
            {
                return Cells[location.x, location.y, location.z];
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }
    }


}