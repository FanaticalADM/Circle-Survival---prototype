using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem clickedParticle;

    private float lifeTimer;

    private float lifeTimerMulti;
    public float LifeTimerMulti { get { return lifeTimerMulti; } set { lifeTimerMulti = value; } }

    private float currentLifeTimer;

    Material material;
    private float startingHue=0.32f;

    private int position;
    public int Position { get { return position; } set { position = value; } }

    [SerializeField]
    private int scoreValue = 1;

    private float awesomeClick=0.8f;
    private float greatClisk=0.7f;
    private float goodClick=0.3f;
    private float badClick = 0.2f;
    private float verybadClick = 0.1f;

    private Collider thisCollider;

    void Start()
    {
        thisCollider = gameObject.GetComponent<Collider>();
        material = GetComponent<MeshRenderer>().material;
        LifeTimerSetup();
    }

    void Update()
    {
        TouchControll();
        ColorChange();
        CheckGameOver();
    }

    private void DestroyBall()
    {
        Instantiate(clickedParticle, transform.position, Quaternion.identity);
        float lifeTimeProcentage = currentLifeTimer / lifeTimer;
        GameManager.instance.TargetClick();
        SpawnManager.instance.FreeGridSpaces.Add(position);

        if (lifeTimeProcentage > awesomeClick)
        {
            GameManager.instance.FeedbackRespond("AWESOME");
        }
        else if (lifeTimeProcentage > greatClisk)
        {
            GameManager.instance.FeedbackRespond("GREAT");
        }
        else if (lifeTimeProcentage > goodClick)
        {
            GameManager.instance.FeedbackRespond("GOOD");
        }
        else if (lifeTimeProcentage > badClick)
        {
            GameManager.instance.FeedbackRespond("KEEP UP");
        }
        else if (lifeTimeProcentage > verybadClick)
        {
            GameManager.instance.FeedbackRespond("SO CLOSE");
        }
        else
            GameManager.instance.FeedbackRespond("PHEW");

        ScoreManager.instance.ScoreChange(scoreValue);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        CheckRayCast(Input.mousePosition);
    }

    private void TouchControll()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            CheckRayCast(Input.touches[0].position);
        }
    }

    private void CheckRayCast(Vector3 input)
    {
        Ray ray = Camera.main.ScreenPointToRay(input);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == thisCollider)
            {
                DestroyBall();
            }
        }
    }

    private void LifeTimerSetup()
    {
        lifeTimer = Random.Range(2, 4) * lifeTimerMulti;
        currentLifeTimer = lifeTimer;
    }

    private void ColorChange()
    {
        if (currentLifeTimer > 0)
        {
            currentLifeTimer -= Time.deltaTime;
            material.color = Color.HSVToRGB(startingHue * (currentLifeTimer / lifeTimer), 0.69f, 1);
        }
    }

    private void CheckGameOver()
    {
        if (currentLifeTimer < 0)
        {
            GameManager.instance.GameOver();
        }
    }

}
