using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickSound;
    private AudioSource audioSource;

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}
