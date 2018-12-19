using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    //Create a place in the Unity editor to change various game settings
    static public GameSettings Instance;

    public float dropVel = 0.3f; // units/sec
    public float growthVel = 0.3f; // units/sec
    public float networkCapacityIncrease = 3;
    public float growthRatePerSecond = 1.1f; // in seconds
    public float demand = 0.01f;
    public float initialNetworkCapacity = 0.001f;
    public float GameDuration = 4; // in seconds
    public float lineWidth = 0.3f;
    public int NumberOfHouses = 7;
    public int SpawnTime = 5;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
