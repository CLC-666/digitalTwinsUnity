using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class testingRemoteNodeRed : MonoBehaviour
{
	string oldMessage;
	#region private members 	
	private TcpClient socketConnectionMagFront;
	private TcpClient socketConnectionManual;
	private Thread clientReceiveThreadMagFront;
	private Thread clientReceiveThreadManual;
	public string locationData;
	#endregion
	// Use this for initialization 	
	void Start()
	{
		ConnectToTcpServer();
	}
	// Update is called once per frame
	void Update()
	{

	}
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThreadMagFront = new Thread(new ThreadStart(Server));
			clientReceiveThreadMagFront.IsBackground = true;
			clientReceiveThreadMagFront.Start();
			//clientReceiveThreadManual = new Thread(new ThreadStart(ListenManual));
			//clientReceiveThreadManual.IsBackground = true;
			//clientReceiveThreadManual.Start();

		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void Server()
	{
		try
		{
			Debug.Log("hello");
			socketConnectionMagFront = new TcpClient("192.168.1.61", 9991);
			Debug.Log("hello1");
			Byte[] bytes = new Byte[17];
			Debug.Log("hello2");

			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnectionMagFront.GetStream())
				{
					Debug.Log("hello3");
					int length;
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						Debug.Log("hello4");
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// Convert byte array to string message. 		
						Debug.Log("hello5");
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						//if (serverMessage != oldMessage)
						//{
						locationData = serverMessage;
						//}
						//oldMessage = serverMessage;
						Debug.Log(locationData);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}
