using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineDrawingController : MonoBehaviour
{

    //Name a lineRenderer
    LineRenderer lineRenderer;
    int count = 0;
    public float lineCreationTime;

    //Boolean for active drawing
    bool activeDrawing = true;

    // Use this for initialization
    void Start()
    {

        //Get the line renderer
        lineRenderer = GetComponent<LineRenderer>();

        //Set the line renderer's width based on whatever was entered in game settings
        lineRenderer.startWidth = GameSettings.Instance.lineWidth;
        lineRenderer.endWidth = GameSettings.Instance.lineWidth;

    }

    public void StartLine()
    {

        //Start the line renderer where the mouse (or touch) is
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, mousePos);

    }

    // Update is called once per frame
    void Update()
    {

        // Function to draw lines if active drawing is true
        if (activeDrawing)
        {
            // Draw on left mouse button down
            if (Input.GetMouseButton(0))
            {
                lineRenderer.positionCount = count + 1;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                lineRenderer.SetPosition(count, mousePos);
                count++;
            }

            //Check if the line intersects a house
            House intersectedHouse = CheckHouseIntersection();

            //If the line does not intersect a house, then destroy the line
            if (Input.GetMouseButtonUp(0) && intersectedHouse == null)
            {
                Destroy(this.gameObject);
            }

            //Otherwise connect the line, set the line color to blue, and stop active drawing
            else if (intersectedHouse != null)
            {
                lineRenderer.material.color = new Color32(246, 142, 0, 1);
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
            lineRenderer.material.color = new Color(0.8f, 0.8f, 0.8f, Mathf.Lerp(1, 0.01f, t));
        }
    }


    // Function for checking if lines connect with houses
    House CheckHouseIntersection()
    {

        //Send a raycast from the mouse or touch position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        //Make the raycast count through all the objects in its way
        for (int i = 0; i < hits.Length; i++)
        {
            //If the raycast hits a house (as opposed to a status bar), then return true, otherwise return null
            GameObject objectHit = hits[i].transform.gameObject;
            House house = objectHit.GetComponent<House>();
            if (house != null)
            {
                return house;
            }
        }

        return null;
    }
}
