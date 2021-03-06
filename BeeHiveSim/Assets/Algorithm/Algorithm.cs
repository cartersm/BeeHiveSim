﻿using System.Collections.Generic;
using Assets.Graphic;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace Assets.Algorithm
{
    /// <summary>
    /// Implements the Beehive Construction Algorithm.
    /// </summary>
    public class Algorithm
    {
        private readonly int _numBees;
        public int MaxSteps;
        public int MaxI;
        public int MaxJ;
        public int MaxK;

        private readonly List<Bee> _bees;
        public BeeHiveGrid Grid { get; set; }
        private readonly Random _random;
        private readonly string _filename;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numBees">The number of bees (agents) to use.</param>
        /// <param name="maxSteps">The maximum number of steps the algorithm should take.</param>
        /// <param name="filename">The filename to load the Lookup Table fro.m</param>
        /// <param name="maxI">The size of the Grid's X-axis.</param>
        /// <param name="maxJ">The size of the Grid's Y-axis.</param>
        /// <param name="maxK">The size of the Grid's Z-axis.</param>
        public Algorithm(int numBees, int maxSteps, string filename, int maxI, int maxJ, int maxK)
        {
            this._numBees = numBees;
            this.MaxSteps = maxSteps;
            this._filename = filename;
            this.MaxI = maxI;
            this.MaxJ = maxJ;
            this.MaxK = maxK;

            this._bees = new List<Bee>();
            this._random = new Random();
            this.Grid = new BeeHiveGrid(maxI, maxJ, maxK);
        }

        /// <summary>
        /// Initialize the starting cell and starting locations of the bees.
        /// </summary>
        public void Start()
        {
            var lookupTable = ConfigParser.Parse(this._filename);

            for (var k = 0; k < this._numBees; k++)
            {
                var p = GetUnoccupiedPoint();

                this.Grid.OccupyCell(p);
                this._bees.Add(new Bee(p, lookupTable));
            }
        }

        /// <summary>
        /// Run one iteration of the algorithm.
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
        /// <returns>The first unoccupied cell found.</returns>
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
        /// <param name="p">The point to start from.</param>
        /// <returns>A random point from the unoccupied cells adjacent to the given point.</returns>
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
