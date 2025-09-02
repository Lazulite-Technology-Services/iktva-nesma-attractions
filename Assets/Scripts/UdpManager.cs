using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Concurrent;

public class UdpManager : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;

    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();

   
    public string remoteIP;
    public int remotePort = 8000;
    public int localPort = 9000;

    void Start()
    {
        udpClient = new UdpClient(localPort);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void Update()
    {
        while (messageQueue.TryDequeue(out string msg))
        {
            HandleCommand(msg);
        }
    }

    void ReceiveData()
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                Debug.Log("Received: " + text);
                
                messageQueue.Enqueue(text);
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }

    public void HandleCommand(string msg)
    {
        StartCoroutine(VideoHandler.instance.PlayVideo(msg));
    }

    public void SendMessage(string message)
    {
        Debug.Log(message);
        UdpClient sender = new UdpClient();
        byte[] data = Encoding.UTF8.GetBytes(message);
        sender.Send(data, data.Length, remoteIP, remotePort);
        sender.Close();
    }

    void OnApplicationQuit()
    {
        receiveThread?.Abort();
        udpClient?.Close();
    }
}