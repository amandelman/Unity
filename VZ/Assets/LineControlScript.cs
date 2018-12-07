using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControlScript : MonoBehaviour {

    public GameObject dataCenter;
    public GameObject singleLinePrefab;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //Debug.Log(Camera.main);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == dataCenter)
                {
                    //Debug.Log("Datacenter clicked");
                    StartNewLine();
                }
            }

        }
    }

    void StartNewLine() {
        GameObject singleLine = Instantiate(singleLinePrefab);
        singleLine.GetComponent<LineDrawingController>().StartLine();
    }
}
