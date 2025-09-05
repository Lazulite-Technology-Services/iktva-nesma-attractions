using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private OSC oscObject;

    //[SerializeField] private UdpManager udpManagerInstance;
   

    private void Awake()
    {
        Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("ip"))
        {
            //Debug.Log("control is above Ip assign");
            oscObject.outIP = PlayerPrefs.GetString("ip");
            //Debug.Log("control is after Ip assign");
            //Debug.Log("control is before startIP " + oscObject.outIP);
            OSC.INSTANCE.StartIP();
            //Debug.Log("control is after startIP " + oscObject.outIP);
        }
        else
        {
            Debug.Log("ip playerpref doesnt exist");
        }
    }

    void  Init()
    {
        instance = this;

        
            
        //udpManagerInstance.remoteIP = PlayerPrefs.GetString("ip");
    }

    public void SaveIP(TMP_InputField ip)
    {
        Debug.Log(ip);
        PlayerPrefs.SetString("ip", ip.text);
        oscObject.outIP = PlayerPrefs.GetString("ip");

        OSC.INSTANCE.StartIP();
        //udpManagerInstance.remoteIP = PlayerPrefs.GetString("ip");
    }

    public void SendMessageToClient(string msg)
    {
        //udpManagerInstance.SendMessage(msg);
    }
}


