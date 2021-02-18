using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class magFrontClient : MonoBehaviour
{
    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    public string locationData;
    public string[] sepData;
    #endregion
    // Use this for initialization 	
    void Start()
    {
        ConnectToTcpServer();
    }
    // Update is called once per frame
    void Update()
    {
        //************** Transferline Msg Struct **************//
        //Station Name, start, middle, release, end, carrier ID
        if (GameObject.Find("Main Camera").GetComponent<runInSimMode>().simMode == false)
        {
            parseLocationData();
        }
    }
    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        string old = "";
        try
        {
            socketConnection = new TcpClient("10.14.129.55", 5555);
            Byte[] bytes = new Byte[11];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        locationData = serverMessage;

                        if (locationData.Equals(old) == false)
                        {
                            Debug.Log(locationData);
                            old = locationData;
                            //Debug.Log(locationData);
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    private void SendMessage()
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "This is a message from one of your clients.";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }


    void parseLocationData()
    {
        sepData = locationData.Split(',');

        if (sepData[0].Equals("1"))
        {
            if (sepData[1].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStartInduction = true;
            }
            if (sepData[1].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStartInduction = false;
            }

            if (sepData[2].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStopInduction = true;
            }
            if (sepData[2].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontStopInduction = false;
            }

            if (sepData[3].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontCarrierRelease = true;
            }
            if (sepData[3].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontCarrierRelease = false;
            }

            if (sepData[4].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontEndInduction = true;
            }
            if (sepData[4].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().magFrontEndInduction = false;
            }
        }

        if (sepData[0].Equals("2"))
        {
            if (sepData[1].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStartInduction = true;
            }
            if (sepData[1].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStartInduction = false;
            }

            if (sepData[2].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStopInduction = true;

            }
            if (sepData[2].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualStopInduction = false;
            }

            if (sepData[3].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualCarrierRelease = true;
                if (sepData[5] == "1")
                {
                    GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualRFID = 1;
                }
                if (sepData[5] == "2")
                {
                    GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualRFID = 2;
                }
                if (sepData[5] == "3")
                {
                    GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualRFID = 3;
                }
                if (sepData[5] == "4")
                {
                    GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualRFID = 4;
                }
            }
            if (sepData[3].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualCarrierRelease = false;
            }

            if (sepData[4].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualEndInduction = true;
            }
            if (sepData[4].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().manualEndInduction = false;
            }
        }

        if (sepData[0].Equals("3"))
        {
            if (sepData[1].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStartInduction = true;
            }
            if (sepData[1].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStartInduction = false;
            }

            if (sepData[2].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStopInduction = true;
            }
            if (sepData[2].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectStopInduction = false;
            }

            if (sepData[3].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectCarrierRelease = true;
            }
            if (sepData[3].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectCarrierRelease = false;
            }

            if (sepData[4].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectEndInduction = true;
            }
            if (sepData[4].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().camInspectEndInduction = false;
            }
        }


        if (sepData[0].Equals("7"))
        {
            if (sepData[1].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StartInduction = true;
            }
            if (sepData[1].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StartInduction = false;
            }

            if (sepData[2].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = true;
            }
            if (sepData[2].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1StopInduction = false;
            }

            if (sepData[3].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1CarrierRelease = true;
            }
            if (sepData[3].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1CarrierRelease = false;
            }

            if (sepData[4].Equals("1"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1EndInduction = true;
            }
            if (sepData[4].Equals("0"))
            {
                GameObject.Find("Main Camera").GetComponent<runInSimMode>().codesys1EndInduction = false;
            }
        }
    }
}
