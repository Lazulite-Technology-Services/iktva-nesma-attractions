using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class UdpManager : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;

    public string remoteIP = "192.168.1.100";
    public int remotePort = 9000;
    public int localPort = 8000;

    void Start()
    {
        udpClient = new UdpClient(localPort);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
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

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    HandleCommand(text);
                });
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }

    public void HandleCommand(string msg)
    {
        if (msg == "Play") PlayAnimation();
        else if (msg == "Stop") StopAnimation();
    }

    public void SendMessage(string message)
    {
        UdpClient sender = new UdpClient();
        byte[] data = Encoding.UTF8.GetBytes(message);
        sender.Send(data, data.Length, remoteIP, remotePort);
        sender.Close();
    }

    void PlayAnimation()
    {
        Debug.Log("🎬 Play animation command received");
    }

    void StopAnimation()
    {
        Debug.Log("⏹️ Stop animation command received");
    }

    void OnApplicationQuit()
    {
        receiveThread?.Abort();
        udpClient?.Close();
    }
}