using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoPlayerGameManager : MonoBehaviour
{
    [SerializeField] private Button commandButton, save, close;

    [SerializeField] private GameObject commandPanel;

    [SerializeField] private TMP_InputField ipField;

    [SerializeField] private UdpManager udpManagerInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        commandButton.onClick.AddListener(EnableDisableCommandPanel);
        save.onClick.AddListener(() => SaveIP(ipField.text));
        close.onClick.AddListener(EnableDisableCommandPanel);


        ipField.text = PlayerPrefs.GetString("remoteip");
        udpManagerInstance.remoteIP = PlayerPrefs.GetString("remoteip");
    }

    public void SaveIP(string ip)
    {
        Debug.Log(ip);
        PlayerPrefs.SetString("remoteip", ip);
        udpManagerInstance.remoteIP = PlayerPrefs.GetString("ip");
    }

    public void SendMessageToClient(string msg)
    {
        udpManagerInstance.SendMessage(msg);
    }

    private void EnableDisableCommandPanel()
    {
        commandPanel.SetActive(!commandPanel.activeSelf);
        
        Debug.Log(PlayerPrefs.GetString("ip"));
    }
}