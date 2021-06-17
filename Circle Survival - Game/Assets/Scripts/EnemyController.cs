using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float lifeTimer = 3;
    private int position;
    public int Position { get { return position; } set { position = value; } }
    private Collider thisCollider;

    void Start()
    {
        thisCollider = gameObject.GetComponent<Collider>();
        StartCoroutine(DestroyMe());
    }

    private void Update()
    {
        TouchControll();
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(lifeTimer);
        SpawnManager.instance.FreeGridSpaces.Add(position);
        Destroy(gameObject);
    }

    private void CheckRayCast(Vector3 input)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == thisCollider)
            {
                ClickedOnBomb();
            }
        }
    }

    private void ClickedOnBomb()
    {
        GameManager.instance.GameOver();
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
}
