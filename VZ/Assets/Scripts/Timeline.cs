using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timeline : MonoBehaviour {

    public Image progress;

    public float GameTime;
    public float lastGameTime = 0;

    public GameController gameController;

    static public Timeline Instance;

    // Use this for initialization
    void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        GameTime = Time.time;

        //Get TimeSinceStart of (re)loaded game
        float TimeLastFrame = Time.deltaTime;
        //Get GameDuration value, which is set in GameSettings Script
        //Divide the two to get a number (percentage) between 0 and 1 -> fillNumber
        float FillNumber = TimeLastFrame / GameSettings.Instance.GameDuration;

        //If time is not up yet -> fill progressbar
        if (TimeLastFrame <= GameSettings.Instance.GameDuration)
        {
            progress.fillAmount += FillNumber;
        }

        //If time is up -> Game Over
        if ((GameTime - lastGameTime) > GameSettings.Instance.GameDuration) // first: && !gameController.gameOver
        {
            Debug.Log("Call Game Over Function!");
            //gameController.GetComponent<GameController>().GameOver();
            gameController.GameOver();
            lastGameTime = GameTime;
        }
    }

    public void ResetProgressBar ()
    {
        progress.fillAmount = 0;
        lastGameTime = GameTime;
    }

}
