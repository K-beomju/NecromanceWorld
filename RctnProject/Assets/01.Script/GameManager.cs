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
        public Enemy[] enemy;
        public List<Enemy> list; // 몬스터의 수


    }
    public EnemyStruct[] enemyStructs = new EnemyStruct[10]; // 몬스터 그룹

    [HideInInspector]
    public int enemyGroup; // 적 그룹
    [HideInInspector]
    public int playerGroup; // 플레이어 그룹
    [HideInInspector]
    public int enemyCount; // 적 그룹안에 있는 적들의 갯수


    [Header("Pooling Objs")]
    public GameObject[] enemyPrefab;
    public GameObject hitEffect;
    public GameObject deadTxt;

    [Header("Status")]
    public bool isRun;

    public GameObject crossHair;



    public CameraEffect camEffect;
    public RectTransform canvas;

    public Transform[] spawnPoint;
    public int spawn;



    private ObjectPooling<Enemy>[] enemyPool;
    private ObjectPooling<EffectObject> hitPool;
    private ObjectPooling<Uipanel> deadPool;

    public Vector2 moveVector;
    public int x;

    void Awake()
    {


        instance = this;
        Cursor.visible = false;


        hitPool = new ObjectPooling<EffectObject>(hitEffect, this.transform, 10);
        deadPool = new ObjectPooling<Uipanel>(deadTxt, canvas, 10);
        enemyPool = new ObjectPooling<Enemy>[enemyPrefab.Length];
        for (int i = 0; i < enemyPrefab.Length; i++)
        {

            enemyPrefab[i].gameObject.SetActive(false);
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], this.transform, 70);

        }



    }

    protected void Start()
    {
        spawn = 0;
        enemyGroup = 0;
        playerGroup = 5;
        isRun = false;
        if (enemyPrefab != null)
        {
            StartCoroutine(SpawnEnemys());
        }


    }



    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            if (enemyGroup != 1)
            {
                //float randX = UnityEngine.Random.Range(-34, 29);
                // float randY = UnityEngine.Random.Range(-24, 12);
                int randCount = UnityEngine.Random.Range(2, 6);
                enemyCount = randCount;
                enemyStructs[enemyGroup].enemy = new Enemy[enemyCount];

                for (int i = 0; i < enemyCount; i++)
                {

                    int t = UnityEngine.Random.Range(0, 360);

                    enemyStructs[enemyGroup].list.Add(enemyStructs[enemyGroup].enemy[i] = enemyPool[0].GetOrCreate());
                    enemyStructs[enemyGroup].enemy[i].transform.position = new Vector2(spawnPoint[spawn].transform.position.x, spawnPoint[spawn].transform.position.y) +
                      new Vector2(Mathf.Cos(t * 1), Mathf.Sin(t * 1));

                }
                enemyGroup++;

                if (spawn >= 10)
                {
                    spawn = 0;
                }
                else
                {
                    spawn++;
                }



            }

            yield return new WaitForSeconds(10f);

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

    public static EffectObject GetHitEffect()
    {
        return instance.hitPool.GetOrCreate();
    }

    public static Uipanel GetDeadText() // 적 HP bar
    {
        return instance.deadPool.GetOrCreate();
    }
}
