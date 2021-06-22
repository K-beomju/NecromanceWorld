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

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        enemyManager = GetComponentInParent<EnemyManager>();

    }




    void Update()
    {

        // if (Input.GetMouseButton(1) && isDead) // 우클릭
        // {

        //     if (enemyManager.SearchGroup())
        //     {
        //         GameManager.instance.necroAudio.Play();
        //         player = GameManager.GetCreatePlayer(0);
        //         player.SetPosition(transform.position);






        //         GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);
        //         OnNecroEffect(1);
        //         GameManager.CamShake(4f, 0.5f);

        //         enemyManager.panel.gameObject.SetActive(false);
        //         gameObject.SetActive(false);

        //     }

        // }


        if (enemyGroup.isPatrol)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
        anim.SetBool("Attack", isAttack);
        SensingAttack();


    }


    protected override void Attack()
    {
        if (isAttack)
        {
            LivingEntity target = hitCollider.transform.GetComponent<LivingEntity>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }

        }
    }

    private void SensingAttack()
    {
        if (!isDead)
        {
            hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, whatIsLayer);
            if (hitCollider)
            {
                moveSpeed = 1;
                isAttack = true;
            }
            else
            {
                moveSpeed = 4;
                isAttack = false;
            }
        }
    }


    public override void OnDamage(float damage)
    {
        StartCoroutine(ChangeColor(myColor));
        base.OnDamage(damage);
    }

    protected override void Die()
    {
        isDead = true;

       gameObject.transform.parent = this.gameObject.transform.root;
        OnNecroEffect(2);
        anim.enabled = false;
        circle.enabled = false;
        enemyManager.Dead(this);

    }



}

