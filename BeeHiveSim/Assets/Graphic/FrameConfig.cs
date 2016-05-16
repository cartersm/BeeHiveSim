using UnityEngine;
using System.Collections;

public class FrameConfig : MonoBehaviour
{
    public Transform start = new RectTransform();
    public Transform end = new RectTransform();

    void Start () {
        start.position = new Vector3(0, 0, 0);
        end.position = new Vector3(2, 2, 2);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(start.position, end.position);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
