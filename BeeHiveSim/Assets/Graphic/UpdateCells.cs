using System.Collections;
using UnityEngine;


namespace Assets.Graphic
{
    public class UpdateCells : MonoBehaviour
    {
        public int x = 0;
        public int y = 0;
        public int z = 0;
        public int init_x = 0;
        public int init_y = 0;
        public int init_z = 0;
        public int init_type = 1;
        public int  nBees = 20;
        public int nSteps = 3000;
        private int _nStepsTaken;
        private Algorithm.Algorithm _algorithm;

        public int[,,] old;
        public GameObject[,,] OldObjects;

        int rotationMaker = 0;
        void Start ()
        {


            //preSetCells();
            old = new int[x, y, z];
            OldObjects = new GameObject[x, y, z];
            //Console.Write("-1\n");
            //this.hexagon = Instantiate(Resources.Load("Hexagon")) as GameObject;
            //oneThousandCells();


            this._algorithm = new Algorithm.Algorithm(nBees, nSteps, "Assets/Editor/Architecture4d.txt",x , y, z);
            this._algorithm.Grid.OccupyCell(init_x, init_y, init_z);
            this._algorithm.Grid.SetBrickType(init_x, init_y, init_z, init_type);
            this._algorithm.Start();
            Application.runInBackground = true;

        }

        // Update is called once per frame
        void Update ()
        {
            //Console.Write("0\n");
            //waithalfsec();
            //Console.Write("-0.5-\n");
            if (this._nStepsTaken >= this._algorithm.TMax) return;
            this._nStepsTaken++;
            this._algorithm.Update();
            

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    for (var k = 0; k < z; k++)
                    {
                        var temp = this._algorithm.Grid.Cells[i, j, k];
                        var preOcc = old[i, j, k];
                        if (temp.IsOccupied && temp.BrickType == 0)
                        {
                            if (rotationMaker == 0) 
                            {
                                transform.Rotate(0, 180, 0);
                                rotationMaker = 1;
                            }
                            else if (rotationMaker == 1) 
                            {
                                transform.Rotate(0, 180, 0);
                                rotationMaker = 0;
                            }
                            else if (rotationMaker == 2)
                            {
                                transform.Rotate(-90, 0, 0);
                                rotationMaker = 0;
                            }

                            if (old[i, j, k] == 1)
                            {
                                Destroy(OldObjects[i, j, k]);
                                OldObjects[i, j, k] = null;
                                old[i, j, k] = 0;
                            }
                            var tempUnity = getUnityPoint3D(i, j, k);
                            var tempCell =
                                Instantiate(Resources.Load("PrefabBee"),
                                    new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as
                                    GameObject;
                            tempCell.name = string.Format("PrefabBee{0}{1}{2}", i.ToString("00"), j.ToString("00"), k.ToString("00"));
                            OldObjects[i, j, k] = tempCell;
                            old[i, j, k] = 1;
       
                        }
                        else if (!temp.IsOccupied)
                        {
                            //Console.Write("2\n");
                            Destroy(OldObjects[i, j, k]);
                            OldObjects[i, j, k] = null;
                            old[i, j, k] = 0;
                            /*
                            var tempUnity = getUnityPoint3D(i, j, k);
                            GameObject tempCell = Instantiate(Resources.Load("Empty"), new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as GameObject;
                            tempCell.name = "Empty" + (i * 100 + j * 10 + k);
                            OldObjects[i, j, k] = tempCell;
                            old[i, j, k] = 1;
                            */
                        }
                        else if (temp.IsOccupied && temp.BrickType != 0)
                        {
                            if (rotationMaker == 0)
                            {
                                transform.Rotate(90, 0, 0);
                                rotationMaker = 2;
                            }
                            else if (rotationMaker == 1)
                            {
                                transform.Rotate(90,180,0);
                                rotationMaker = 2;
                            }
                           

                            if (old[i, j, k] == 1)
                            {
                                Destroy(OldObjects[i, j, k]);
                                OldObjects[i, j, k] = null;
                                old[i, j, k] = 0;
                            }
                            var tempUnity = getUnityPoint3D(i, j, k);
                            if (temp.BrickType == 1)
                            {
                                var tempCell = Instantiate(Resources.Load("Hexagon2"),
                                new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as
                                GameObject;
                                tempCell.name = string.Format("Hexagon2{0}{1}{2}", i.ToString("00"), j.ToString("00"),
                                    k.ToString("00"));
                                OldObjects[i, j, k] = tempCell;
                            }
                            else
                            {
                                var tempCell = Instantiate(Resources.Load("Hexagon"),
                                new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as
                                GameObject;
                                tempCell.name = string.Format("Hexagon{0}{1}{2}", i.ToString("00"), j.ToString("00"),
                                    k.ToString("00"));
                                OldObjects[i, j, k] = tempCell;
                            }


                            old[i, j, k] = 1;
   
                        }
                        else
                        {
                            //Console.Write("2\n");
                            Destroy(OldObjects[i, j, k]);
                            OldObjects[i, j, k] = null;
                            old[i, j, k] = 0;
                            /*
                            var tempUnity = getUnityPoint3D(i, j, k);
                            GameObject tempCell = Instantiate(Resources.Load("Empty"), new Vector3(tempUnity.x, tempUnity.y, tempUnity.z), transform.rotation) as GameObject;
                            tempCell.name = "Empty" + (i * 100 + j * 10 + k);
                            OldObjects[i, j, k] = tempCell;
                            old[i, j, k] = 1;
                            */
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

        void preSetCells()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; i < y; i++)
                {
                    for (int k = 0; i < z; i++)
                    {
                        UnityPoint3D temp = getUnityPoint3D(i, j, k);
                        GameObject tempCell = Instantiate(Resources.Load("Empty"), new Vector3(temp.x, temp.y, temp.z), transform.rotation) as GameObject;
                        tempCell.name = "Empty" + (i * 100 + j * 10 + k);
                        OldObjects[i, j, k] = tempCell;
                        old[i, j, k] = 1;
                    }
                }
            }
        }


        void oneThousandCells()
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (i * 100 + j * 10 + k <= 500)
                        {
                            UnityPoint3D temp = getUnityPoint3D(i, j, k);
                            GameObject tempCell = Instantiate(Resources.Load("Empty"), new Vector3(temp.x, temp.y, temp.z), transform.rotation) as GameObject;
                            tempCell.name = "Empty" + (i * 100 + j * 10 + k);
                        }

                        if (i * 100 + j * 10 + k > 500)
                        {

                            UnityPoint3D temp = getUnityPoint3D(i, j, k);
                            GameObject tempCell = Instantiate(Resources.Load("Hexagon2"), new Vector3(temp.x, temp.y, temp.z), transform.rotation) as GameObject;
                            tempCell.name = "Hexagon2" + (i * 100 + j * 10 + k);
                        }
                        
                        //Destroy(tempCell, (i * 100 + j * 10 + k) / 20f);
                    }
                }
            }
            //GameObject upcell = GameObject.Find("Hexagon0");
            //GameObject downcell = GameObject.Find("Hexagon1");

        }
    }
}
