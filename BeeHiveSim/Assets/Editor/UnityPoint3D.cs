using UnityEngine;
using System.Collections;

public class UnityPoint3D : MonoBehaviour {
    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

    public UnityPoint3D(double x = 0, double y = 0, double z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
