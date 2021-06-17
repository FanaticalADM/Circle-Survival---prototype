using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        CheckToDestroyParticle();
    }

    private void CheckToDestroyParticle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
