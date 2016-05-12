﻿using System;
using Assets.Editor;
using Assets.Graphic;

namespace Assets.Algorithm
{
    public class BeeHiveGrid
    {
        public int X;
        public int Y;
        public int Z;
        // x, y, z
        public Cell[,,] Cells { get; private set; }

        public BeeHiveGrid(int x = 20, int y = 20, int z = 20)
        {
            Cells = new Cell[20, 20, 20];
            X = x;
            Y = y;
            Z = z;
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

        public Cell[,] GetAdjacentCells(Point3D location, bool forLocalConfig=false)
        {
            var cells = new Cell[3, 7];
            for (var i = -1; i <= 1; i++)
            {
                int x = location.X, y = location.Y, z = location.Z + i;

                var idx = i + 1;
                cells[idx, 0] = _tryGetCell(x - 1, y + 1, z, forLocalConfig);
                cells[idx, 1] = _tryGetCell(x, y + 1, z, forLocalConfig);
                cells[idx, 2] = _tryGetCell(x + 1, y, z, forLocalConfig);
                cells[idx, 3] = _tryGetCell(x + 1, y - 1, z, forLocalConfig);
                cells[idx, 4] = _tryGetCell(x, y - 1, z, forLocalConfig);
                cells[idx, 5] = _tryGetCell(x - 1, y, z, forLocalConfig);

                cells[idx, 6] = 
                    i == 0 
                    ? null 
                    : _tryGetCell(x, y, z, forLocalConfig);
            }

            return cells;
        }

        private Cell _tryGetCell(int x, int y, int z, bool forLocalConfig=false)
        {
            try
            {
                return Cells[x, y, z];
            }
            catch (IndexOutOfRangeException)
            {
                // no null cells on edges
                return forLocalConfig ? new Cell(0) : null;
            }
        }

        public void DepositBrick(Point3D location, int brickToPlace)
        {
            Cells[location.X, location.Y, location.Z].BrickType = brickToPlace;
            this.OccupyCell(location);
        }

        public void OccupyCell(Point3D location)
        {
            this.Cells[location.X, location.Y, location.Z].IsOccupied = true;
        }

        public void OccupyCell(int x, int y, int z)
        {
            this.Cells[x, y, z].IsOccupied = true;
        }

        public void UnOccupyCell(Point3D location)
        {
            this.Cells[location.X, location.Y, location.Z].IsOccupied = false;
        }

        public bool IsCellOccupied(Point3D location)
        {
            return IsCellOccupied(location.X, location.Y, location.Z);
        }

        public bool IsCellOccupied(int x, int y, int z)
        {
            return this.Cells[x, y, z].IsOccupied;
        }

        public void SetBrickType(int x, int y, int z, int brickType)
        {
            this.Cells[x, y, z].BrickType = brickType;
        }

        public void SetBrickType(Point3D p, int brickType)
        {
            this.Cells[p.X, p.Y, p.Z].BrickType = brickType;
        }
    }
}