using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public float networkCapacity;
    public List<LineDrawingController> connectedLines = new List<LineDrawingController>(); //Dynamic array to hold lines
    public GameObject statusBar;
    
    float creationTime;
    float efficiency;
    float happiness = 1;

    static public House Instance;

    // Use this for initialization
    void Start()
    {
        //Set networkCapacity to whatever was set in the game settings menu
        networkCapacity = GameSettings.Instance.initialNetworkCapacity;

        //Store the game time in the creationTime variable
        creationTime = Time.time;

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        // Store the time that a house was created
        float timeSinceHouseCreation = Time.time - creationTime;

        //Demand calculated with time to the power of rate
        GameSettings.Instance.demand = Mathf.Pow(timeSinceHouseCreation, GameSettings.Instance.growthRatePerSecond);

        //Demand calculated with rate to the power of time (actual exponential growth)
        //GameSettings.Instance.demand = Mathf.Pow(GameSettings.Instance.growthRatePerSecond, timeSinceHouseCreation);


        // Get a ratio of a house's capacity over its demand. Demand always grows, so the ratio tends to drop below 1.
        // The ratio *can* be higher than 1, but never less than zero
        // Happiness < 1 -> a customer getting unhappy
        happiness = networkCapacity / GameSettings.Instance.demand;


        //Get y scale of status bar
        Vector3 statusBarScale = statusBar.transform.localScale;
        float yScale = statusBarScale.y;

        //Make the yScale of the status bar grow or shrink, depending on happiness
        if (happiness >= 1)
        {
            yScale = yScale + GameSettings.Instance.growthVel * Time.deltaTime;
            yScale = Mathf.Min(yScale, 1); //yScale is never more than 1
        }
        else
        {
            yScale = yScale - GameSettings.Instance.dropVel * Time.deltaTime;
            yScale = Mathf.Max(yScale, 0); //yScale is never less than 0
        }
        statusBarScale.y = yScale;
        statusBar.transform.localScale = statusBarScale;

        //Remove house and connected lines if happiness falls to 0 or below
        if (yScale <= 0)
        {
            // House dies
            Destroy(gameObject);
            // Connected lines die
            for (int i = 0; i < connectedLines.Count; i++)
            {
                Destroy(connectedLines[i].gameObject);
            }

            GameController.Instance.destroyedHouses++;
        }


        // Dying building flashes red below 0.3 happiness. Goes back to white above 0.3 happiness.
        if (statusBarScale.y < 0.3)
        {
            if (Time.time % 0.5 < 0.25)
            {
                statusBar.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
            else
            {
                statusBar.GetComponentInChildren<Renderer>().material.color = new Color32(126, 18, 119, 1);
            }
        }
        else
        {
            statusBar.GetComponentInChildren<Renderer>().material.color = new Color32(126, 18, 119, 1);
        }
    }


}
