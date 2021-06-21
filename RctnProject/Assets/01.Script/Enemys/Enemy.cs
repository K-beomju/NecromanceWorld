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


    protected override void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        enemyManager = GetComponentInParent<EnemyManager>();

    }



    void Update()
    {
        base.Attack("Player");
        if(isAttack)
        {
            enemyGroup.enabled = false;
        }
        else
        {
             enemyGroup.enabled = true;
        }
        if(enemyGroup.moveSpot.x >= transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }





        if (Input.GetMouseButton(1) && isDead) // 우클릭
        {

            if (enemyManager.SearchGroup())
            {
                GameManager.instance.necroAudio.Play();
                player = GameManager.GetCreatePlayer(0);
                player.SetPosition(transform.position);

                GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);
                OnNecroEffect(1);
                GameManager.CamShake(3f, 0.5f);

                enemyManager.panel.gameObject.SetActive(false);
                gameObject.SetActive(false);

            }

        }


    }

    protected override void Die()
    {
        gameObject.transform.parent = this.gameObject.transform.root;
        isDead = true;
        OnNecroEffect(2);
        anim.enabled = false;
        circle.enabled = false;
        enemyManager.Dead(this);
        sprite.color = Color.red;
    }







}

