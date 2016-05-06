using UnityEngine;
using System.Collections;

public class UnityPoint3D : MonoBehaviour {
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public UnityPoint3D(float x = 0, float y = 0, float z = 0)
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
