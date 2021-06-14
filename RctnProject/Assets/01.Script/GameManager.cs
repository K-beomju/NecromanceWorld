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
    [Space(45)]
    public GameObject[] enemyPrefab;
    public GameObject[] effect;
    public GameObject deadTxt;

    public RectTransform canvas;

    [Header("Status")]
    [Space(45)]
    [HideInInspector]
    public bool isRun;
    public float moveSpeed;

    public GameObject crossHair;




    public Transform[] spawnPoint;
    public int spawn;





    private ObjectPooling<Enemy>[] enemyPool;
    private ObjectPooling<EffectObject>[] effectPool;
    private ObjectPooling<Uipanel> deadPool;

    public CinemachineTargetGroup cinemachine;
    public CameraEffect camEffect;






    void Awake()
    {


        instance = this;
        Cursor.visible = false;


        effectPool = new ObjectPooling<EffectObject>[effect.Length];
        for (int i = 0; i < effect.Length; i++)
        {
            effectPool[i] = new ObjectPooling<EffectObject>(effect[i], this.transform, 10);
        }

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
        moveSpeed = 1;
        enemyGroup = 0;
        playerGroup = 5;
        enemyCount = 0;
        spawn = 0;


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
            if (enemyGroup != 2)
            {
                int randCount = UnityEngine.Random.Range(playerGroup - 3, playerGroup + 1);
                enemyCount = randCount;
                enemyStructs[enemyGroup].enemy = new Enemy[enemyCount];

                for (int i = 0; i < enemyCount; i++)
                {

                    int t = UnityEngine.Random.Range(0, 360);

                    enemyStructs[enemyGroup].list.Add(enemyStructs[enemyGroup].enemy[i] = enemyPool[0].GetOrCreate());
                    enemyStructs[enemyGroup].enemy[i].transform.position = new Vector2(spawnPoint[spawn].transform.position.x, spawnPoint[spawn].transform.position.y) +
                      new Vector2(Mathf.Cos(t * 1), Mathf.Sin(t * 1));

                }
               StartCoroutine(GroupMove(enemyGroup, enemyCount));
                enemyGroup++;



                spawn++;




            }

            yield return new WaitForSeconds(2f);

        }
    }

    private IEnumerator GroupMove(int enemyGroup , int enemyCounts)
    {

         while (true)
         {
            for (int j = 0; j < enemyCounts; j++)
             {
                 if(GameManager.instance.enemyStructs[enemyGroup].enemy[j] != null)
                 {

                    int randX = UnityEngine.Random.Range(-20,20);
                    int randY = UnityEngine.Random.Range(-10,10);
                 GameManager.instance.enemyStructs[enemyGroup].enemy[j].transform.position
                 =  Vector2.MoveTowards(GameManager.instance.enemyStructs[enemyGroup].enemy[j].transform.position,
                 new Vector2(randX,randY).normalized, moveSpeed * Time.deltaTime);

                 }

             }
            yield return null;
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

    public static EffectObject GetHitEffect(int i)
    {
        return instance.effectPool[i].GetOrCreate();
    }

    public static Uipanel GetDeadText()
    {
        return instance.deadPool.GetOrCreate();
    }





}
