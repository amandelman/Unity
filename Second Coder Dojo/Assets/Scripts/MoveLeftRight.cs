using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveLeftRight : MonoBehaviour {

    //name rigidbody property for script
    public Rigidbody rigidBody;
    // Use this for initialization
    void Start () {


		
	}
	
    public void MoveLeft(){
        //transform moves things more discretely and rigidly
        //transform.position = transform.position + new Vector3(-0.5f, 0.5f, 0);

        //adding a force to a rigidbody moves things more smoothly
        rigidBody.AddForce(-100, 50, 0);
    }

    public void MoveRight(){
        //transform.position = transform.position + new Vector3(0.5f, 0.5f, 0);
        rigidBody.AddForce(100, 50, 0);
    }


	// Update is called once per frame
    void Update () {

        }
		
	}

