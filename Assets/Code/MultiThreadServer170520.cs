using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public class MultiThreadServer170520 : MonoBehaviour
{

    int counter = 0;
    string IP_ADDRESS;

    void Start()
    {
        IP_ADDRESS = getIPAddress().ToString();

        try { 
        Thread magFrontThread = new Thread(new ThreadStart(magFront));
        Thread manualThread = new Thread(new ThreadStart(manual));

        magFrontThread.Start();
        magFrontThread.IsBackground = true;
        manualThread.Start();
        manualThread.IsBackground = true;
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    void Update()
    {
        Debug.Log(counter += 1);
    }


    public void magFront()
    {
        bool keepReading = false;
        int PORT = 9001;
        string data;
        Socket listener;
        Socket handler;

        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // host running the application.
        Debug.Log("Ip " + IP_ADDRESS + PORT);
        IPAddress[] ipArray = Dns.GetHostAddresses(getIPAddress());
        IPEndPoint localEndPoint = new IPEndPoint(ipArray[0], PORT);

        // Create a TCP/IP socket.
        listener = new Socket(ipArray[0].AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            while (true)
            {
                keepReading = true;

                // Program is suspended while waiting for an incoming connection.
                Debug.Log("Waiting for Connection on " + PORT.ToString());     //It works

                handler = listener.Accept();
                Debug.Log("Client Connected");     //It doesn't work
                data = null;

                // An incoming connection needs to be processed.
                while (keepReading)
                {
                    bytes = new byte[17];
                    int bytesRec = handler.Receive(bytes);
                    Debug.Log("Received from Server");

                    if (bytesRec <= 0)
                    {
                        keepReading = false;
                        handler.Disconnect(true);
                        break;
                    }

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Debug.Log(data);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(1);
                }

                System.Threading.Thread.Sleep(1);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void manual()
    {
        bool keepReading = false;
        int PORT = 9002;
        string data;
        Socket listener;
        Socket handler;

        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // host running the application.
        Debug.Log("Ip " + IP_ADDRESS + PORT);
        IPAddress[] ipArray = Dns.GetHostAddresses(getIPAddress());
        IPEndPoint localEndPoint = new IPEndPoint(ipArray[0], PORT);

        // Create a TCP/IP socket.
        listener = new Socket(ipArray[0].AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            while (true)
            {
                keepReading = true;

                // Program is suspended while waiting for an incoming connection.
                Debug.Log("Waiting for Connection on " + PORT.ToString());     //It works

                handler = listener.Accept();
                Debug.Log("Client Connected");     //It doesn't work
                data = null;

                // An incoming connection needs to be processed.
                while (keepReading)
                {
                    bytes = new byte[17];
                    int bytesRec = handler.Receive(bytes);
                    Debug.Log("Received from Server");

                    if (bytesRec <= 0)
                    {
                        keepReading = false;
                        handler.Disconnect(true);
                        break;
                    }

                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Debug.Log(data);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(1);
                }

                System.Threading.Thread.Sleep(1);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    private string getIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }

        }
        return localIP;
    }
}

