using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBehaviour : MonoBehaviour
{
    [SerializeField] private Image logo;

    [SerializeField] private Image[] buttons;

    private float tweenSpeed = 0.15f;

    private void OnEnable()
    {
        StartCoroutine(AnimateElements());
    }

    
    IEnumerator AnimateElements()
    {
        logo.DOColor(new Color(1, 1, 1, 1), tweenSpeed);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CanvasGroup>().alpha = 1;
            buttons[i].transform.DOScale(1.15f, tweenSpeed).SetEase(Ease.InOutSine).SetEase(Ease.InOutSine).OnComplete(OnTweenComplete);
            yield return new WaitForSeconds(0.25f);
        }
    }

    void OnTweenComplete()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.DOScale(1f, tweenSpeed).SetEase(Ease.InOutSine);
        }
    }

    private void OnDisable()
    {
        logo.DOColor(new Color(1, 1, 1, 0), tweenSpeed);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CanvasGroup>().alpha = 0;
            buttons[i].transform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}
