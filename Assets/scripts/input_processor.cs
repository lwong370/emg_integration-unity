using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class input_processor : MonoBehaviour
{
    private ipc_connect tcp;
    private Queue<int> buffer;
    String receivedValue;

    // Start is called before the first frame update
    void Start() {

        tcp = new ipc_connect();
        buffer = new Queue<int>();
        receivedValue="";
        
        for(int i = 0; i < 5; i++) {
            addToBuffer(buffer, 0);
        }
        tcp.connectToServer(out receivedValue);
        runBuffer(receivedValue);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void runBuffer(String data) {
        UnityEngine.Debug.Log("Testing");
     
        UnityEngine.Debug.Log("Testing value: " + data);
        // addToBuffer(buffer, int.Parse(data));

        // //Print buffer contents
        // String consolePrintBuffer = "";
        // foreach (var item in buffer){
        //     consolePrintBuffer += ", " + item;
        // }
        // UnityEngine.Debug.Log(consolePrintBuffer);
        
    }

    void addToBuffer(Queue<int> buffer, int value) {
        if (buffer.Count >= 5) {
            buffer.Dequeue();
        }

        buffer.Enqueue(value);
    }

}
