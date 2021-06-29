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
    public bool isHit;

    void Start()
    {
        isHit = false;
        SetAbility(0, 0);
        enemy = GetComponent<Enemy>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        enemyManager = GetComponentInParent<EnemyManager>();

    }


    void Update()
    {
          SensingAttack();
        //moveSpotDir();


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

    protected bool IsDir()
    {
        if (enemyGroup.isChase ? isIdle : !isIdle)
        {
            return true;
        }
        return false;
    }

    public override void OnDamage(float damage)
    {
        if (this.gameObject.activeInHierarchy)
        {
            isHit = true;
            StartCoroutine(ChangeColor());
        }

        base.OnDamage(damage);
    }


    protected override void Die()
    {
        isDead = true;
        isAttack = false;
        isIdle = false;
        if(mobGrade <=2)
        {
        transform.GetChild(0).transform.rotation = Quaternion.identity;
        transform.GetChild(0).transform.localPosition = knifeLocalPos;
        }

        enemyDead = GameManager.GetCreateEnemyDead(mobGrade);
        UiManager.instance.UpDateMobPieces(mobGrade);
        enemyDead.SetPosition(transform.position);
          GameManager.instance.deadAudio[1].Play();
        OnNecroEffect(2);
        enemyManager.Dead(this);
        gameObject.SetActive(false);
    }


    public void OnNecromance()
    {
        gameObject.transform.parent = this.gameObject.transform.root;
        GameManager.instance.necroAudio.Play();
        player = GameManager.GetCreatePlayer(mobGrade);
        player.SetPosition(transform.position);
        enemyDead.gameObject.SetActive(false);
        GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);

        OnNecroEffect(1);
        GameManager.CamShake(4f, 0.5f);
    }



}

