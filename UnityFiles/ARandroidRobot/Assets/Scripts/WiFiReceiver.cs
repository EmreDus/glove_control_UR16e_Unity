using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

public class WiFiReceiver : MonoBehaviour
{
    // IP address and port of the ESP32 server
    public string serverIP = "192.168.1.155";
    public int serverPort = 80;

    private TcpClient client;
    private NetworkStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    public string[] values = new string[17];

    public static float qw1, qx1, qy1, qz1;
    public static float qw2, qx2, qy2, qz2;
    public static float qw3, qx3, qy3, qz3;
    public static float fng1, fng2, fng3, fng4, fng5;

    private void Start()
    {
        // Connect to the ESP32 server
        try
        {
            client = new TcpClient(serverIP, serverPort);
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            Debug.Log("Connected to ESP32 server.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to ESP32 server: " + e.Message);
        }
    }

    private void Update()
    {
        if (stream != null && stream.DataAvailable)
        {
            try
            {
                // Read the sensor data from the server
                string gloveData = reader.ReadLine();

                if (gloveData == null)
                {
                    Debug.Log("Disconnected from ESP32 server.");
                    client.Close();
                    return;
                }

                // Parse the glove data into individual values
                values = gloveData.Split('/');

                // Extract the gyroscope values and use them to rotate the target object
                float.TryParse(values[0], out qw1);
                float.TryParse(values[1], out qx1);
                float.TryParse(values[2], out qy1);
                float.TryParse(values[3], out qz1);

                float.TryParse(values[4], out qw2);
                float.TryParse(values[5], out qx2);
                float.TryParse(values[6], out qy2);
                float.TryParse(values[7], out qz2);

                float.TryParse(values[8], out qw3);
                float.TryParse(values[9], out qx3);
                float.TryParse(values[10], out qy3);
                float.TryParse(values[11], out qz3);

                float.TryParse(values[12], out fng1);
                float.TryParse(values[13], out fng2);
                float.TryParse(values[14], out fng3);
                float.TryParse(values[15], out fng4);
                float.TryParse(values[16], out fng5);
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading data from ESP32 server: " + e.Message);
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }

    void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }
    }
}