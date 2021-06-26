using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    private Enemy enemy;
    private Uipanel panel;
    private EnemyManager enemyManager;
    private EnemyGroup enemyGroup;
    private LivingPlayer player;
    private EnemyDead enemyDead;

    public int enemyMobs;
    public float chaseSpeed;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        enemyManager = GetComponentInParent<EnemyManager>();
    }



    void Update()
    {
        SensingAttack();
        moveSpotDir();


        anim.SetBool("Idle", IsDir());
        anim.SetBool("Attack", isAttack);
    }

    protected Vector3 moveSpotDir()
    {
        if (enemyGroup.moveSpot.x < transform.position.x)
        {
            return transform.localScale = new Vector2(-1, 1);
        }
          return transform.localScale = new Vector2(1, 1);
    }

    protected  bool IsDir()
    {
         if(enemyGroup.isChase ? isIdle : !isIdle)
        {
            return true;
        }
        return false;
    }


    protected override void Die()
    {
        isDead = true;
        isAttack = false;
        isIdle = false;
        enemyDead = GameManager.GetCreateEnemyDead(enemyMobs);
        enemyDead.SetPosition(transform.position);

        OnNecroEffect(2);
        anim.enabled = false;
        circle.enabled = false;
        enemyManager.Dead(this);
        gameObject.SetActive(false);
    }


    public void OnNecromance()
    {
        Debug.Log("hrd");
        gameObject.transform.parent = this.gameObject.transform.root;
        GameManager.instance.necroAudio.Play();
        player = GameManager.GetCreatePlayer(enemyMobs);
        player.SetPosition(transform.position);
        enemyDead.gameObject.SetActive(false);
        GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);

        OnNecroEffect(1);
        GameManager.CamShake(4f, 0.5f);



    }



}

