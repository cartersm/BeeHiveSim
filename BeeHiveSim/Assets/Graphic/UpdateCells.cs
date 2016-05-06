using UnityEngine;
using System.Collections;
using Assets.Editor;


public class UpdateCells : MonoBehaviour
{

     void Start () {
        //this.hexagon = Instantiate(Resources.Load("Hexagon")) as GameObject;
         for (int i = 0; i < 10; i++)
         {
             for (int j = 0; j < 10; j++)
             {
                 for (int k = 0; k < 10; k++)
                 {
                     UnityPoint3D temp = getUnityPoint3D(i, j, k);
                     Instantiate(Resources.Load("Hexagon"), new Vector3(temp.x, temp.y, temp.z), transform.rotation)
                         .name = "Hexagon" + (i*100 + j*10 + k);
                 }
             }
         }
     }

    // Update is called once per frame
    void Update () {
	
	}

    UnityPoint3D getUnityPoint3D(Point3D location)
    {
        UnityPoint3D unityPoint3D = new UnityPoint3D();
        unityPoint3D.x = Mathf.Sqrt(3)/2*location.x;
        unityPoint3D.y = 0.9f*location.z;
        unityPoint3D.z = location.y+0.5f*location.x;
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
}
