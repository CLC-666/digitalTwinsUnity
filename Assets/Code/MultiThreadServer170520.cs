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
    Thread manualThread;
    Thread magFrontThread;
    int counter = 0;
    string IP_ADDRESS;

    public string magFData;
    public string manData;
    public string camData;
    public string code1Data;
    public string code2Data;
    public string magBData;
    public string pressData;
    public string heatData;

    void Start()
    {
        IP_ADDRESS = getIPAddress().ToString();

        try
        {
            //magFrontThread = new Thread(new ThreadStart(magFront));
            //manualThread = new Thread(new ThreadStart(manual));
            //Thread CameraThread = new Thread(new ThreadStart(camInspect));
            //Thread codesys1Thread = new Thread(new ThreadStart(codesys1));
            //Thread codesys2Thread = new Thread(new ThreadStart(codesys2));
            Thread magBackThread = new Thread(new ThreadStart(magBack));
            Thread pressingThread = new Thread(new ThreadStart(pressing));
            Thread heatingThread = new Thread(new ThreadStart(heating));


            //magFrontThread.Start();
            //magFrontThread.IsBackground = true;
            //manualThread.Start();
            //manualThread.IsBackground = true;
            //CameraThread.Start();
            //CameraThread.IsBackground = true;
            //codesys1Thread.Start();
            //codesys1Thread.IsBackground = true;
            //codesys2Thread.Start();
            //codesys2Thread.IsBackground = true;
            magBackThread.Start();
            magBackThread.IsBackground = true;
            pressingThread.Start();
            pressingThread.IsBackground = true;
            heatingThread.Start();
            heatingThread.IsBackground = true;




        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    void Update()
    {

    }




    public void magFront()
    {
        while(true) {
            bool keepReading = false;
            int PORT = 9001;
            string data;
            string old = "";
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
                    Debug.Log("Make sure your firewall is turned off LOL");
                    handler = listener.Accept();
                    Debug.Log("Client Connected");     //It doesn't work
                    data = null;

                    // An incoming connection needs to be processed.
                    while (keepReading)
                    {
                        bytes = new byte[17];
                        int bytesRec = handler.Receive(bytes);
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("MagFront" + data);
                            old = data;
                            magFData = data;
                        }

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
    }


    public void manual()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9002;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("Manual" + data);
                            old = data;
                            manData = data;
                        }
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
    }

    public void camInspect()
    {
        while(true) {
            bool keepReading = false;
            int PORT = 9003;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("camInspect" + data);
                            old = data;
                            camData = data;
                        }

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
    }

    public void codesys1()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9997;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("codesys1" + data);
                            old = data;
                            code1Data = data;
                        }

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
    }

    public void codesys2()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9005;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("codesys2" + data);
                            old = data;
                            code2Data = data;
                        }

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
    }

    public void magBack()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9006;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("magBack" + data);
                            old = data;
                            magBData = data;
                        }
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
    }

    public void pressing()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9007;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("pressing" + data);
                            old = data;
                            pressData = data;
                        }

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
    }

    public void heating()
    {
        while (true)
        {
            bool keepReading = false;
            int PORT = 9008;
            string data;
            string old = "";
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
                        //Debug.Log("Received from Server");

                        if (bytesRec <= 0)
                        {
                            keepReading = false;
                            handler.Disconnect(true);
                            break;
                        }

                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (data.Equals(old) == false)
                        {
                            //Debug.Log("heating" + data);
                            old = data;
                            heatData = data;
                        }

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

