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

        public Cell[][] GetAdjacentCells(Point3D location)
        {
            var cells = new Cell[3][];
            for (var i = -1; i < 2; i++)
            {
                int x = location.x, y = location.y, z = location.z + i;

                var innerCells = new Cell[7];

                var idx = i + 1;
                innerCells[0] = _tryGetCell(x - 1, y + 1, z);
                innerCells[1] = _tryGetCell(x, y + 1, z);
                innerCells[2] = _tryGetCell(x + 1, y, z);
                innerCells[3] = _tryGetCell(x + 1, y - 1, z);
                innerCells[4] = _tryGetCell(x, y - 1, z);
                innerCells[5] = _tryGetCell(x - 1, y, z);

                innerCells[6] = 
                    i == 0 
                    ? null 
                    : _tryGetCell(x, y, z);

                cells[idx] = innerCells;
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
            Cells[location.x, location.y, location.z].BrickType = brickToPlace;
        }
    }
}