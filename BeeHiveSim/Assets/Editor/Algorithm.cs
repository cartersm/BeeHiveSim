using System.Collections;
using System.Collections.Generic;

// Implements the BeeHive Construction Algorithm
namespace Assets.Editor
{
    public class Algorithm
    {
        private Hashtable _lookupTable;
        private int _m;
        private int _tMax;
        private List<Bee> _bees;
        public BeeHiveGrid Grid { get; set; }

        public Algorithm(int m, int tMax)
        {
            this._m = m;
            this._tMax = tMax;
            this._lookupTable = new Hashtable();
            this._bees = new List<Bee>();
        }

        public void Start()
        {
            // TODO: place one block at top of grid

            for (var k = 1; k <= this._m; k++)
            {
                // TODO: randomly assign agent k to an unoccupied site (currently assigned to (0, 0, 0)
                this._bees.Add(new Bee(k, new Point3D()));
            }

            // Main loop
            for (var t = 1; t <= this._tMax; t++)
            {
                Update();
            }
        }

        public void Update()
        {
            for (var k = 1; k <= this._m; k++)
            {
                // TODO: sense local configuration (look at surrounding blocks)
                var config = new LocalConfiguration(this.Grid, this._bees[k].Location);
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
            var a = new Algorithm(10, 100);
            a.Start();
        }
    }
}
