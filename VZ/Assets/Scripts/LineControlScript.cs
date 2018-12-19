using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControlScript : MonoBehaviour
{

    public GameObject dataCenter;
    public GameObject singleLinePrefab;

    // Use this for initialization
    void Start()
    {

        //Cursor invisible. Comment this out for dev on regular screens. Make active for play on touchscreens
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If left mouse button click (or screen is touched)
        if (Input.GetMouseButtonDown(0))
        {
            //Send a raycast from the cursor or the location where the screen is touched
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //If the raycast hits the data center
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == dataCenter && GameController.Instance.gameOver == false)
                {
                    //Run StartNewLine
                    StartNewLine();
                }
            }
        }
    }

    void StartNewLine()
    {
        //Instantiate a singleLine prefab in the hierarchy
        GameObject singleLine = Instantiate(singleLinePrefab);
        //And then run the StartLine script in the LineDrawingController
        singleLine.GetComponent<LineDrawingController>().StartLine();
        singleLine.tag = "line";
    }
}
