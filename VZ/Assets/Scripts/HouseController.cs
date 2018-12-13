using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

    float nextHouseSpawn;
    float houseY = -3.78f;
    float houseX = -2.2f;
    public GameObject housePrefab;

    // Use this for initialization
    void Start () {
        nextHouseSpawn = Time.time + 5.0f;
    }

    // Update is called once per frame
    void Update()
    { 

        if (houseX < 6)
        {
            if (Time.time > nextHouseSpawn)
            {
                // Add house
                GameObject House = Instantiate(housePrefab, new Vector3(houseX, houseY, 0), Quaternion.identity);
                nextHouseSpawn += 5.0f;
                houseX += 4.4f;
            }
        }
    }
}
