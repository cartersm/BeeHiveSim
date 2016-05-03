using System;
using System.Collections;
using System.Collections.Generic;

// Implements the BeeHive Construction Algorithm
namespace Assets.Editor
{
    public class Algorithm
    {
        private int _numBees;
        private int _tMax;
        private List<Bee> _bees;
        public BeeHiveGrid Grid { get; set; }
        private Random _random;

        public Algorithm(int numBees, int tMax)
        {
            this._numBees = numBees;
            this._tMax = tMax;
            this._bees = new List<Bee>();
            this._random = new Random();
        }

        public void Start()
        {
            // TODO: place one block at top of grid

            for (var k = 1; k <= this._numBees; k++)
            {
                // TODO: construct lookup table here
                var lookupTable = new Dictionary<LocalConfiguration, double>();

                int x, y, z;
                do
                {
                    x = _random.Next(21);
                    y = _random.Next(21);
                    z = _random.Next(21);

                } while (this.Grid.Cells[x, y, z].IsOccupied);
                this.Grid.Cells[x, y, z].IsOccupied = true;
                this._bees.Add(new Bee(k, new Point3D(x, y, z), lookupTable));

            }

            // Main loop
            for (var t = 1; t <= this._tMax; t++)
            {
                Update();
            }
        }

        public void Update()
        {
            for (var k = 1; k <= this._numBees; k++)
            {
                // TODO: sense local configuration (look at surrounding blocks)
                var cells = this.Grid.GetAdjacentCells(this._bees[k].Location);
                var config = new LocalConfiguration(cells);
                var isInLookupTable = this._bees[k].SenseEnvironment(config);
                if (isInLookupTable)
                {
                    // TODO: deposit brick specified by lookup table
                    // TODO: draw a new brick
                }
                // TODO: move to random, unoccupied, adjacent site (currently (0, 0, 0))
                this._bees[k].Location = new Point3D();
            }
        }

        public void Main(string[] args)
        {
            var a = new Algorithm(10, 20000);
            a.Start();
        }
    }
}
