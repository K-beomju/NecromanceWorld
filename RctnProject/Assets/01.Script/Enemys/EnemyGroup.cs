using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public float speed;
    private float patrolSpeed;
    private float waitTime;
    private float startWaitTime;
    public bool isChase;
    private bool isDead;
    private int lastTimeIDied;

    public Vector2 moveSpot;
    private Enemy enemies;
    private EnemyManager enemyManager;
     private Uipanel panel;




    void Start()
    {
        isDead = false;
        isChase = false;
        enemies = GetComponent<Enemy>();
        enemyManager = GetComponentInParent<EnemyManager>();

        startWaitTime = 1; //Random.Range(3, 7);
        waitTime = startWaitTime;
        moveSpot = new Vector2(Random.Range(-33, 33), Random.Range(-33, 33));
    }




    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Enemy>().isAttack)
            {
                speed = 0;
            }
            else
            {
                speed = 3;
            }

        }


        // if(!isChase)
        // {
        //    Platrol();
        // }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                isDead = true;
                lastTimeIDied = i;
                return;
            }
        }
        if(isDead)
         GetDead();

    }

    public void GetDead()
    {
        panel = GameManager.GetDeadText();
         panel.SetPosition(new Vector3(transform.GetChild(lastTimeIDied).transform.position.x
         , transform.GetChild(lastTimeIDied).transform.position.y + 1f,transform.GetChild(lastTimeIDied).transform.position.z));
           isDead = false;

    }


    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpot.x, moveSpot.y - 7.5f), speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot) < 8f)
        {

            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                moveSpot = new Vector2(Random.Range(-33, 33), Random.Range(-33, 23));

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
