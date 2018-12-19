using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LostText : MonoBehaviour {

    private TextMeshProUGUI Text;

    // Use this for initialization
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Text.text = "You didn't make it to 2030," + "\n" + "you lost all of your customers.";
    }
}
