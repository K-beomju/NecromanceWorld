using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Serializable]
    public struct EnemyStruct
    {
        public List<Enemy> list; // 몬스터의 수
        public Enemy enemy;
    }
    public EnemyStruct[] enemyStructs = new EnemyStruct[10]; // 몬스터 그룹

    [HideInInspector]
    public int enemyGroup; // 적 그룹
    [HideInInspector]
    public int playerGroup; // 플레이어 그룹
    [HideInInspector]
    public int enemyCount; // 적 그룹안에 있는 적들의 갯수


    public GameObject crossHair;
    public CameraEffect camEffect;


    public GameObject[] enemyPrefab;
    private ObjectPooling<Enemy>[] enemyPool;

    public CinemachineVirtualCamera myCinemachine;





    void Awake()
    {

         myCinemachine = GetComponent<CinemachineVirtualCamera>();
        instance = this;
        Cursor.visible = false;
        enemyPool = new ObjectPooling<Enemy>[enemyPrefab.Length];
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            enemyPrefab[i].gameObject.SetActive(false);
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], this.transform, 50);
        }


    }

    protected void Start()
    {
        enemyGroup = 0;
        enemyCount = 0;
        playerGroup = 5;
        StartCoroutine(SpawnEnemys());

    }





    private IEnumerator SpawnEnemys()
    {
            while (true)
            {
                float randX = UnityEngine.Random.Range(-35, 30);
                float randY = UnityEngine.Random.Range(-24, 12);
                if (enemyPrefab != null)
                {
                    int randCount = UnityEngine.Random.Range(2, 5);
                    enemyCount = randCount;
                    for (int i = 0; i <= enemyCount; i++)
                    {
                        int t = UnityEngine.Random.Range(0, 360);
                        enemyStructs[enemyGroup].enemy = enemyPool[0].GetOrCreate();
                        enemyStructs[enemyGroup].enemy.transform.position = /*new Vector2(randX, randY) +*/ new Vector2(Mathf.Cos(t * 1), Mathf.Sin(t * 1));
                        enemyStructs[enemyGroup].list.Add(enemyStructs[enemyGroup].enemy);
                    }

                    if (enemyGroup >= 9)
                    {
                        enemyGroup = 0;
                    }
                    else
                    {
                        enemyGroup++;
                    }
                }
               yield return new WaitForSeconds(1000f);
            }



    }

    public void Dead(Enemy _enemy)
    {
        for (int i = enemyStructs.Length - 1; i >= 0; i--)
        {
            enemyStructs[i].list.Remove(_enemy);

        }
    }





    public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }
}
