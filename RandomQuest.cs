using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomQuest : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI scoreText;
    public RectTransform hpBar;
    public Button[] noteButtons;
    public Button sDoButton;  
    public Button fDoButton; 
    public GameObject gameOverText;
    public GameObject canvasUI;

    public GameObject trebleClefImage;
    public GameObject bassClefImage; 

    private string[] notes = { "Do", "Re", "Mi", "Fa", "Sol", "La", "Ti" };
    private string currentNote = "";
    private bool isTrebleDo = false; 
    private int score = 0;
    private int hp = 5;
    private float maxHpWidth;
    private Vector2 hpBarStartPos;
    private bool canClick = true;
    private bool isGameOver = false;

    public GameAssistant gameAssistant;
    public SoundManager soundManager;

    public Button homeButton;
    public Button restartButton;
    public GameManager gameManager;

    void Start()
    {
        gameAssistant = FindObjectOfType<GameAssistant>();
        soundManager = FindObjectOfType<SoundManager>();
        maxHpWidth = hpBar.rect.width;
        hpBarStartPos = hpBar.anchoredPosition;
        gameOverText.SetActive(false);
        trebleClefImage.SetActive(false);
        bassClefImage.SetActive(false);
        StartCoroutine(RandomTextLoop());
        SetupButtons();
        UpdateUI();
        homeButton.onClick.AddListener(() => gameManager.GoToHome());
        restartButton.onClick.AddListener(() => gameManager.RestartGame());
    }

    IEnumerator RandomTextLoop()
    {
        while (!isGameOver)
        {
            currentNote = notes[Random.Range(0, notes.Length)];
            textMesh.text = currentNote;
            canClick = true;

           
            UpdateClefUI(currentNote);

            float timer = 3f;
            while (timer > 0 && canClick)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            if (canClick)
            {
                hp--;
                UpdateHPBar();
                if (hp <= 0) GameOver();
            }
            canClick = false;
        }
    }

    void UpdateClefUI(string note)
    {
        trebleClefImage.SetActive(false);
        bassClefImage.SetActive(false);
        isTrebleDo = false;

        if (note == "Re" || note == "Mi" || note == "Fa")
        {
            bassClefImage.SetActive(true); 
        }
        else if (note == "Sol" || note == "La" || note == "Ti")
        {
            trebleClefImage.SetActive(true); 
        }
        else if (note == "Do")
        {   
            if (Random.value < 0.5f)
            {
                bassClefImage.SetActive(true);
                isTrebleDo = false; 
            }
            else
            {
                trebleClefImage.SetActive(true);
                isTrebleDo = true; 
            }
        }
    }

    void SetupButtons()
    {
        for (int i = 0; i < noteButtons.Length; i++)
        {
            string note = notes[i];
            noteButtons[i].onClick.AddListener(() => CheckAnswer(note));
        }

        sDoButton.onClick.AddListener(() => CheckAnswer("S_Do"));
        fDoButton.onClick.AddListener(() => CheckAnswer("F_Do"));
    }

    public void CheckAnswer(string userInput)
    {
        if (!canClick || isGameOver) return;
        canClick = false;

        bool isCorrect = false;

        if (currentNote == "Do")
        {
            if ((isTrebleDo && userInput == "S_Do") || (!isTrebleDo && userInput == "F_Do"))
            {
                isCorrect = true;
            }
        }
        else
        {
            if (userInput == currentNote)
            {
                isCorrect = true;
            }
        }
        soundManager.PlaySound(userInput);
        if (isCorrect)
        {
            score++;
            gameAssistant.ShowCorrectFeedback();
            gameAssistant.UpdateSpeedBar(score); 
        }
        else
        {
            hp--;
            UpdateHPBar();
            gameAssistant.ShowIncorrectFeedback();

            if (hp <= 0) GameOver();
        }
        soundManager.CheckScoreAndSwitchMusic(score);
        UpdateUI();
    }



    void UpdateUI()
    {
        scoreText.text = "Combo " + score;
    }

    public void AddScoreFromAd()
    {
        score += 100;
        gameAssistant.UpdateSpeedBar(score);
        UpdateUI();
    }


    void UpdateHPBar()
    {
        float newWidth = (hp / 5f) * maxHpWidth;
        float offsetX = (maxHpWidth - newWidth) / 2f;

        hpBar.sizeDelta = new Vector2(newWidth, hpBar.sizeDelta.y);
        hpBar.anchoredPosition = new Vector2(hpBarStartPos.x - offsetX, hpBar.anchoredPosition.y);
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
        canvasUI.SetActive(false);
    }
}
