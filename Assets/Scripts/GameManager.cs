using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string ip;

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
    }

    public void SaveIP()
    {
        PlayerPrefs.SetString("ip", ip);
    }

    public void SendMessageToClient(string msg)
    {
        udpManagerInstance.SendMessage(msg);
    }
}
