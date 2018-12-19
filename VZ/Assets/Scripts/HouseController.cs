using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{

    float HousePosX;
    public GameObject housePrefab;
    public GameObject HouseOrigin;
    public GameObject City;
    float[] Positions = new float[] { -2.2f, 2.2f, -3.3f, 3.3f };
    private int counterLoop = 0;
    private float lastSpawnTime = 0;

    float[] ResetPositions = new float[] { -1.1f, 0.0f, 1.1f };
    //private float resetTime = 0;

    // Use this for initialization
    void Start()
    {
        HousePosX = HouseOrigin.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Timeline.Instance.GameTime);

        if ((Timeline.Instance.GameTime - lastSpawnTime) > GameSettings.Instance.SpawnTime && counterLoop < (GameSettings.Instance.NumberOfHouses - 3))
        {
            GameObject House = Instantiate(housePrefab, new Vector3(HousePosX, Positions[counterLoop], 0), City.transform.rotation);
            //Set the new house's parent in the hierarchy to City
            House.transform.SetParent(City.transform);
            //Set tag for new house
            House.tag = "house";

            counterLoop++;
            lastSpawnTime = Time.time;
        }
    }

    public void ResetHouses()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject House1 = Instantiate(housePrefab, new Vector3(HousePosX, ResetPositions[i], 0), City.transform.rotation);
            House1.transform.SetParent(City.transform);
            House1.tag = "house";
        }

        counterLoop = 0;
        lastSpawnTime = Timeline.Instance.GameTime;
    }
}