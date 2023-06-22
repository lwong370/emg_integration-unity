using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class input_processor : MonoBehaviour
{
    private ipc_connect tcp;

    // Start is called before the first frame update
    void Start() {
        tcp = new ipc_connect();
        tcp.connectToServer();
    }

    // Update is called once per frame
    void Update() {
        
    }

}
