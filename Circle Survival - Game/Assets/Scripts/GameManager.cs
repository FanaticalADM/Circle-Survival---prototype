using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public event Action<string> onFeedbackRespond;
    public void FeedbackRespond(string feedback)
    {
        if (onFeedbackRespond != null)
            onFeedbackRespond(feedback);
    }

    public event Action onTargetClick;
    public void TargetClick()
    {
        if (onTargetClick != null)
            onTargetClick();
    }


public event Action onGameOver;
public void GameOver()
{
    if (onGameOver != null)
        onGameOver();
    Time.timeScale = 0;
}

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void GameReset()
    {
        SceneManager.LoadScene("Menu");
        ScoreManager.instance.Score = 0;
    }
}