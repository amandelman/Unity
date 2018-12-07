using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("right")){
            rigidBody.AddForce(200, 150, 0);
        } else if(Input.GetKeyDown("left")){
            rigidBody.AddForce(-200, 150, 0);
        } else if (Input.GetKeyDown("up"))
        {
            rigidBody.AddForce(0, 150, 200);
        } else if (Input.GetKeyDown("down"))
        {
            rigidBody.AddForce(0, 150, -200);
        }



    }
}
