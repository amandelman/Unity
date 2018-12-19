using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    static public GameController Instance;
    
    public GameObject StartButton, RestartButton;

    public bool gameStart;
    public bool gameOver;
    public bool gameRestart;
    //Royalty Free Music from Bensound: https://www.bensound.com/royalty-free-music/track/sci-fi
    public AudioSource MainMusic;
    public AudioSource IntroMusic;

    public int destroyedHouses = 0;

    public GameObject City;

    public HouseController houseController;
    public Timeline timeline;

    //public WonText wonScript;
    public bool CallWonText;
    public GameObject GameWon, GameWonText, GameLost, GameLostText;
    public int housesLeftPercentage = 0;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        Reboot();
    }

    //If game is first started OR restarted > Reset everything
    //This is still bugged -> BUT WHO CARES
    void Reboot()
    {
        Debug.Log("Game (Re)loaded");
        Time.timeScale = 0;
        gameStart = false;
        gameOver = false;
        //gameRestart = false;

        GameWon.SetActive(false);
        GameWonText.SetActive(false);
        GameLost.SetActive(false);
        GameLostText.SetActive(false);
        CallWonText = false;

        StartButton.SetActive(true);
        RestartButton.SetActive(false);
        timeline.ResetProgressBar();
        destroyedHouses = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //If no houses left
        if (destroyedHouses == 7)
        {
            GameOver();
        }
    }

    //If Start button is clicked > StartGame()
    //Unpauses game (timescale 1) & hides buttons
    public void StartGame()
    {
        Debug.Log("Game started");
        gameStart = true;
        Time.timeScale = 1;

        StartButton.SetActive(false);
        RestartButton.SetActive(false);
    }

    //If Restart button is clicked > RestartGame()
    //Reloads scene/game
    public void RestartGame()
    {
        Debug.Log("Game reloaded");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        //gameRestart = true;

        DestroyAllHouses();
        houseController.ResetHouses();
        Reboot();
    }

    //Destroy all Houses that are left after gameplay
    public void DestroyAllHouses()
    {
        // All Houses die
        GameObject[] houses = GameObject.FindGameObjectsWithTag("house");
        foreach (GameObject house in houses)
        {
            // House dies
            GameObject.Destroy(house);
        }

        // All Lines die
        GameObject[] lines = GameObject.FindGameObjectsWithTag("line");
        foreach (GameObject line in lines)
        {
            // Connected line dies
            GameObject.Destroy(line);
        }
    }

    //If Time is up OR No houses left > Game Over()
    //Pauses the game (timescale 0) & Shows Restart button > if clicked RestartGame()
    public void GameOver()
    {
        //Debug.Log("Game Over");
        Time.timeScale = 0;
        gameStart = false;
        gameOver = true;
        RestartButton.SetActive(true);

        MainMusic.Stop();
        IntroMusic.Play();

        if (destroyedHouses < 7)
        {
            int HousesLeft = 7 - destroyedHouses;
            housesLeftPercentage = HousesLeft * 100 / 7;
            CallWonText = true;
            GameWon.SetActive(true);
            GameWonText.SetActive(true);
        }
        else if (destroyedHouses == 7)
        {
            GameLost.SetActive(true);
            GameLostText.SetActive(true);
        }

        //destroyedHouses = 0;
    }

}
