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
    public EnemyStruct[] enemyStructs; // 몬스터 그룹



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
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], this.transform, 10);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            int rand = UnityEngine.Random.Range(2,5);

            for (int i = 0; i < rand; i++)
            {
               enemyStructs[0].list.Add(enemyPool[0].GetOrCreate());
            }


            yield return new WaitForSeconds(4f);
        }
    }

    public void Dead(Enemy _enemy)
    {
        for (int i = enemyStructs.Length - 1;  i >= 0; i--)
        {
        enemyStructs[i].list.Remove(_enemy);

        }
    }


     public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }
}
