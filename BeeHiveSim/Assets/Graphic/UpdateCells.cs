using System;
using System.Collections;
using UnityEngine;

namespace Assets.Graphic
{
    public class UpdateCells : MonoBehaviour
    {
        private int _nStepsTaken;
        private Algorithm.Algorithm _algorithm;
        public int[,,] old = new int[20,20,20];
        public GameObject[,,] OldObjects = new GameObject[20,20,20];

        void Start () {
            Console.Write("-1\n");
            //this.hexagon = Instantiate(Resources.Load("Hexagon")) as GameObject;
            //oneThousandCells();
            var nBees = 100;
            var nSteps = 10000;
            this._algorithm = new Algorithm.Algorithm(nBees, nSteps, "Assets/Editor/Architecture4d.txt");
            this._algorithm.Start();
        }

        // Update is called once per frame
        void Update ()
        {
            //Console.Write("0\n");
            waithalfsec();
            //Console.Write("-0.5-\n");
            if (this._nStepsTaken >= this._algorithm.TMax) return;
            this._nStepsTaken++;
            this._algorithm.Update();
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    for (var k = 0; k < 20; k++)
                    {
                        var temp = this._algorithm.Grid.Cells[i,j,k];
                        var preOcc = old[i, j, k];
                        if (temp.IsOccupied && (preOcc == 0))
                        {
                            //Console.Write("1\n");
                            var tempUnity = getUnityPoint3D(i, j, k);
                            var tempCell =
                                Instantiate(Resources.Load("Hexagon"),
                                    new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as
                                    GameObject;

                            tempCell.name = "Hexagon" + (i*10000 + j*100 + k);
                            OldObjects[i, j, k] = tempCell;
                            old[i, j, k] = 1;
                        }
                        else if (!temp.IsOccupied && (preOcc != 0))
                        {
                            //Console.Write("2\n");
                            Destroy(OldObjects[i, j, k]);
                            OldObjects[i, j, k] = null;
                            old[i, j, k] = 0;
                        }
                        else
                        {
                            //Do nothing
                        }

                    }
                }
            }
        }

        IEnumerator waithalfsec()
        {
            yield return new WaitForSeconds(1f);
        }

        UnityPoint3D getUnityPoint3D(Point3D location)
        {
            UnityPoint3D unityPoint3D = new UnityPoint3D();
            unityPoint3D.x = Mathf.Sqrt(3)/2*location.X;
            unityPoint3D.y = 0.9f*location.Z;
            unityPoint3D.z = location.Y+0.5f*location.X;
            return unityPoint3D;
        }

        UnityPoint3D getUnityPoint3D(int x, int y, int z)
        {
            UnityPoint3D unityPoint3D = new UnityPoint3D();
            unityPoint3D.x = Mathf.Sqrt(3) / 2 * x;
            unityPoint3D.y = 0.9f * z;
            unityPoint3D.z = y + 0.5f * x;
            return unityPoint3D;
        }

        void oneThousandCells()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        UnityPoint3D temp = getUnityPoint3D(i, j, k);
                        GameObject tempCell = Instantiate(Resources.Load("Hexagon"), new Vector3(temp.x, temp.y, temp.z), transform.rotation) as GameObject;
                        tempCell.name = "Hexagon" + (i * 100 + j * 10 + k);
                        Destroy(tempCell, (i * 100 + j * 10 + k) / 20f);
                    }
                }
            }
        }
    }
}
