using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameAssistant : MonoBehaviour
{
    public GameObject Q1T;
    public GameObject Q1F;
    public RectTransform speedBar;

    private float maxSpeedWidth = 100f;
    private Vector2 speedBarStartPos;

    void Start()
    {
        Q1T.SetActive(false);
        Q1F.SetActive(false);
        speedBar.sizeDelta = new Vector2(0, speedBar.sizeDelta.y);

        speedBarStartPos = speedBar.anchoredPosition;
    }

    public void ShowCorrectFeedback()
    {
        StartCoroutine(ShowImageForSeconds(Q1T, 0.25f));
    }

    public void ShowIncorrectFeedback()
    {
        StartCoroutine(ShowImageForSeconds(Q1F, 0.25f));
    }

    IEnumerator ShowImageForSeconds(GameObject image, float duration)
    {
        image.SetActive(true);
        yield return new WaitForSeconds(duration);
        image.SetActive(false);
    }

    public void UpdateSpeedBar(int score)
    {
        float newWidth = Mathf.Clamp((score / 25f) * maxSpeedWidth, 0, maxSpeedWidth);
        speedBar.sizeDelta = new Vector2(newWidth, speedBar.sizeDelta.y);

        float offsetX = newWidth / 2f;
        speedBar.anchoredPosition = new Vector2(speedBarStartPos.x + offsetX, speedBar.anchoredPosition.y);
    }
}



