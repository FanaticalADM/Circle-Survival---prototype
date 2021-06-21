using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    public static ScoreManager Instance
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
        DontDestroyOnLoad(this.gameObject);
    }

    private string highScoreSaveName = "highscore";
    public string HighScoreSaveName { get { return highScoreSaveName; } }

    [SerializeField]
    private int score = 0;
    public int Score { get { return score; } set { score = value; } }

    private int highScore;
    public int HighScore { get { return highScore; } set { highScore = value; } }

    private bool newHighScore;
    public bool NewHighScore => newHighScore;

    public event Action onScoreChange;
    public void ScoreChange(int addscore)
    {
        score += addscore;
        if (onScoreChange != null)
            onScoreChange();

        if (score >= highScore)
        {
            highScore = score;
            newHighScore = true;
            PlayerPrefs.SetInt(highScoreSaveName, highScore);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreSaveName);
        newHighScore = false;
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(highScoreSaveName, 0);
        highScore = 0;
    }

    
}
