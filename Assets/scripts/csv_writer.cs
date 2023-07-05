using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using data;

public class csv_writer : MonoBehaviour
{
    string fileDirectory = "";
    string filename = "";
    string filePath = "";

    public void writeCSV(DataPoint dp) {

        fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        filename = "bear_data.csv";
        filePath = Path.Combine(fileDirectory, filename);

        using (StreamWriter s = new StreamWriter(filePath, true)) {
            s.WriteLine("Timestamp, Majority Output");
            s.WriteLine(dp.timeStamp + "," + dp.majority);
        }

        //Look up how to close StreamWriter and when.
    }
}
