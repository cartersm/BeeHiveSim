using System.Collections;
using UnityEngine;

namespace Assets.Graphic
{
    /// <summary>
    /// The entry point of the simulation. 
    /// This behavior is attached to a single hexagon, and generates the simulation from there.
    /// </summary>
    public class UpdateCells : MonoBehaviour
    {
        public int X = 20;
        public int Y = 20;
        public int Z = 20;
        public int InitX = 10;
        public int InitY = 10;
        public int InitZ = 19;
        public int InitType = 1;
        public int NBees = 30;
        public int NSteps = 5000;
        public string Path = "Assets/Configs/Architecture4d.txt";
        private int _nStepsTaken;
        public static int MaxI = 20;
        public static int MaxJ = 20;
        public static int MaxK = 20;
        private Algorithm.Algorithm _algorithm;


        public int[,,] OldOccupation;
        public GameObject[,,] OldObjects;

        private int _rotationMaker;
        public void Start()
        {
            OldOccupation = new int[X, Y, Z];
            OldObjects = new GameObject[X, Y, Z];

            this._algorithm = new Algorithm.Algorithm(NBees, NSteps, Path, X, Y, Z);
            this._algorithm.Grid.OccupyCell(InitX, InitY, InitZ);
            this._algorithm.Grid.SetBrickType(InitX, InitY, InitZ, InitType);

            this._algorithm.Start();
            Application.runInBackground = true;

        }

        // Update is called once per frame
        /// <summary>
        /// Runs one step of the algorithm, evaluates changes to cells, and transforms them to match those changes.
        /// </summary>
        // TODO: CONSIDER: for the sake of efficiency, future implementations could have the algorithm store the cells 
        //      that were changed on the most recent iteration. Then this method would only need to run on those cells.
        //      This would bring the runtime down to a constant 2 * nBees (nBees cells unoccupied/bricked, nBees cells
        //      newly occupied by bees), instead of O(X * Y * Z) increasing as more cells are placed.
        public void Update()
        {
            if (this._algorithm == null) return; // TODO: FIXME: Algorithm apparently becomes null once we stop
            if (this._nStepsTaken >= this._algorithm.MaxSteps) return;
            this._nStepsTaken++;
            this._algorithm.Update();


            for (var i = 0; i < X; i++)
            {
                for (var j = 0; j < Y; j++)
                {
                    for (var k = 0; k < Z; k++)

                    {
                        var temp = this._algorithm.Grid.GetCell(i, j, k);
                        if (temp.IsOccupied && temp.BrickType == 0)
                        {
                            switch (_rotationMaker)
                            {
                                case 0:
                                    transform.Rotate(0, 180, 0);
                                    _rotationMaker = 1;
                                    break;
                                case 1:
                                    transform.Rotate(0, 180, 0);
                                    _rotationMaker = 0;
                                    break;
                                case 2:
                                    transform.Rotate(-90, 0, 0);
                                    _rotationMaker = 0;
                                    break;
                            }

                            if (OldOccupation[i, j, k] == 1)
                            {
                                Destroy(OldObjects[i, j, k]);
                                OldObjects[i, j, k] = null;
                                OldOccupation[i, j, k] = 0;
                            }
                            var tempUnity = GetUnityPoint3D(i, j, k);
                            var tempCell = Instantiate(Resources.Load("PrefabBee"),
                                new Vector3(tempUnity.X, tempUnity.Y, tempUnity.Z), transform.rotation) as GameObject;
                            if (tempCell == null) continue;
                            tempCell.name = string.Format("PrefabBee{0}{1}{2}", i.ToString("00"),
                                j.ToString("00"), k.ToString("00"));
                            OldObjects[i, j, k] = tempCell;
                            OldOccupation[i, j, k] = 1;
                        }
                        else if (!temp.IsOccupied)
                        {
                            Destroy(OldObjects[i, j, k]);
                            OldObjects[i, j, k] = null;
                            OldOccupation[i, j, k] = 0;
                        }
                        else if (temp.IsOccupied && temp.BrickType != 0)
                        {
                            switch (_rotationMaker)
                            {
                                case 0:
                                    transform.Rotate(90, 0, 0);
                                    _rotationMaker = 2;
                                    break;
                                case 1:
                                    transform.Rotate(90, 180, 0);
                                    _rotationMaker = 2;
                                    break;
                            }

                            if (OldOccupation[i, j, k] == 1)
                            {
                                Destroy(OldObjects[i, j, k]);
                                OldObjects[i, j, k] = null;
                                OldOccupation[i, j, k] = 0;
                            }
                            var tempUnity = GetUnityPoint3D(i, j, k);
                            if (temp.BrickType == 1)
                            {
                                var tempCell = Instantiate(Resources.Load("Hexagon2"),
                                new Vector3(tempUnity.X, tempUnity.Y, tempUnity.Z), transform.rotation) as
                                GameObject;
                                if (tempCell != null)
                                {
                                    tempCell.name = string.Format("Hexagon2{0}{1}{2}", i.ToString("00"),
                                        j.ToString("00"), k.ToString("00"));
                                    OldObjects[i, j, k] = tempCell;
                                }
                            }
                            else
                            {
                                var tempCell = Instantiate(Resources.Load("Hexagon"),
                                new Vector3(tempUnity.X, tempUnity.Y, tempUnity.Z), transform.rotation) as
                                GameObject;
                                if (tempCell != null)
                                {
                                    tempCell.name = string.Format("Hexagon{0}{1}{2}", i.ToString("00"),
                                        j.ToString("00"), k.ToString("00"));
                                    OldObjects[i, j, k] = tempCell;
                                }
                            }


                            OldOccupation[i, j, k] = 1;

                        }
                        else
                        {
                            Destroy(OldObjects[i, j, k]);
                            OldObjects[i, j, k] = null;
                            OldOccupation[i, j, k] = 0;
                        }

                    }
                }
            }
        }

        public IEnumerator Waithalfsec()
        {
            yield return new WaitForSeconds(1f);
        }

        public UnityPoint3D GetUnityPoint3D(Point3D location)
        {
            var unityPoint3D = new UnityPoint3D
            {
                X = Mathf.Sqrt(3) / 2 * location.X,
                Y = 0.9f * location.Z,
                Z = location.Y + 0.5f * location.X
            };
            return unityPoint3D;
        }

        public UnityPoint3D GetUnityPoint3D(int x, int y, int z)
        {
            var unityPoint3D = new UnityPoint3D
            {
                X = Mathf.Sqrt(3) / 2 * x,
                Y = 0.9f * z,
                Z = y + 0.5f * x
            };
            return unityPoint3D;
        }
    }
}
