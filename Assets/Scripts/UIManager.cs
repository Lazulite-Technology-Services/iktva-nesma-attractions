using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    [SerializeField] private GameObject commandPanel;

    [SerializeField] private Button startButton, home, save, close, commandButton;

    [SerializeField] private Button[] videoButtons;

    private int currenIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    void Init()
    {
        startButton.onClick.AddListener(()=> EnableOrDisableScreen(0,1));
        home.onClick.AddListener(()=> EnableOrDisableScreen(1,0));
        
        commandButton.onClick.AddListener(EnableDisableCommandPanel);
        close.onClick.AddListener(EnableDisableCommandPanel);

        //for (int i = 0; i < videoButtons.Length; i++)
        //{
        //    videoButtons[i].onClick.AddListener(() => GameManager.instance.SendMessageToClient(videoButtons[i].name));
        //    //videoButtons[i].onClick.AddListener(() => VideoHandler.instance.PlayVideo(videoButtons[i].GetComponent<ButtonData>().path));
        //}
    }

    void EnableOrDisableScreen(int index, int index2)
    {
        screens[index].SetActive(false);
        screens[index2].SetActive(true);
    }

    void EnableDisableCommandPanel()
    {
        commandPanel.SetActive(!commandPanel.activeSelf);
    }
}
