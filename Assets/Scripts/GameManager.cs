using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UdpManager udpManagerInstance;
   

    private void Awake()
    {
        Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void  Init()
    {
        instance = this;
        udpManagerInstance.remoteIP = PlayerPrefs.GetString("ip");
    }

    public void SaveIP(TMP_InputField ip)
    {
        Debug.Log(ip);
        PlayerPrefs.SetString("ip", ip.text);
        udpManagerInstance.remoteIP = PlayerPrefs.GetString("ip");
    }

    public void SendMessageToClient(string msg)
    {
        udpManagerInstance.SendMessage(msg);
    }
}
