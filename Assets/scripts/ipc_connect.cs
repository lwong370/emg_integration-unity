using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System;

public class ipc_connect : MonoBehaviour {
    TcpListener listener;
    TcpClient client;
    NetworkStream stream;
    private Thread clientReceiveThread; 	
    byte[] buffer;

    private Queue<int> bufferQueue = new Queue<int>();

    // Start is called before the first frame update
    void Start() {  
        for(int i = 0; i < 5; i++) {
            addToBuffer(bufferQueue, 0);
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public void connectToServer() {
        try {  	
			clientReceiveThread = new Thread(new ThreadStart(listenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start(); 
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e);
		}
    }

    public void listenForData() {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 1234;

        listener = new TcpListener(ipAddress, port);

        try{
            listener.Start();
            Debug.Log("Waiting for connection...");

            // Create a buffer to store received data
            buffer = new byte[1024];

            while(true) {
                using(client = listener.AcceptTcpClient()) {
                    Debug.Log("Client connected.");

                    using(stream = client.GetStream()) {
                        int length;
                        while ((length = stream.Read(buffer, 0, buffer.Length)) != 0) { 							
							var incommingData = new byte[length]; 							
							Array.Copy(buffer, 0, incommingData, 0, length);  							
							
                            // Convert byte array to string message. 							
							string clientMessage = Encoding.ASCII.GetString(incommingData); 							
							// Debug.Log("client message received as: " + clientMessage); 	
                            addToBuffer(bufferQueue, int.Parse(clientMessage));
						} 
                    }
                }
            }
        } catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 
		}   
    }

    void addToBuffer(Queue<int> queue, int value) {
        if (queue.Count >= 5) {
            queue.Dequeue();
        }
        queue.Enqueue(value);

        //Print buffer contents
        String consolePrintBuffer = "";
        foreach (var item in queue){
            consolePrintBuffer += " " + item;
        }
        UnityEngine.Debug.Log(consolePrintBuffer);
    }
}
