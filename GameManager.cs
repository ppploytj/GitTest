using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvasHomeGame;
    public GameObject canvasMenuGame;
    public GameObject canvasGameStory1;
    public GameObject canvasGameStory2;
    public GameObject canvasGameStory3;
    public GameObject canvasOnGame;
    public GameObject GameLogo;
    public GameObject gameOverText;
    public Button startButton;
    public Button playButton;
    public Button hallButton;
    public Button bt1;
    public Button bt2;
    public Button bt3;
    public TextMeshProUGUI countdownText;
    public RandomQuest randomQuest;

    public AudioSource audioSource;
    public AudioClip countdownBeep;

    void Start()
    {
        canvasHomeGame.SetActive(true);
        canvasMenuGame.SetActive(false);
        canvasGameStory1.SetActive(true);
        canvasGameStory2.SetActive(true);
        canvasGameStory3.SetActive(true);
        canvasOnGame.SetActive(false);

        countdownText.gameObject.SetActive(false);
        randomQuest.enabled = false;
        gameOverText.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        playButton.onClick.AddListener(GoToMenu);
        hallButton.onClick.AddListener(CloseMenu);

        bt1.onClick.AddListener(() => canvasGameStory1.SetActive(false));
        bt2.onClick.AddListener(() => canvasGameStory2.SetActive(false));
        bt3.onClick.AddListener(() => canvasGameStory3.SetActive(false));
    }

    public void StartGame()
    {
        GameLogo.SetActive(false);
        startButton.gameObject.SetActive(false);

        StartCoroutine(CountdownAndStart());
    }

    IEnumerator CountdownAndStart()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            audioSource.PlayOneShot(countdownBeep);
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        canvasOnGame.SetActive(true);
        randomQuest.enabled = true;
    }

    public void GoToMenu()
    {
        canvasHomeGame.SetActive(false);
        canvasMenuGame.SetActive(true);
    }

    public void CloseMenu()
    {
        canvasMenuGame.SetActive(false);
    }

    public void GoToHome()
    {
        canvasHomeGame.SetActive(true);
        canvasOnGame.SetActive(false);
        gameOverText.SetActive(false);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}


