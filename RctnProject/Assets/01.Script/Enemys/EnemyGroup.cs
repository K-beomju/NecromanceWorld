using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public float speed;
    private float waitTime;
    private float startWaitTime;
    public bool isChase;
    private bool isDead;
    private int lastTimeIDied;

    public Vector3 moveSpot;
    private Enemy enemies;
    private EnemyManager enemyManager;
    private Uipanel panel;


    // 공격시 추적
    public List<GameObject> playerList = new List<GameObject>();
    public GameObject target;
    private float currentDist = 0;
    public float range;

    void Start()
    {

    }

    void OnEnable()
    {

        isChase = false;

          isDead = false;

        enemies = GetComponent<Enemy>();
        enemyManager = GetComponentInParent<EnemyManager>();

        startWaitTime = 1; //Random.Range(3, 7);
        waitTime = startWaitTime;
        moveSpot = new Vector2(Random.Range(-33, 33), Random.Range(-33, 33));

            for (int i = 0; i < enemyManager.playerGroup.transform.childCount; i++)
        {
            if (enemyManager.playerGroup.transform.GetChild(i).gameObject.activeSelf)
            {
                playerList.Add(enemyManager.playerGroup.transform.GetChild(i).gameObject);
            }
        }
    }


    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Enemy>().isHit)
            {
               isChase = true;
                if(isChase)
                {
               Chase();
                }
            }
        }
        if(!isChase)
        Patrol();


        // 텍스트 패널
        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).gameObject.activeSelf)
            {
                isDead = true;
                lastTimeIDied = i;
                return;
            }
        }
        if (isDead)
            GetDead();

    }

    public void GetDead()
    {
        panel = GameManager.GetDeadText();
        panel.SetPosition(new Vector3(transform.GetChild(lastTimeIDied).transform.position.x
        , transform.GetChild(lastTimeIDied).transform.position.y + 1f, transform.GetChild(lastTimeIDied).transform.position.z));
        isDead = false;

    }


    public void Chase()
    {
        if(target == null)
        {

        float shortDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach (GameObject player in playerList)
        {
            float DistanceToPlayers = Vector2.Distance(transform.position, player.transform.position);

            if (DistanceToPlayers < shortDistance)
            {
                shortDistance = DistanceToPlayers;
                nearestPlayer = player;

            }
        }
        if (nearestPlayer != null && shortDistance <= range)
        {
            target = nearestPlayer;

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position , speed * Time.deltaTime);
            // if(Vector2.Distance(transform.position,target.transform.position) < 0.1f)
            // {
            //     speed = 0;
            // }
        }
        // else
        // {

        //     Chase();
        //     isChase = false;
        //     target = null;
        // }
        }

    }

    void OnDrawGizmos()
    {
        if(target != null)
        {
            Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position , target.transform.position - transform.position);
          Gizmos.color = Color.white;

        }
        Gizmos.DrawWireSphere(transform.position , range);
   Gizmos.DrawRay(transform.position , moveSpot - transform.position);
    }



    public void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpot.x, moveSpot.y), speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot) < 1f)
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
