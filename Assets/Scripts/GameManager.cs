using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    private string ip;

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
}
