using UnityEngine;						
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour
{
    bool socketReady = false;               // global variables are setup here
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "192.168.17.157";
    public Int32 Port = 5001;

    void Start()
    {
        setupSocket();         // setup the server connection when the program starts
    }

    void Update()
    {
        while (theStream.DataAvailable)   // if new data is recieved from Arduino
        {               
            string recievedData = readSocket();       // write it to a string

            if (recievedData == "Light on")
            {
                Debug.Log("Received message from ESP32: Light is on");
            }

            if (recievedData == "Light off")
            {
                Debug.Log("Received message from ESP32: Light is off");
            }
        }
    }

    public void setupSocket()       // Socket setup here
    {                           
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);        // catch any exceptions
        }
    }

    public void writeSocket(string theLine)        // function to write data out
    {           
        if (!socketReady)
            return;
        String tmpString = theLine;
        theWriter.Write(tmpString);
        theWriter.Flush();
    }

    public String readSocket()             // function to read data in
    {                       
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "NoData";
    }

    public void closeSocket()       // function to close the socket
    {                           
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    public void maintainConnection() // function to maintain the connection
    {                   
        if (!theStream.CanRead)
        {
            setupSocket();
        }
    }
}
