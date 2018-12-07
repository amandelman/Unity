using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawingController : MonoBehaviour {

    public float networkCapacityIncrease = 3;

    LineRenderer lineRenderer;
    int count = 0;
    bool activeDrawing = true;


	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
	}

    public void StartLine() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, mousePos);
    }

	
	// Update is called once per frame
	void Update () {
        if (activeDrawing)
        {
            // draw on mouse down
            if (Input.GetMouseButton(0))
            {
                lineRenderer.positionCount = count + 1;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                lineRenderer.SetPosition(count, mousePos);
                count++;
            }

            House intersectedHouse = CheckHouseIntersection();


            if (Input.GetMouseButtonUp(0) && intersectedHouse == null)
            {
                Destroy(this.gameObject);
            }
            else if (intersectedHouse != null)
            {
                lineRenderer.material.color = Color.blue;
                intersectedHouse.networkCapacity += networkCapacityIncrease;
                intersectedHouse.connectedLines.Add(this);
                activeDrawing = false;

            }

        }
	}




    House CheckHouseIntersection() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            GameObject objectHit = hits[i].transform.gameObject;
            House house = objectHit.GetComponent<House>();
            if (house!=null) {
                return house;
            } 
        }

        return null;
    }
}
