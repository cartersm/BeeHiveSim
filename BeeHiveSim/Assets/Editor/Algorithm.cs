using System;
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
            // place one brick at a predefined site
            this.Grid.Cells[10, 10, 10].BrickType = 1;
            this.Grid.Cells[10, 10, 10].IsOccupied = true;

            for (var k = 1; k <= this._numBees; k++)
            {
                // TODO: construct lookup table here
                var lookupTable = new Dictionary<LocalConfiguration, double>();

                var p = GetPoint();
                this.Grid.Cells[p.x, p.y, p.z].IsOccupied = true;
                this._bees.Add(new Bee(k, new Point3D(p.x, p.y, p.z), lookupTable));
            }

            // Main loop
            for (var t = 1; t <= this._tMax; t++)
            {
                Update();
            }
        }

        private Point3D GetPoint()
        {
            int x, y, z;
            do
            {
                x = _random.Next(21);
                y = _random.Next(21);
                z = _random.Next(21);
            } while (this.Grid.Cells[x, y, z].IsOccupied);
            return new Point3D(x ,y, z);
        }

        public void Update()
        {
            for (var k = 1; k <= this._numBees; k++)
            {
                var bee = this._bees[k];
                // Sense local configuration
                var cells = this.Grid.GetAdjacentCells(bee.Location);
                var config = new LocalConfiguration(cells);
                var brickToPlace = bee.SenseEnvironment(config);
                if (brickToPlace != 0)
                {
                    // Deposit brick specified by lookup table
                    this.Grid.DepositBrick(bee.Location, brickToPlace);
                }

                var p = GetPoint();
                this.Grid.Cells[p.x, p.y, p.z].IsOccupied = true;
                bee.Location = p;
            }
        }

        public void Main(string[] args)
        {
            var a = new Algorithm(10, 20000);
            a.Start();
        }
    }
}
