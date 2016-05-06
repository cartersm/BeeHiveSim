using System;
using Assets.Editor;
using Assets.Graphic;

namespace Assets.Algorithm
{
    public class BeeHiveGrid
    {
        private int _x;
        private int _y;
        private int _z;

        public BeeHiveGrid(int x = 20, int y = 20, int z = 20)
        {
            Cells = new Cell[20, 20, 20];
            _x = x;
            _y = y;
            _z = z;
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    for (var k = 0; k < z; k++)
                    {
                        Cells[i, j, k] = new Cell(i, j, k);
                    }
                }
            }
        }

        // x, y, z
        public Cell[,,] Cells { get; private set; }

        public Cell[,] GetAdjacentCells(Point3D location)
        {
            var cells = new Cell[3, 7];
            for (var i = -1; i < 2; i++)
            {
                int x = location.X, y = location.Y, z = location.Z + i;

                var idx = i + 1;
                cells[idx, 0] = _tryGetCell(x - 1, y + 1, z);
                cells[idx, 1] = _tryGetCell(x, y + 1, z);
                cells[idx, 2] = _tryGetCell(x + 1, y, z);
                cells[idx, 3] = _tryGetCell(x + 1, y - 1, z);
                cells[idx, 4] = _tryGetCell(x, y - 1, z);
                cells[idx, 5] = _tryGetCell(x - 1, y, z);

                cells[idx, 6] = 
                    i == 0 
                    ? null 
                    : _tryGetCell(x, y, z);
            }

            return cells;
        }

        private Cell _tryGetCell(int x, int y, int z)
        {
            try
            {
                return Cells[x, y, z];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public void DepositBrick(Point3D location, int brickToPlace)
        {
            Cells[location.X, location.Y, location.Z].BrickType = brickToPlace;
        }

        public void OccupyCell(Point3D location)
        {
            this.Cells[location.X, location.Y, location.Z].IsOccupied = true;
        }

        public void UnOccupyCell(Point3D location)
        {
            this.Cells[location.X, location.Y, location.Z].IsOccupied = false;
        }

        public bool isCellOccupied(Point3D location)
        {
            return isCellOccupied(location.X, location.Y, location.Z);
        }

        public bool isCellOccupied(int x, int y, int z)
        {
            return this.Cells[x, y, z].IsOccupied;
        }
    }
}