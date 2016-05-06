using System.Collections.Generic;
using Assets.Editor;
using Assets.Graphic;
using UnityEngine;
using Random = System.Random;

// Implements the BeeHive Construction Algorithm
namespace Assets.Algorithm
{
    public class Algorithm : MonoBehaviour
    {
        private int _numBees;
        public int TMax;
        private List<Bee> _bees;
        public BeeHiveGrid Grid { get; set; }
        private Random _random;
        private string _filename;

        public Algorithm(int numBees, int max, string filename)
        {
            this._numBees = numBees;
            this.TMax = max;
            this._bees = new List<Bee>();
            this._random = new Random();
            this._filename = filename;
            this.Grid = new BeeHiveGrid();
        }

        public void Start()
        {
            // place one brick at a predefined site
            this.Grid.Cells[10, 10, 10].BrickType = 1;
            this.Grid.Cells[10, 10, 10].IsOccupied = true;
            var lookupTable = ConfigParser.Parse(this._filename);

            for (var k = 0; k < this._numBees; k++)
            {
                var p = GetUnoccupiedPoint();
                this.Grid.Cells[p.X, p.Y, p.Z].IsOccupied = true;
                this._bees.Add(new Bee(k, new Point3D(p.X, p.Y, p.Z), lookupTable));
            }
        }

        public void Update()
        {
            for (var k = 0; k < this._numBees; k++)
            {
                var bee = this._bees[k];
                // Sense local configuration
                var cells = this.Grid.GetAdjacentCells(bee.Location);
                var config = new LocalConfiguration(cells);
                var brickToPlace = bee.SenseEnvironment(config);
                
                if (brickToPlace != null)
                {
                    Debug.Log("Discovered Config: ");
                    Debug.Log(brickToPlace);
                    // Deposit brick specified by lookup table
                    this.Grid.DepositBrick(bee.Location, brickToPlace.BrickType);
                }
                else
                {
                    this.Grid.UnOccupyCell(bee.Location);
                }

                var p = GetUnoccupiedPoint(bee.Location);
                this.Grid.OccupyCell(p);
                bee.Location = p;
                Debug.Log(bee.Location);
            }
        }

        private Point3D GetUnoccupiedPoint(Point3D p = null)
        {
            int x, y, z, nAttempts = 0;
            bool isPointIllegal = false, breakCond = false;
            int maxAttempts = 100;
            do
            {
                if (p == null)
                {
                    // get random point
                    x = _random.Next(20);
                    y = _random.Next(20);
                    z = _random.Next(20);
                }
                else
                {
                    // get adjacent point
                    x = _random.Next(Mathf.Max(p.X - 1, 0), Mathf.Min(p.X + 2, 20));
                    y = _random.Next(Mathf.Max(p.Y - 1, 0), Mathf.Min(p.Y + 2, 20));
                    z = _random.Next(Mathf.Max(p.X - 1, 0), Mathf.Min(p.X + 2, 20));
                    // exclude cartesian results that aren't adjacent in hexagons
                    // exclude results that are equal to the given point
                    isPointIllegal = (x == p.X + 1 && y == p.Y + 1)
                        || (x == p.X - 1 && y == p.Y - 1)
                        || (new Point3D(x, y, z).Equals(p));
                }
                nAttempts += 1;
                // unoccupied and legal OR greater than max attempts if not
                breakCond = (!this.Grid.Cells[x, y, z].IsOccupied && !isPointIllegal) || (nAttempts >= maxAttempts);
            } while (!breakCond);

            return nAttempts == maxAttempts ?
                p :
                new Point3D(x, y, z);
        }

        //public void Main(string[] args)
        //{
        //    var a = new Algorithm(10, 100, "TextFile1.txt");
        //    a.Start();
        //}
    }
}
