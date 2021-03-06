using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip gameoverSound;
    [SerializeField]
    private AudioClip targetClicked;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameOver += ExplosionSound;
        ScoreManager.Instance.onScoreChange += TargetSound;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void TargetSound()
    {
        audioSource.PlayOneShot(targetClicked);
    }

    private void ExplosionSound()
    {
        audioSource.PlayOneShot(gameoverSound);
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameOver -= ExplosionSound;
        ScoreManager.Instance.onScoreChange -= TargetSound;
    }
}
