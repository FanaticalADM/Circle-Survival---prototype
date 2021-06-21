using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject highScoreText;
    private TextMeshProUGUI highScoreTMP;

    // Start is called before the first frame update
    void Start()
    {
        highScoreTMP = highScoreText.GetComponent<TextMeshProUGUI>();
        HighScoreSetup();
    }

    // Play button
    public void Play()
    {
        SceneManager.LoadScene("Game Scene");
    }

    // High Score button
    public void DeleteHighScore()
    {
        ScoreManager.Instance.ResetHighScore();
        HighScoreSetup();
    }

    // Exit button
    public void ExitGame()
    {
        Application.Quit();
    }

    private void HighScoreSetup()
    {
        highScoreTMP.text = $"High Score: {ScoreManager.Instance.HighScore} \n press to reset";
    }
}
