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


    [Header("Status")]
    [Space(45)]
    [HideInInspector]
    public bool isRun;
    public float moveSpeed;

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


    public CinemachineTargetGroup cinemachine;
    public CameraEffect camEffect;






    public AudioSource necroAudio;
    public AudioSource deadAudio;
    public AudioSource[] attackAudio;



    void Awake()
    {
        instance = this;
        Cursor.visible = false;

        // 적 그룹 obj
        groupPool = new ObjectPooling<EnemyGroup>(enemyGroupPrefab, enemyGroupObj.transform, 3);

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
            enemyPool[i] = new ObjectPooling<Enemy>(enemyPrefab[i], enemyGroupObj.transform, 70);
        }

        // 플레이어 풀링
        playerPool = new ObjectPooling<LivingPlayer>[playerPrefab.Length];
        for (int i = 0; i < playerPrefab.Length; i++)
        {
            playerPrefab[i].gameObject.SetActive(false);
            playerPool[i] = new ObjectPooling<LivingPlayer>(playerPrefab[i], playerGroupObj.transform, 70);
        }

    }

    protected void Start()
    {
        moveSpeed = 3;
        isRun = false;
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























}
