using System;
using Assets.Graphic;

namespace Assets.Editor
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
            // TODO
            var cells = new Cell[3, 7];
            throw new NotImplementedException();
        }

        private Cell _getCell(Point3D location)
        {
            try
            {
                return Cells[location.x, location.y, location.z];
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