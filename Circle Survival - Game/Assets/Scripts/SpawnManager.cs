using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private ParticleSystem fireworks;

    private List<GameObject> objectList = new List<GameObject>();
    private List<ParticleSystem> particleList = new List<ParticleSystem>();

    [SerializeField]
    private List<int> freeGridSpaces = new List<int>();
    public List<int> FreeGridSpaces { get { return freeGridSpaces; } set { freeGridSpaces = value; } }

    [SerializeField]
    [Range(0, 4)]
    private int rows = 4;
    [SerializeField]
    [Range(0, 9)]
    private int minimumCollumn = 0;
    [SerializeField]
    [Range(0, 9)]
    private int maximumCollumn = 9;

    [SerializeField]
    private float spawnTime = 2.0f;
    public float SpawnTime { get { return spawnTime; } set { spawnTime = value; } }

    private float spawnTimeAcceleration = 0.01f;
    private float bombSpawnRate = 10f;
    private float lifeTimerMulti = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onGameOver += DestroyAllGameOver;
        GameManager.instance.onTargetClick += SpawnFireworks;
        FreeGridSpacesSetup(rows, minimumCollumn, maximumCollumn);
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeFix();
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (freeGridSpaces.Count != 0)
                SpawnObject();
            yield return new WaitForSeconds(spawnTime);

        }
    }

    private void SpawnObject()
    {
        int randomPostion = Random.Range(0, freeGridSpaces.Count);
        int position = freeGridSpaces[randomPostion];
        float horizontalPosition = position % 10;
        float verticalPosition = position - position % 10;

        bool isBombSpawned = Random.Range(0, 100) < bombSpawnRate;
        string objectName;
        if (isBombSpawned)
        {
            objectName = "Bomb(Clone)";
        }
        else
        {
            objectName = "Ball(Clone)";
        }


        GameObject newObject = null;
        foreach (GameObject oldObject in objectList)
        {
            if (!oldObject.activeInHierarchy && oldObject.name == objectName)
            {
                newObject = oldObject;
            }
        }



        if (newObject == null)
        {
            if (isBombSpawned)
            {
                SpawnBomb(horizontalPosition, verticalPosition, position);
            }
            else
            {
                SpawnBall(horizontalPosition, verticalPosition, position);
            }
        }
        else
        {
            if (isBombSpawned)
            {
                ReSpawnBomb(horizontalPosition, verticalPosition, position, newObject);
            }
            else
            {
                ReSpawnBall(horizontalPosition, verticalPosition, position, newObject);
            }
        }

        freeGridSpaces.RemoveAt(randomPostion);

    }

    private void DestroyAllGameOver()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            Destroy(objectList[i].gameObject);
        }
        gameObject.SetActive(false);
    }

    private void SpawnBomb(float horizontalPosition, float verticalPosition, int position)
    {
        GameObject newbomb = Instantiate(bomb, new Vector3(horizontalPosition * 10, 0, verticalPosition), Quaternion.identity);
        newbomb.GetComponent<EnemyController>().Position = position;
        objectList.Add(newbomb);

    }

    private void SpawnBall(float horizontalPosition, float verticalPosition, int position)
    {
        GameObject newball = Instantiate(ball, new Vector3(horizontalPosition * 10, 0, verticalPosition), Quaternion.identity);
        newball.GetComponent<TargetController>().Position = position;
        newball.GetComponent<TargetController>().LifeTimerMulti = lifeTimerMulti;
        objectList.Add(newball);
    }

    private void ReSpawnBomb(float horizontalPosition, float verticalPosition, int position, GameObject newobject)
    {
        newobject.transform.position = new Vector3(horizontalPosition * 10, 0, verticalPosition);
        newobject.GetComponent<EnemyController>().Position = position;
        newobject.SetActive(true);
    }

    private void ReSpawnBall(float horizontalPosition, float verticalPosition, int position, GameObject newobject)
    {
        newobject.transform.position = new Vector3(horizontalPosition * 10, 0, verticalPosition);
        newobject.GetComponent<TargetController>().Position = position;
        newobject.GetComponent<TargetController>().LifeTimerMulti = lifeTimerMulti;
        newobject.SetActive(true);
    }

    private void SpawnTimeFix()
    {
        spawnTime = spawnTime * (1 - spawnTimeAcceleration * Time.deltaTime);
        lifeTimerMulti = lifeTimerMulti * (1 - spawnTimeAcceleration * Time.deltaTime);
    }

    private void FreeGridSpacesSetup(int numberOfRows, int minCollumn, int maxCollumn)
    {
        if (minCollumn > maxCollumn)
        {
            Debug.LogError("minimum and maximum collumn value is wrong; minimum and maximum collumn value is switched");
            int temporary = minimumCollumn;
            minCollumn = maxCollumn;
            maxCollumn = temporary;
        }

        for (int i = 0; i < numberOfRows * 10; i++)
        {
            int checkCollumn = i % 10;
            if (checkCollumn >= minCollumn && checkCollumn <= maxCollumn)
                freeGridSpaces.Add(i);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.onGameOver -= DestroyAllGameOver;
    }

    void SpawnFireworks(Transform newposition)
    {
            ParticleSystem newfireworks = Instantiate(fireworks, newposition.position, Quaternion.identity);
    }
}
