using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineDrawingController : MonoBehaviour {
    LineRenderer lineRenderer;
    int count = 0;
    public float lineCreationTime;
    bool activeDrawing = true;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = GameSettings.Instance.lineWidth;
        lineRenderer.endWidth = GameSettings.Instance.lineWidth;
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

        // Function to draw lines
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
                //Debug.Log(timeSinceLineCreated);
                lineRenderer.material.color = Color.blue;
                intersectedHouse.networkCapacity += GameSettings.Instance.networkCapacityIncrease;
                intersectedHouse.connectedLines.Add(this);
                activeDrawing = false;
                lineCreationTime = Time.time;

            }

        //Make the lines become thin and transparent over time
        }
        else
        {
            float deltaTime = Time.time - lineCreationTime;
            float maxTime = 15;

            float t = deltaTime / maxTime; //create a rate of decay for the lines
            t = Mathf.Min(1, t); //makes sure "t" is never smaller than 1
            float width = Mathf.Lerp(GameSettings.Instance.lineWidth, 0.01f, t); //interpolates between current line width and super thin line width based on the value of t. Make second value 0 to make lines disappear completely.
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.material.color = new Color(0, 0, 255, Mathf.Lerp(1, 0.01f, t));

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
