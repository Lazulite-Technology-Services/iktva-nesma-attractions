using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private OSC oscObject;
   

    private void Awake()
    {
        Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("ip"))
        {
            oscObject.outIP = PlayerPrefs.GetString("ip");
            OSC.INSTANCE.StartIP();
        }
        else
        {
            Debug.Log("ip playerpref doesnt exist");
        }
    }

    void  Init()
    {
        instance = this;
    }

    public void SaveIP(TMP_InputField ip)
    {
        Debug.Log(ip);
        PlayerPrefs.SetString("ip", ip.text);
        oscObject.outIP = PlayerPrefs.GetString("ip");

        OSC.INSTANCE.StartIP();
    }
}


