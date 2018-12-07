using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {
    public GameObject objectToFollow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(objectToFollow.transform);
    //    if (Input.GetKey("a"))
    //    {
    //        transform.Rotate(0, 1, 0);
    //    }
    //    else if (Input.GetKey("d"))
    //    {
    //        transform.Rotate(0, -1, 0);
    //    }
    }
}
