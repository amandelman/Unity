using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffScript : MonoBehaviour {

    //make the SerialControl script available here in the form of the variable "serialScript"
    public SerialControl serialScript;

    public GameObject treeRight;

    public GameObject treeLeft;


    //Declare material variables for skin and leaf materials
    //public Material skinMaterial;
    //public Material leafMaterial;

	// Use this for initialization
	void Start () {
	}

    // create functions to add skin or leaf materials
    //public void addSkin(){
    //    GetComponent<Renderer>().material = skinMaterial;
    //    //call the port variable from SerialControl with the variable serialScript
    //    serialScript.port.WriteLine("on");
    //    serialScript.port.BaseStream.Flush();
    //}


    //public void removeSkin(){
    //    GetComponent<Renderer>().material = leafMaterial;
    //    //call the port variable from SerialControl with the variable serialScript
    //    serialScript.port.WriteLine("off");
    //    serialScript.port.BaseStream.Flush();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Renderer>().material = collision.gameObject.GetComponent<Renderer>().material;

        if (collision.gameObject== treeRight) {
            serialScript.port.WriteLine("leaf");
            serialScript.port.BaseStream.Flush();
        } else if (collision.gameObject == treeLeft){
            serialScript.port.WriteLine("skin");
            serialScript.port.BaseStream.Flush();
        } else {
            serialScript.port.WriteLine("lava");
            serialScript.port.BaseStream.Flush();
        }

        Debug.Log(GetComponent<Renderer>().material);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
