using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    [SerializeField] private GameObject commandPanel;

    [SerializeField] private Button startButton, home, save, close, commandButton;

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
