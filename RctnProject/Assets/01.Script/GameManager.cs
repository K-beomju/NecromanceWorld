using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyStruct
    {

        public List<Enemy> list; // 몬스터의 수
    }
    public EnemyStruct[] enemyStructs = new EnemyStruct[10]; // 몬스터 그룹
    [HideInInspector]
    public int enemyGroup; // 적 그룹
    [HideInInspector]
    public int playerGroup; // 플레이어 그룹
    [HideInInspector]
    public int enemyCount; // 적 그룹안에 있는 적들의 갯수


    public static GameManager instance;
    public GameObject crossHair;
    public CameraEffect camEffect;

    public GameObject[] enemyPrefab;
    private ObjectPooling<Enemy>[] enemyPool;





    void Awake()
    {


        instance = this;
        Cursor.visible = false;
        enemyPool = new ObjectPooling<Enemy>[enemyPrefab.Length];
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], this.transform, 50);
        }
        enemyGroup = 0;
        enemyCount = 0;
        playerGroup = 5;
    }

    protected void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    private IEnumerator SpawnEnemys()
    {

        while (true)
        {
            if (enemyPrefab != null)
            {
                for (int i = 0; i <= enemyGroup; i++)
                {
                    if (enemyStructs[i].list.Count == 0)
                    {
                        enemyGroup = i;
                    }
                    else
                    {
                        enemyGroup++;
                    }
                }

                int randCount = UnityEngine.Random.Range(2, 5);
                enemyCount = randCount;
                for (int i = 0; i <=  2; i++)
                {
                    enemyStructs[enemyGroup].list.Add(enemyPool[0].GetOrCreate());

                }
                yield return new WaitForSeconds(10f);
            }
        }

    }







    public void Dead(Enemy _enemy)
    {
        for (int i = enemyStructs.Length - 1; i >= 0; i--)
        {
            enemyStructs[i].list.Remove(_enemy);

        }
    }


    // public static void CamShake(float intense, float during)
    // {
    //     instance.camEffect.SetShake(intense, during);
    // }
}
