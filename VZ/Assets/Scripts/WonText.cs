using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WonText : MonoBehaviour
{

    private TextMeshProUGUI Text;
    private int housesLeftPercentage;

    // Use this for initialization
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameController.Instance.CallWonText)
        {
            Text.text = "You made it to 2030 and kept over " + "\n" + GameController.Instance.housesLeftPercentage + "% of your customers!";
        }
    }

    //public void UpdateText()
    //{
    //    Text.text = "You made it to 2030 and kept over " + "\n" + GameController.Instance.housesLeftPercentage + "% of your customers!";
    //}
}
