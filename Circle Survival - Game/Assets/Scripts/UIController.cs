using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject hud;
    [SerializeField]
    private GameObject feedbackText;
    [SerializeField]
    private GameObject scoreText;
    private TextMeshProUGUI scoreTMP;
    [SerializeField]
    private GameObject timeText;
    private TextMeshProUGUI timeTMP;
    [SerializeField]
    private GameObject gameoverText;
    [SerializeField]
    private GameObject gameoverBackground;
    [SerializeField]
    private GameObject highScoreText;
    private TextMeshProUGUI highScoreTMP;
    [SerializeField]
    private GameObject menuButton;

    private float time;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        Setup();

        scoreTMP = scoreText.GetComponent<TextMeshProUGUI>();
        timeTMP = timeText.GetComponent<TextMeshProUGUI>();
        highScoreTMP = highScoreText.GetComponent<TextMeshProUGUI>();

        ScoreManager.instance.onScoreChange += ScoreValueUpdate;
        GameManager.instance.onGameOver += ShowGameOverScreen;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
    }

    private void ScoreValueUpdate()
    {
        scoreTMP.text = ScoreManager.instance.Score.ToString();
    }

    private void ShowGameOverScreen()
    {
        gameoverBackground.SetActive(true);
        gameoverText.SetActive(true);
        menuButton.SetActive(true);
        highScoreText.SetActive(true);
        hud.SetActive(false);

        if (ScoreManager.instance.NewHighScore)
        {
            highScoreTMP.text = $"NEW HIGH SCORE: {ScoreManager.instance.Score}";
        }
        else
        {
            highScoreTMP.text = $"YOUR SCORE: {ScoreManager.instance.Score}";
        }
    }

    private void TimeUpdate()
    {
        time += Time.deltaTime;
        timeTMP.text = Mathf.Round(time).ToString();
    }

    private void Setup()
    {
        startTime = Time.realtimeSinceStartup;
        feedbackText.SetActive(true);
        hud.SetActive(true);
        gameoverBackground.SetActive(false);
        gameoverText.SetActive(false);
        highScoreText.SetActive(false);
        menuButton.SetActive(false);
    }

    private void OnDestroy()
    {
        ScoreManager.instance.onScoreChange -= ScoreValueUpdate;
        GameManager.instance.onGameOver -= ShowGameOverScreen;
    }

}
