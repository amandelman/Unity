using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialControl : MonoBehaviour
{
    //declare a variable for the MoveLeftRight script to it's accessible
    public MoveLeftRight mlscript;

    //set the port equal to a variable
    string portName = "/dev/cu.usbmodem1412";

    //declare serial port variable
    public SerialPort port;


    //function for reading serial data
    public string ReadFromArduino(int timeout = 20)
    {
        port.ReadTimeout = timeout;
        try
        {
            return port.ReadLine();
        }
        catch (System.TimeoutException e)
        {
            return null;
        }
    }

    // Use this for initialization
    void Start()
    {
        List<string> portNames = new List<string>();
        portNames.AddRange(System.IO.Ports.SerialPort.GetPortNames());
        portNames.AddRange(System.IO.Directory.GetFiles("/dev/", "cu.*"));
        string[] ports = portNames.ToArray();
        //Debug.Log(portNames.Count + "available ports: \n" + string.Join("\r\n", portNames.ToArray()));

        Debug.Log("Serial ports:");
        foreach (string str in ports)
        {
            Debug.Log(str);
        }
        Debug.Log("done");

        //set a port
        port = new SerialPort(portName, 115200); //microbit
        port.ReadTimeout = 50;
        port.Open();

    }

    // Update is called once per frame
    void Update()
    {
        string message = ReadFromArduino();

        //make it so the null messages don't appear in the console and waste my time
        if (message != null)
        {
            Debug.Log(message);
            //trim the message so that all the ASCII garbage doesn't fuck it up
            if (message.Trim().Equals("left"))
            {
                mlscript.MoveLeft();
            }

            if (message.Trim().Equals("right"))
            {
                mlscript.MoveRight();
            }
        }
    }
}
