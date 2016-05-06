﻿using UnityEngine;
using System.Collections;
using Assets.Editor;

public class UpdateCells : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    UnityPoint3D getUnityPoint3D(Point3D location)
    {
        UnityPoint3D unityPoint3D = new UnityPoint3D();
        unityPoint3D.x = Mathf.Sqrt(3)/2*location.x;
        unityPoint3D.y = 0.9*location.z;
        unityPoint3D.z = location.y+0.5*location.x;
        return unityPoint3D;
    }
}