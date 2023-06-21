using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System;

public class ipc_connect : MonoBehaviour
{
    TcpListener listener;
    TcpClient client;
    NetworkStream stream;
    private Thread clientReceiveThread; 	
    byte[] buffer;

    // Start is called before the first frame update
    void Start() {  
        //   connectToServer();
    }

    // Update is called once per frame
    void Update() {
        // this.ReadFromPipe();
    }

    public void connectToServer(out String receivedData) {
        try {  	
            String value = null;
			clientReceiveThread = new Thread(() => {
                listenForData(out value);
            }); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start(); 
            
 	
            receivedData = value;
            Debug.Log("client message received as: " + receivedData); 	

		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e);
            receivedData = null; 	
		}
    }

    public void listenForData(out String value) {
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
                            value = clientMessage;
						} 
                    }
                }
            }
        } catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 
		}   
        value = "";
    }
}
