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
        if (isAttack)
        {
            ChaseTarget(hitCollider);
        }

        if (enemyGroup.moveSpot.x > transform.position.x)
        {
            transform.localScale = new Vector2(ChangePos.x, ChangePos.y);
        }
        else
        {
            transform.localScale = new Vector2(-ChangePos.x, ChangePos.y);
        }




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



    private void ChaseTarget(Collider2D hitCollider)
    {
        if (hitCollider != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, hitCollider.transform.position, chaseSpeed * Time.deltaTime);

        }
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
                if (enemyGroup.moveSpot.x < hitCollider.transform.position.x)
                {
                    transform.localScale = new Vector2(ChangePos.x, ChangePos.y);
                }
                else
                {
                    transform.localScale = new Vector2(-ChangePos.x, ChangePos.y);
                }

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
        isAttack = false;


        OnNecroEffect(2);
        anim.enabled = false;
        circle.enabled = false;
        enemyManager.Dead(this);

    }


    public void OnNecromance()
    {
        GameManager.instance.necroAudio.Play();
        player = GameManager.GetCreatePlayer(enemyMobs);
        player.SetPosition(transform.position);

        GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);

        OnNecroEffect(1);
        GameManager.CamShake(4f, 0.5f);

        gameObject.transform.parent = this.gameObject.transform.root;
        gameObject.SetActive(false);

    }



}

