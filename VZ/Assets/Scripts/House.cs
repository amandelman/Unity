using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a comment

public class House : MonoBehaviour
{
    //OLD LOGIC
    //public float panicFactor = 0.3f;
    //float dropRate;
    //Offloaded to Game Settings
    //public float dropVel = 0.3f; // units/sec
    //public float growthVel = 0.3f; // units/sec
    //public float growthRatePerSecond = 1.1f; // in seconds
    //public float demand = 0.01f;

    public float networkCapacity;
    public List<LineDrawingController> connectedLines = new List<LineDrawingController>();
    public GameObject statusBar;
    float creationTime;
    float efficiency;
    float happiness = 1;

    // Use this for initialization
    void Start()
    {
        networkCapacity = GameSettings.Instance.initialNetworkCapacity;
        creationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        float timeSinceHouseCreation = Time.time - creationTime;


        //Demand calculated with time to the power of rate
        GameSettings.Instance.demand = Mathf.Pow(timeSinceHouseCreation, GameSettings.Instance.growthRatePerSecond);

        //Demand calculated with rate to the power of time (actual exponential growth)
        //GameSettings.Instance.demand = Mathf.Pow(GameSettings.Instance.growthRatePerSecond, timeSinceHouseCreation);

    

        //OLD LOGIC
        //efficiency = networkCapacity / demand;


        //NEW LOGIC
        happiness = networkCapacity / GameSettings.Instance.demand;
        // this value can be higher than 1 but never less than zero
        // target is always happiness >= 1 -> happy customer!
        // happiness < 1 -> a customer getting unhappy


        //Get y scale of status bar
        Vector3 statusBarScale = statusBar.transform.localScale;
        float yScale = statusBarScale.y;


        //NEW LOGIC
        if (happiness >= 1) {
            yScale = yScale + GameSettings.Instance.growthVel * Time.deltaTime;
            yScale = Mathf.Min(yScale, 1);
        }
        else
        {
            yScale = yScale - GameSettings.Instance.dropVel * Time.deltaTime;
            yScale = Mathf.Max(yScale, 0);
        }
        statusBarScale.y = yScale;
        statusBar.transform.localScale = statusBarScale;

        //////OLD LOGIC
        //if (efficiency >= 1)
        //{
        //    happiness = 1;
        //}
        //else
        //{
        //    // efficiency: 0..1 (bad..good)
        //    float dropRate = 1 - efficiency;
        //    happiness = happiness - dropRate * Time.deltaTime * panicFactor;
        //}

        //for (int i = 0; i < connectedLines.Count; i++)
        //{
        //    connectedLines[i].GetComponent.transform{ Material.}

        //}


        //HOW TO THIN LINES?
        //if (yScale > 0) {
        //float timeSinceLineCreated = Time.time - lineCreationTime;



        //Remove house and connected lines if happiness falls to 0 or below
        if (yScale <= 0) {
            // House dies
            Destroy(gameObject);
            // Connected lines die
            for (int i = 0; i < connectedLines.Count; i++) {
                Destroy(connectedLines[i].gameObject);
            }
        }



        // OLD LOGIC 
        //Set building status bar height according to happiness
        //Vector3 statusBarScale = statusBar.transform.localScale;
        //statusBarScale.y = happiness;
        //statusBar.transform.localScale = statusBarScale;


        // Dying building flashes red below 0.3 happiness. Goes back to white above 0.3 happiness.
        if (statusBarScale.y < 0.3) {
            if (Time.time % 0.5 < 0.25) {
                statusBar.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
            else {
                statusBar.GetComponentInChildren<Renderer>().material.color = Color.white;
            }
        }
        else {
            statusBar.GetComponentInChildren<Renderer>().material.color = Color.white;
        }


    }


}
