using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    private Enemy enemy;
    private Uipanel panel;
    private EnemyManager enemyManager;
    private LivingPlayer player;


    protected override void Start()
    {
        isDead = false;
        isAttack = false;
        enemy = GetComponent<Enemy>();
        enemyManager = GetComponentInParent<EnemyManager>();

    }



    void Update()
    {
        base.Attack("Player");

        if (Input.GetMouseButton(1) && isDead) // 우클릭
        {

            if(enemyManager.SearchGroup())
            {
                GameManager.instance.necroAudio.Play();
                player = GameManager.GetCreatePlayer(0);
                player.SetPosition(transform.position);

                GameManager.instance.cinemachine.AddMember(player.transform, 1, 0);
                OnNecroEffect(1);
                GameManager.CamShake(3f, 0.5f);


                   // isDead = false;
                    enemyManager.panel.gameObject.SetActive(false);
                  //  Destroy(enemy);
                    gameObject.SetActive(false);

            }

        }




        if (GameManager.instance.isRun)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }



    }

    protected override void Die()
    {
        isDead = true;
        OnNecroEffect(2);
        anim.enabled = false;
        circle.enabled = false;
        enemyManager.Dead(this);
        sprite.color = Color.red;
    }







}

