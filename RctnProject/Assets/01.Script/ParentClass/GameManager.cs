using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;



    [Header("Pooling Objs")]
    [Space(45)]
    public GameObject[] enemyPrefab;
    public GameObject enemyGroupPrefab;
    public GameObject[] effect;
    public GameObject[] playerPrefab;
    public GameObject deadTxt;
    public GameObject[] enemyDeadPrefab;
    [Space(70)]


    public GameObject crossHair;
    public RectTransform crossTxt;

    public RectTransform canvas;
    public GameObject enemyGroupObj;
    public GameObject playerGroupObj;
    public GameObject endPanel;


    protected ObjectPooling<EnemyGroup> groupPool;
    private ObjectPooling<Enemy>[] enemyPool;
    private ObjectPooling<EffectObject>[] effectPool;
    private ObjectPooling<Uipanel> deadPool;
    private ObjectPooling<LivingPlayer>[] playerPool;
    private ObjectPooling<EnemyDead>[] enemydeadPool;


    public CinemachineTargetGroup cinemachine;
    public CameraEffect camEffect;

    public AudioSource necroAudio;
    public AudioSource[] deadAudio;
    public AudioSource[] attackAudio;
    public AudioSource shopAudio;
    public AudioSource stageAudio;


    public int playerCount;


    void Awake()
    {
        instance = this;
        Cursor.visible = false;

        // 적 그룹 obj
        groupPool = new ObjectPooling<EnemyGroup>(enemyGroupPrefab, enemyGroupObj.transform, 16);

        // 이펙트 풀링
        effectPool = new ObjectPooling<EffectObject>[effect.Length];
        for (int i = 0; i < effect.Length; i++)
        {
            effectPool[i] = new ObjectPooling<EffectObject>(effect[i], this.transform, 10);
        }

        // Text 박스 풀링
        deadPool = new ObjectPooling<Uipanel>(deadTxt, canvas, 10);

        // 적 풀링
        enemyPool = new ObjectPooling<Enemy>[enemyPrefab.Length];
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            enemyPrefab[i].gameObject.SetActive(false);
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], enemyGroupObj.transform, 100);
        }

        // 플레이어 풀링
        playerPool = new ObjectPooling<LivingPlayer>[playerPrefab.Length];
        for (int i = 0; i < playerPrefab.Length; i++)
        {
            playerPrefab[i].gameObject.SetActive(false);
            playerPool[i] = new ObjectPooling<LivingPlayer>(playerPrefab[i], playerGroupObj.transform, 70);
        }

        // 적 죽음 오브젝트 풀링
        enemydeadPool = new ObjectPooling<EnemyDead>[enemyDeadPrefab.Length];
        for (int i = 0; i < enemyDeadPrefab.Length; i++)
        {
            enemyDeadPrefab[i].gameObject.SetActive(false);
            enemydeadPool[i] = new ObjectPooling<EnemyDead>(enemyDeadPrefab[i], this.transform, 10);
        }

        endPanel.SetActive(false);
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

    public static Enemy GetCreateEnemy(int i)
    {
        return  instance.enemyPool[i].GetOrCreate();
    }

    public static EnemyGroup GetCreateGroup()
    {
        return  instance.groupPool.GetOrCreate();
    }

      public static LivingPlayer GetCreatePlayer(int i)
    {
        return  instance.playerPool[i].GetOrCreate();
    }

    public static EnemyDead GetCreateEnemyDead(int i)
    {
        return instance.enemydeadPool[i].GetOrCreate();
    }


    void Update()
    {

        for (int i = 0; i < playerGroupObj.transform.childCount  ; i++)
        {
            if(playerGroupObj.transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }
            crossHair.SetActive(false);
          endPanel.SetActive(true);
            Cursor.visible = true;

    }




















}
