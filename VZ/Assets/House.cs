using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a comment

public class House : MonoBehaviour {

    public float growthRatePerSecond = 1.1f; // in seconds
    public float panicFactor = 0.3f;

    public float demand = 0.001f;
    public float networkCapacity = 0.001f;

    public List<LineDrawingController> connectedLines = new List<LineDrawingController>();

    public GameObject statusBar;

    float creationTime;

    float efficiency; //
    float happiness = 1;

	// Use this for initialization
	void Start () {
        creationTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        float timeSinceHouseCreation = Time.time - creationTime;
        demand = Mathf.Pow(timeSinceHouseCreation, growthRatePerSecond);

        // this value can be higher than 1 but never less than zero
        // target is always efficiency >= 1 -> happy customer!
        // efficiency < 1 -> a customer getting unhappy
        efficiency = networkCapacity / demand;
        //Debug.Log(efficiency);

        if (efficiency>=1) {
            happiness = 1;
        } else {
            // efficiency: 0..1 (bad..good)
            float dropRate = 1 - efficiency;
            happiness = happiness - dropRate * Time.deltaTime * panicFactor;
        }

        if (happiness <= 0) {
            // HOUSE DIES
            Destroy(gameObject);

            for (int i = 0; i < connectedLines.Count; i++){
                Destroy(connectedLines[i].gameObject);
            }
        }


        Vector3 statusBarScale = statusBar.transform.localScale;
        statusBarScale.y = happiness;
        statusBar.transform.localScale = statusBarScale;
    }
}
