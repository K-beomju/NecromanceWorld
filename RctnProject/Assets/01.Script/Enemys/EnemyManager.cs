using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyStruct
    {
        public Enemy[] enemy;
        public List<Enemy> list; // 몬스터의 수

    }
    public EnemyStruct[] enemyStructs; // 몬스터 그룹

    private Enemy enemyPool;
    private EnemyGroup enemyGroup;
    [HideInInspector]
    public Uipanel panel;



    public int enemyGroupCount; // 적 그룹
    private int enemyCount; // 적 그룹안에 있는 적들의 갯수
    public Transform[] spawnPoint;
    private int spawn;
    private int enemySet;
    private bool isClear;

    [Header("적의 그룹수 지정")]
    public int setEnemyGroup;
    public int remainingEnemy;

    public GameObject playerGroup;

    public int enemyGrade;

    void OnEnable()
    {
        isClear = false;
        enemySet = 0;
        enemyCount = 0;


    }

    void Start()
    {
        EnemySpawner();

    }

    public void EnemySpawner()
    {

        for (enemyGroupCount = 0; enemyGroupCount < setEnemyGroup; enemyGroupCount++)
        {
            SetEnemyGroup();

            if (UiManager.instance.attackPower <= 10)
            {
                enemyGrade = 0;
            }
            else if (UiManager.instance.attackPower <= 20)
            {
                var random = UnityEngine.Random.Range(0, 11);
                enemyGrade = random <= 7 ? 0 : 1;
            }
            else if(UiManager.instance.attackPower <= 30)
            {
                var random = UnityEngine.Random.Range(0,10);

                if(random <= 3)
                {
                    enemyGrade = 0;
                }
                else if(random <=6 )
                {
                    enemyGrade = 1;
                }
                else
                {
                    enemyGrade = 2;
                }
            }

            SetCreateEnemy(enemyGrade);
            enemyGroup.SetPositionData(new Vector2(spawnPoint[spawn].transform.position.x, spawnPoint[spawn].transform.position.y), Quaternion.identity);
            spawn = (spawn + 1) % spawnPoint.Length;
        }
    }

    void Update()
    {
        for (int i = 0; i < remainingEnemy; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                isClear = true;
                return; // 아직 살아있다면 리턴

            }
        }
        if (isClear)
        {
            Cursor.visible = true;
            UiManager.instance.OnShopPanel();
            isClear = false;
        }
    }

    public void SetEnemyGroup()
    {
        int randCount = 0;
        //적의 스폰 카운트 지정 플레이어 유닛에 맞춰서 지정

            if(enemyGrade == 0)
            randCount = UnityEngine.Random.Range(GameManager.instance.playerCount - 3, GameManager.instance.playerCount + 1);


        enemyCount = randCount;
        Debug.Log(GameManager.instance.playerCount + " " + enemyGrade + " " + enemyCount);
        //적 컨테이너에 적의 {i}번째 그룹에 enemyCount를 미리 생성해준다.
        enemyStructs[enemyGroupCount].enemy = new Enemy[enemyCount];
        //적의 그룹을 가져온다.
        enemyGroup = GameManager.GetCreateGroup();
    }

    public void SetCreateEnemy(int enemyMob)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int t = UnityEngine.Random.Range(0, 361);
            enemyStructs[enemyGroupCount].list.Add(enemyStructs[enemyGroupCount].enemy[i] = GameManager.GetCreateEnemy(enemyMob));
            // 적 컨테이너 그룹에 들어갈 적들을 리스트로 더해주고 enemySet의 (기본적)을 만들어준다.
            //적들을 미리 만들어둔 적의 그룹에 넣어준다.
            enemyStructs[enemyGroupCount].enemy[i].gameObject.transform.parent = enemyGroup.transform;
            // 적 원형 생성
            enemyStructs[enemyGroupCount].enemy[i].transform.position += new Vector3(Mathf.Cos(t), Mathf.Sin(t)); // Mathf.Cos(t) / 2, Mathf.Sin(t) / 2

        }
        // 적 그룹의 포지션을 미리 지정해둔 스폰 포인트에 설정해준다.
    }

    public void Dead(Enemy _enemy)
    {
        for (int i = enemyGroupCount - 1; i >= 0; i--)
        {
            enemyStructs[i].list.Remove(_enemy);
            enemyStructs[i].enemy = null;
        }
    }


}
