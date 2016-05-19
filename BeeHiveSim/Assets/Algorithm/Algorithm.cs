﻿using System.Collections.Generic;
using Assets.Graphic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

// Implements the BeeHive Construction Algorithm
namespace Assets.Algorithm
{
    public class Algorithm
    {
        private readonly int _numBees;
        public int TMax;
        public int maxI;
        public int maxJ;
        public int maxK;

        private readonly List<Bee> _bees;
        public BeeHiveGrid Grid { get; set; }
        private readonly Random _random;
        private readonly string _filename;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numBees">The number of bees (agents) to use</param>
        /// <param name="tMax">The maximum number of steps hte algorithm should take</param>
        /// <param name="filename">The filename to load the Lookup Table from</param>
<<<<<<< Updated upstream
        public Algorithm(int numBees, int tMax, string filename, int x, int y, int z)
=======
        /// <param name="maxI">The max I value of the array</param>
        /// <param name="maxJ">The max I value of the array</param>
        /// <param name="maxK">The max I value of the array</param>
        public Algorithm(int numBees, int tMax, string filename, int maxI, int maxJ, int maxK)
>>>>>>> Stashed changes
        {
            this._numBees = numBees;
            this.TMax = tMax;
            this.maxI = maxI;
            this.maxJ = maxJ;
            this.maxK = maxK;
            this._bees = new List<Bee>();
            this._random = new Random();
            this._filename = filename;
<<<<<<< Updated upstream
            this.Grid = new BeeHiveGrid(x, y, z);
=======
            this.Grid = new BeeHiveGrid(maxI,maxJ,maxK);
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Initialize the starting cell and starting locations of the bees
        /// </summary>
        public void Start()
        {
            // place one brick at a predefined site
<<<<<<< Updated upstream
            //this.Grid.OccupyCell(10, 10, 19);
            //this.Grid.SetBrickType(10, 10, 19, 1);
=======
            this.Grid.OccupyCell(maxI/2, maxJ/2, maxK/2);
            this.Grid.SetBrickType(maxI/2, maxJ/2, maxK/2, 1);
>>>>>>> Stashed changes
            var lookupTable = ConfigParser.Parse(this._filename);

            for (var k = 0; k < this._numBees; k++)
            {
                var p = GetUnoccupiedPoint();

                this.Grid.Cells[p.X, p.Y, p.Z].IsOccupied = true;
                this._bees.Add(new Bee(k, new Point3D(p.X, p.Y, p.Z), lookupTable));
            }
        }

        /// <summary>
        /// Run one iteration of the algorithm
        /// </summary>
        public void Update()
        {
            for (var k = 0; k < this._numBees; k++)
            {
                var bee = this._bees[k];
                // Sense local configuration
                var cells = this.Grid.GetAdjacentCells(bee.Location, true);
                var config = new LocalConfiguration(cells);
                var brickToPlace = bee.SenseEnvironment(config);
                if (brickToPlace != null)
                {
                    Debug.Log("Discovered Config: " + brickToPlace);
                    // Deposit brick specified by lookup table
                    this.Grid.DepositBrick(bee.Location, brickToPlace.BrickType);
                }
                else
                {
                    this.Grid.UnOccupyCell(bee.Location);
                }

                var p = GetUnoccupiedAdjacentPoint(bee.Location);
                this.Grid.OccupyCell(p);
                bee.Location = p;
            }
        }

        /// <summary>
        /// Returns a random unoccupied point in the Grid.
        /// </summary>
        /// <returns>The first unoccupied cell found</returns>
        private Point3D GetUnoccupiedPoint()
        {
            int x, y, z;
            do
            {
                // get random point
                x = _random.Next(this.Grid.X);
                y = _random.Next(this.Grid.Y);
                z = _random.Next(this.Grid.Z);
            } while (this.Grid.IsCellOccupied(x, y, z));
            return new Point3D(x, y, z);
        }

        /// <summary>
        /// Returns a Random unoccupied point adjacent to the given point.
        /// </summary>
        /// <param name="p">The point to start from</param>
        /// <returns>A random point from the unoccupied cells adjacent to the given point</returns>
        private Point3D GetUnoccupiedAdjacentPoint(Point3D p)
        {
            var adjacentCells = this.Grid.GetOneAdjacentCells(p);
            var unoccupiedCells = new List<Cell>();
            foreach (var c in adjacentCells)
            {
                if (c != null && !c.IsOccupied)
                {
                    unoccupiedCells.Add(c);
                }
            }
            return unoccupiedCells[_random.Next(unoccupiedCells.Count)].Location;
        }
    }
}
