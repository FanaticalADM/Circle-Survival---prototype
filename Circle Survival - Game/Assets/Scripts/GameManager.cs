using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public event Action<float> onFeedbackRespond;
    public void FeedbackRespond(float responseTime)
    {
        if (onFeedbackRespond != null)
            onFeedbackRespond(responseTime);
    }

    public event Action<Transform> onTargetClick;
    public void TargetClick(Transform newposition)
    {
        if (onTargetClick != null)
            onTargetClick(newposition);
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
        ScoreManager.Instance.Score = 0;
    }
}
