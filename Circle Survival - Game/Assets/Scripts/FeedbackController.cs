using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackController : MonoBehaviour
{
    private TMP_Text textComponent;
    
    enum TextStatus
    {
        Awesome = 0,
        Great = 1,
        Good = 2,
        Bad = 3,
        VeryBad = 4,
        Phew = 5,
        Title = 6
    }

    private TextStatus textStatus;

    private int frameCounter;
    private int frameDivider = 30;

    private float randomR;
    private float randomG;
    private float randomB;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = gameObject.GetComponent<TMP_Text>();
        textComponent.text = "CIRCLE SURVIVAL";
        textStatus = TextStatus.Title;

        GameManager.instance.onFeedbackRespond += CheckFeedback;
    }

    // Update is called once per frame
    void Update()
    {
        FeedbackTextStyle();
    }

    private void CheckFeedback(float feedback)
    {
        if (feedback > 0.7)
        {
            textStatus = TextStatus.Awesome;
            textComponent.text = "AWESOME";
        }
        else if (feedback > 0.5)
        {
            textStatus = TextStatus.Great;
            textComponent.text = "GREATE";
        }
        else if (feedback > 0.3)
        {
            textStatus = TextStatus.Good;
            textComponent.text = "GOOD";
        }
        else if (feedback > 0.2)
        {
            textStatus = TextStatus.Bad;
            textComponent.text = "KEEP UP";
        }
        else if (feedback > 0.1)
        {
            textStatus = TextStatus.VeryBad;
            textComponent.text = "ALMOST";
        }
        else
        {
            textStatus = TextStatus.Phew;
            textComponent.text = "PHEW";
        }
    }

    private void FeedbackTextStyle()
    {
        frameCounter++;
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

            for (int j = 0; j < 4; j++)
            {
                int index = charInfo.vertexIndex + j;
                var orig = meshInfo.vertices[index];
                if (textStatus == TextStatus.Awesome || textStatus == TextStatus.Great)
                {
                    float speedRate = 0;

                    if (textStatus == TextStatus.Awesome)
                        speedRate = 15f;
                    if (textStatus == TextStatus.Great)
                        speedRate = 10f;

                    meshInfo.vertices[index] = orig + new Vector3(0, Mathf.Sin(Time.time * speedRate + orig.x * 0.01f) * 10f, 0);
                }

                if (textStatus == TextStatus.Awesome)
                {
                    if (frameCounter % frameDivider == 0)
                    {
                        randomR = Random.Range(0f, 1f);
                        randomG = Random.Range(0f, 1f);
                        randomB = Random.Range(0f, 1f);
                    }
                    meshInfo.colors32[index] = new Color(randomR, randomG, randomB);
                }

                if (textStatus == TextStatus.Great)
                {
                    meshInfo.colors32[index] = new Color(0.1f, 1f, 0.3f);
                }

                if (textStatus == TextStatus.Good)
                {
                    meshInfo.colors32[index] = new Color(1f, 1f, 1f);
                }

                if (textStatus == TextStatus.Bad)
                {
                    meshInfo.colors32[index] = new Color(1f, 0.4f, 0.1f);
                }

                if (textStatus == TextStatus.VeryBad || textStatus == TextStatus.Phew)
                {
                    meshInfo.colors32[index] = new Color(1f, 0f, 0f);
                }

            }

        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.onFeedbackRespond -= CheckFeedback;
    }
}
