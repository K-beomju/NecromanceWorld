using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public float speed;
    private float waitTime;
    private float startWaitTime;
    public bool isPatrol;

    public Vector2 moveSpot;
    private Enemy[] enemies;




    void Start()
    {
        enemies = GetComponentsInChildren<Enemy>();

        startWaitTime = Random.Range(3, 7);
        waitTime = startWaitTime;
        moveSpot = new Vector2(Random.Range(-25, 25), Random.Range(-18, 18));
    }
    void OnEnable()
    {
    //  /   StartCoroutine(CurrentEnemy());
    }


    IEnumerator CurrentEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (this.gameObject.transform.childCount == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }





    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpot.x, moveSpot.y - 7.5f), speed * Time.deltaTime);
        isPatrol = true;
        if (Vector2.Distance(transform.position, moveSpot) < 8f)
        {
            isPatrol = false;
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                moveSpot = new Vector2(Random.Range(-25, 25), Random.Range(-20, 20));

            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }




}
