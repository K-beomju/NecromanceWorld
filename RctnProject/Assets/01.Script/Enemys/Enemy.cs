using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Player player;
    public Enemy enemy;
    private Uipanel panel;


    protected override void Start()
    {
        isDead = false;
        isAttack = false;

    }



    void Update()
    {
        base.Attack("Player");

        if (Input.GetMouseButton(1) && isDead) // 우클릭
        {
            for (int i = 0; i < GameManager.instance.enemyGroup; i++) // 몇번째 그룹인지 검사
            {
                if (GameManager.instance.enemyStructs[i].list.Count == 0) // 그 그룹의 리스트가 비었다면
                {
                    for (int j = 0; j < GameManager.instance.enemyCount; j++) // 그 그룹의 적 갯수만큼
                    {
                        if (GameManager.instance.enemyStructs[i].enemy[j] == isDead)
                        {

                            sprite.color = Color.white; // 플레리어 색상변환

                            OnNecroEffect(1);
                            player.enabled = true; // 플레이어 스크립트 활성화
                            circle.enabled = true;

                            isDead = false;

                            Destroy(enemy);
                            GameManager.CamShake(3f, 0.5f);
                            GameManager.instance.cinemachine.AddMember(this.gameObject.transform, 1, 0);



                        }


                    }
                }
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


        GameManager.instance.moveSpeed = 0;
        gameObject.layer = 3; // 플레이어 레이어
                              // 플레이어 그룹 증가
        OnNecroEffect(2);
        sprite.color = Color.red;
        circle.enabled = false;
        GameManager.instance.Dead(this); // 적 그룹 리스트에서 삭제





    }







}

